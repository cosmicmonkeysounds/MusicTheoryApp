using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
    /// <summary>
    /// This feedback will make the bound renderer flicker for the set duration when played (and restore its initial color when stopped)
    /// </summary>
    [AddComponentMenu("")]
    [FeedbackHelp("This feedback lets you flicker the color of a specified renderer (sprite, mesh, etc) for a certain duration, at the specified octave, and with the specified color. Useful when a character gets hit, for example (but so much more!).")]
    [FeedbackPath("Renderer/Flicker")]
    public class MMFeedbackFlicker : MMFeedback
    {
        /// sets the inspector color for this feedback
        #if UNITY_EDITOR
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.RendererColor; } }
        #endif

        /// the possible modes
        /// Color : will control material.color
        /// PropertyName : will target a specific shader property by name
        public enum Modes { Color, PropertyName }

        [Header("Flicker")]
        /// the renderer to flicker when played
        [Tooltip("the renderer to flicker when played")]
        public Renderer BoundRenderer;
        /// the selected mode to flicker the renderer 
        [Tooltip("the selected mode to flicker the renderer")]
        public Modes Mode = Modes.Color;
        /// the name of the property to target
        [MMFEnumCondition("Mode", (int)Modes.PropertyName)]
        [Tooltip("the name of the property to target")]
        public string PropertyName = "_Tint";
        /// the duration of the flicker when getting damage
        [Tooltip("the duration of the flicker when getting damage")]
        public float FlickerDuration = 0.2f;
        /// the frequency at which to flicker
        [Tooltip("the frequency at which to flicker")]
        public float FlickerOctave = 0.04f;
        /// the color we should flicker the sprite to 
        [Tooltip("the color we should flicker the sprite to")]
        public Color FlickerColor = new Color32(255, 20, 20, 255);
        /// the list of material indexes we want to flicker on the target renderer. If left empty, will only target the material at index 0 
        [Tooltip("the list of material indexes we want to flicker on the target renderer. If left empty, will only target the material at index 0")]
        public int[] MaterialIndexes;

        /// the duration of this feedback is the duration of the flicker
        public override float FeedbackDuration { get { return ApplyTimeMultiplier(FlickerDuration); } set { FlickerDuration = value; } }

        protected Color[] _initialFlickerColors;
        protected int[] _propertyIDs;
        protected bool[] _propertiesFound;
        protected Coroutine[] _coroutines;

        /// <summary>
        /// On init we grab our initial color and components
        /// </summary>
        /// <param name="owner"></param>
        protected override void CustomInitialization(GameObject owner)
        {
            if (MaterialIndexes.Length == 0)
            {
                MaterialIndexes = new int[1];
                MaterialIndexes[0] = 0;
            }

            _coroutines = new Coroutine[MaterialIndexes.Length];

            _initialFlickerColors = new Color[MaterialIndexes.Length];
            _propertyIDs = new int[MaterialIndexes.Length];
            _propertiesFound = new bool[MaterialIndexes.Length];

            for (int i = 0; i < MaterialIndexes.Length; i++)
            {
                _propertiesFound[i] = false;

                if (Active && (BoundRenderer == null) && (owner != null))
                {
                    if (owner.MMFGetComponentNoAlloc<Renderer>() != null)
                    {
                        BoundRenderer = owner.GetComponent<Renderer>();
                    }
                    if (BoundRenderer == null)
                    {
                        BoundRenderer = owner.GetComponentInChildren<Renderer>();
                    }
                    if (BoundRenderer != null)
                    {
                        if (BoundRenderer.materials[i].HasProperty("_Color"))
                        {
                            _initialFlickerColors[i] = BoundRenderer.materials[i].color;
                        }
                    }
                }

                if (Active && (BoundRenderer != null))
                {
                    if (Mode == Modes.Color)
                    {
                        if (BoundRenderer.materials[i].HasProperty("_Color"))
                        {
                            _propertiesFound[i] = true;
                            _initialFlickerColors[i] = BoundRenderer.materials[i].color;
                        }
                    }
                    else
                    {
                        if (BoundRenderer.materials[i].HasProperty(PropertyName))
                        {
                            _propertyIDs[i] = Shader.PropertyToID(PropertyName);
                            _propertiesFound[i] = true;
                            _initialFlickerColors[i] = BoundRenderer.materials[i].GetColor(_propertyIDs[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// On play we make our renderer flicker
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
        {
            if (Active && (BoundRenderer != null))
            {
                for (int i = 0; i < MaterialIndexes.Length; i++)
                {
                    _coroutines[i] = StartCoroutine(Flicker(BoundRenderer, i, _initialFlickerColors[i], FlickerColor, FlickerOctave, FeedbackDuration));
                }
            }
        }

        /// <summary>
        /// On reset we make our renderer stop flickering
        /// </summary>
        protected override void CustomReset()
        {
            base.CustomReset();

            if (InCooldown)
            {
                return;
            }

            if (Active && (BoundRenderer != null))
            {
                for (int i = 0; i < MaterialIndexes.Length; i++)
                {
                    SetColor(i, _initialFlickerColors[i]);
                }
            }
        }

        public virtual IEnumerator Flicker(Renderer renderer, int materialIndex, Color initialColor, Color flickerColor, float flickerSpeed, float flickerDuration)
        {
            if (renderer == null)
            {
                yield break;
            }

            if (!_propertiesFound[materialIndex])
            {
                yield break;
            }

            if (initialColor == flickerColor)
            {
                yield break;
            }

            float flickerStop = FeedbackTime + flickerDuration;

            while (FeedbackTime < flickerStop)
            {
                SetColor(materialIndex, flickerColor);
                if (Timing.TimescaleMode == TimescaleModes.Scaled)
                {
                    yield return MMFeedbacksCoroutine.WaitFor(flickerSpeed);
                }
                else
                {
                    yield return MMFeedbacksCoroutine.WaitForUnscaled(flickerSpeed);
                }
                SetColor(materialIndex, initialColor);
                if (Timing.TimescaleMode == TimescaleModes.Scaled)
                {
                    yield return MMFeedbacksCoroutine.WaitFor(flickerSpeed);
                }
                else
                {
                    yield return MMFeedbacksCoroutine.WaitForUnscaled(flickerSpeed);
                }
            }

            SetColor(materialIndex, initialColor);
        }

        protected virtual void SetColor(int materialIndex, Color color)
        {
            if (Mode == Modes.Color)
            {
                if (BoundRenderer.materials[materialIndex].HasProperty("_Color"))
                {
                    BoundRenderer.materials[materialIndex].color = color;
                }
            }
            else
            {
                if (_propertiesFound[materialIndex])
                {
                    BoundRenderer.materials[materialIndex].SetColor(_propertyIDs[materialIndex], color);
                }
            }            
        }
        
        /// <summary>
        /// Stops this feedback
        /// </summary>
        /// <param name="position"></param>
        /// <param name="feedbacksIntensity"></param>
        protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            base.CustomStopFeedback(position, feedbacksIntensity);
            if (Active)
            {
                for (int i = 0; i < _coroutines.Length; i++)
                {
                    if (_coroutines[i] != null)
                    {
                        StopCoroutine(_coroutines[i]);    
                    }
                    _coroutines[i] = null;    
                }
            }
        }
    }
}
