using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Dreamteck.Splines
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [AddComponentMenu("Dreamteck/Splines/Users/Spline Mesh")]
    public partial class SplineMesh : MeshGenerator
    {
        //Mesh data
        [SerializeField]
        [HideInInspector]
        private List<Channel> channels = new List<Channel>();
        private bool useLastResult = false;
        private List<TS_Mesh> combineMeshes = new List<TS_Mesh>();

        Matrix4x4 vertexMatrix = new Matrix4x4();
        Matrix4x4 normalMatrix = new Matrix4x4();
        SplineSample lastResult = new SplineSample(), modifiedResult = new SplineSample();

#if UNITY_EDITOR
        public override void EditorAwake()
        {
            for (int i = 0; i < channels.Count; i++)
            {
                for (int j = 0; j < channels[i].GetMeshCount(); j++) channels[i].GetMesh(j).Refresh();
            }
            base.EditorAwake();
        }
#endif

        protected override void Reset()
        {
            base.Reset();
            AddChannel("Channel 1");
        }

        public void RemoveChannel(int index)
        {
            channels.RemoveAt(index);
            Rebuild();
        }

        public void SwapChannels(int a, int b)
        {
            if (a < 0 || a >= channels.Count || b < 0 || b >= channels.Count) return;
            Channel temp = channels[b];
            channels[b] = channels[a];
            channels[a] = temp;
            Rebuild();
        }

        public Channel AddChannel(Mesh inputMesh, string name)
        {
            Channel channel = new Channel(name, inputMesh, this);
            channels.Add(channel);
            return channel;
        }

        public Channel AddChannel(string name)
        {
            Channel channel = new Channel(name, this);
            channels.Add(channel);
            return channel;
        }

        public int GetChannelCount()
        {
            return channels.Count;
        }

        public Channel GetChannel(int index)
        {
            return channels[index];
        }


        protected override void BuildMesh()
        {
            base.BuildMesh();
            Generate();
        }

        private void Generate()
        {
            int meshCount = 0;
            for (int i = 0; i < channels.Count; i++)
            {
                if (channels[i].GetMeshCount() == 0) continue;

                if (channels[i].autoCount)
                {
                    float avgBounds = 0f;
                    for (int j = 0; j < channels[i].GetMeshCount(); j++)
                    {
                        avgBounds += channels[i].GetMesh(j).bounds.size.z;
                    }

                    if (channels[i].GetMeshCount() > 1)
                    {
                        avgBounds /= channels[i].GetMeshCount();
                    }

                    if (avgBounds > 0f)
                    {
                        float length = CalculateLength(channels[i].clipFrom, channels[i].clipTo);
                        int newCount = Mathf.RoundToInt(length / avgBounds);
                        if (newCount < 1)
                        {
                            newCount = 1;
                        }
                        channels[i].count = newCount;
                    }
                }

                meshCount += channels[i].count;
            }

            if(meshCount == 0)
            {
                tsMesh.Clear();
                return;
            }

            if (combineMeshes.Count < meshCount)
            {
                combineMeshes.AddRange(new TS_Mesh[meshCount - combineMeshes.Count]);
            }
            else if (combineMeshes.Count > meshCount)
            {
                combineMeshes.RemoveRange((combineMeshes.Count - 1) - (combineMeshes.Count - meshCount), combineMeshes.Count - meshCount);
            }

            int combineMeshIndex = 0;
            for (int i = 0; i < channels.Count; i++)
            {
                if (channels[i].GetMeshCount() == 0) continue;
                channels[i].ResetIteration();
                useLastResult = false;
                double step = 1.0 / channels[i].count;
                double space = step * channels[i].spacing * 0.5;
                
                switch (channels[i].type)
                {
                    case Channel.Type.Extrude:
                        for (int j = 0; j < channels[i].count; j++)
                        {
                            double from = DMath.Lerp(channels[i].clipFrom, channels[i].clipTo, j * step + space);
                            double to = DMath.Lerp(channels[i].clipFrom, channels[i].clipTo, j * step + step - space);
                            if (combineMeshes[combineMeshIndex] == null)
                            {
                                combineMeshes[combineMeshIndex] = new TS_Mesh();
                            }
                            Extrude(channels[i], combineMeshes[combineMeshIndex], from, to);
                            combineMeshIndex++;
                        }
                        if (space == 0f) useLastResult = true;
                        break;
                    case Channel.Type.Place:
                        for (int j = 0; j < channels[i].count; j++)
                        {
                            if (combineMeshes[combineMeshIndex] == null)
                            {
                                combineMeshes[combineMeshIndex] = new TS_Mesh();
                            }
                            Place(channels[i], combineMeshes[combineMeshIndex], DMath.Lerp(channels[i].clipFrom, channels[i].clipTo, (double)j / Mathf.Max(channels[i].count - 1, 1)));
                            combineMeshIndex++;
                        }
                        break;
                   
                }
            }
            if (tsMesh == null) tsMesh = new TS_Mesh();
            else tsMesh.Clear();
            tsMesh.Combine(combineMeshes, false);
        }

        private void Place(Channel channel, TS_Mesh target, double percent)
        {
            Channel.MeshDefinition definition = channel.NextMesh();
            if (target == null) target = new TS_Mesh();
            definition.Write(target, channel.overrideMaterialID ? channel.targetMaterialID : -1);
            Vector2 channelOffset = channel.NextRandomOffset();
            Quaternion channelRotation = channel.NextRandomQuaternion();

            var customValues = channel.GetCustomPlaceValues(percent);

            Vector2 finalOffset = channelOffset + customValues.Item1 + new Vector2(offset.x, offset.y);
            Quaternion finalRotation = channelRotation * Quaternion.AngleAxis(rotation, Vector3.forward) * customValues.Item2;
            Vector3 finalScale = channel.NextPlaceScale();

            Evaluate(percent, evalResult);
            ModifySample(evalResult);
            Vector3 originalNormal = evalResult.up;
            Vector3 originalRight = evalResult.right;
            Vector3 originalDirection = evalResult.forward;
            if (channel.overrideNormal)
            {
                evalResult.forward = Vector3.Cross(evalResult.right, channel.customNormal);
                evalResult.up = channel.customNormal;
            }

            Vector3 scaleMod = channel.scaleModifier.GetScale(evalResult);
            finalScale.x *= customValues.Item3.x * scaleMod.x;
            finalScale.y *= customValues.Item3.y * scaleMod.y;
            finalScale.z *= customValues.Item3.z * scaleMod.z;

            float resultSize = GetBaseSize(evalResult);
            vertexMatrix.SetTRS(evalResult.position + originalRight * (finalOffset.x * resultSize) + originalNormal * (finalOffset.y * resultSize) + originalDirection * offset.z, //Position
                evalResult.rotation * finalRotation, //Rotation
                finalScale * resultSize ); //Scale
            normalMatrix = vertexMatrix.inverse.transpose;

            for (int i = 0; i < target.vertexCount; i++)
            {
                target.vertices[i] = vertexMatrix.MultiplyPoint3x4(definition.vertices[i]);
                target.normals[i] = normalMatrix.MultiplyVector(definition.normals[i]);
            }
            for (int i = 0; i < Mathf.Min(target.colors.Length, definition.colors.Length); i++)
            {
                target.colors[i] = definition.colors[i] * evalResult.color * color;
            }
        }

        private void Extrude(Channel channel, TS_Mesh target, double from, double to)
        {
            Channel.MeshDefinition definition = channel.NextMesh();
            if (target == null) target = new TS_Mesh();
            definition.Write(target, channel.overrideMaterialID ? channel.targetMaterialID : -1);
            Vector2 uv = Vector2.zero;
            Vector3 trsVector = Vector3.zero;

            Vector3 channelOffset = channel.NextRandomOffset();
            Vector3 channelScale = channel.NextRandomScale();
            float channelRotation = channel.NextRandomAngle();

            for (int i = 0; i < definition.vertexGroups.Count; i++)
            {
                if (useLastResult && i == definition.vertexGroups.Count) evalResult = lastResult;
                else Evaluate(DMath.Lerp(from, to, definition.vertexGroups[i].percent), evalResult);
                ModifySample(evalResult, modifiedResult);
                Vector3 originalNormal = modifiedResult.up;
                Vector3 originalRight = modifiedResult.right;
                Vector3 originalDirection = modifiedResult.forward;
                if (channel.overrideNormal)
                {
                    modifiedResult.forward = Vector3.Cross(modifiedResult.right, channel.customNormal);
                    modifiedResult.up = channel.customNormal;
                }
                var customValues = channel.GetCustomExtrudeValues(modifiedResult.percent);
                Vector3 finalOffset = offset + channelOffset + (Vector3)customValues.Item1;
                float finalRotation = rotation + channelRotation + customValues.Item2;
                Vector3 finalScale = channelScale;
                Vector2 scaleMod = channel.scaleModifier.GetScale(modifiedResult);
                finalScale.x *= customValues.Item3.x * scaleMod.x;
                finalScale.y *= customValues.Item3.y * scaleMod.y;
                finalScale.z = 1f;
                float resultSize = modifiedResult.size;
                vertexMatrix.SetTRS(modifiedResult.position + originalRight * (finalOffset.x * resultSize) + originalNormal * (finalOffset.y * resultSize) + originalDirection * offset.z, //Position
                    modifiedResult.rotation * Quaternion.AngleAxis(finalRotation, Vector3.forward), //Rotation
                    finalScale * resultSize); //Scale
                normalMatrix = vertexMatrix.inverse.transpose;
                if (i == 0) lastResult.CopyFrom(evalResult);

                for (int n = 0; n < definition.vertexGroups[i].ids.Length; n++)
                {
                    int index = definition.vertexGroups[i].ids[n];
                    trsVector = definition.vertices[index];
                    trsVector.z = 0f;
                    target.vertices[index] = vertexMatrix.MultiplyPoint3x4(trsVector);
                    trsVector = definition.normals[index];
                    target.normals[index] = normalMatrix.MultiplyVector(trsVector);
                    target.colors[index] = target.colors[index] * modifiedResult.color * color;
                    if (target.uv.Length > index)
                    {
                        uv = target.uv[index];
                        switch (channel.overrideUVs)
                        {
                            case Channel.UVOverride.ClampU: uv.x = (float)modifiedResult.percent; break;
                            case Channel.UVOverride.ClampV: uv.y = (float)modifiedResult.percent; break;
                            case Channel.UVOverride.UniformU: uv.x = CalculateLength(0.0, ClipPercent(modifiedResult.percent)); break;
                            case Channel.UVOverride.UniformV: uv.y = CalculateLength(0.0, ClipPercent(modifiedResult.percent)); break;
                        }
                        target.uv[index] = new Vector2(uv.x * uvScale.x * channel.uvScale.x, uv.y * uvScale.y * channel.uvScale.y);
                        target.uv[index] += uvOffset + channel.uvOffset;
                    }
                }
            }
        }

        protected override void CreateMesh()
        {
            base.CreateMesh();
            mesh.name = "Spline Mesh";
        }
    }
}
