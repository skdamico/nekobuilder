using UnityEngine;
using System.Collections.Generic;
using TriangleNet.Geometry;

public class HeightMap {
    public enum HeightMapType { Perlin }


    public static List<float> GenerateHeightMap(int width, int height, ICollection<Vertex> vertices, HeightMapType type) {
        List<float> elevations = new List<float>();

        if (type == HeightMapType.Perlin) {
            // Setup Perlin Noise Parameters
            float sampleSize = 0.1f; // 1.0f default
            int octaves = 6;
            float frequencyBase = 2;
            float persistence = 1.1f;

            float[] seed = new float[octaves];

            for (int i = 0; i < octaves; i++) {
                seed[i] = Random.Range(0.0f, 100.0f);
            }

            // Sample perlin noise to get elevations
            foreach (Vertex vert in vertices) {
                float elevation = 0.0f;
                float amplitude = Mathf.Pow(persistence, octaves);
                float frequency = 1.0f;
                float maxVal = 0.0f;

                for (int o = 0; o < octaves; o++) {
                    float sample = (Mathf.PerlinNoise(seed[o] + (float)vert.x * sampleSize / (float)width * frequency,
                                                      seed[o] + (float)vert.y * sampleSize / (float)height * frequency) - 0.5f) * amplitude;
                    elevation += sample;
                    maxVal += amplitude;
                    amplitude /= persistence;
                    frequency *= frequencyBase;
                }

                elevation = elevation / maxVal;
                elevations.Add(elevation);
            }
        }

        return elevations;
    }
}
