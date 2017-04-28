using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TriangleNet.Geometry;
using TriangleNet.Topology;

public class ProceduralTerrain {

    // Maximum size of the terrain.
    int width = 50;
    int depth = 50;

    // Random points to generate in the terrain
    int randomPoints = 1000;

    // Triangles in each chunk.
    int trianglesInChunk = 20000;

    // Elevations at each point in the mesh
    List<float> elevations;
    public List<Vector3> vertices;

    // The delaunay mesh
    private TriangleNet.Mesh mesh = null;
 
    public ProceduralTerrain(int width = 50, int depth = 50, int randomPoints = 1000, int trianglesInChunk = 20000) {
        this.width = width;
        this.depth = depth;
        this.randomPoints = randomPoints;
        this.trianglesInChunk = trianglesInChunk;
    }

    public virtual GameObject Generate() {
        //UnityEngine.Random.InitState(0);

        elevations = new List<float>();

        // Use Triangle.NET geometry classes
        Polygon polygon = new Polygon();
        for (int i = 0; i < randomPoints; i++) {
            polygon.Add(new Vertex(Random.Range(0.0f, width), Random.Range(0.0f, depth)));
        }

        TriangleNet.Meshing.ConstraintOptions options = new TriangleNet.Meshing.ConstraintOptions() { ConformingDelaunay = true };
        mesh = (TriangleNet.Mesh)polygon.Triangulate(options);

        elevations = HeightMap.GenerateHeightMap(width, depth, mesh.Vertices,  HeightMap.HeightMapType.Perlin);

        return TranslateMeshToGameObject();
    }

    /*
     * TriangleNet.Mesh -> UnityEngine.Mesh
     */
    private GameObject TranslateMeshToGameObject() {

        // Empty game object to hold the terrain chunks
        GameObject terrain = new GameObject("terrain");
        Material sharedMaterial = Resources.Load<Material>("Material/TerrainMaterial");

        IEnumerator<Triangle> triangleEnumerator = mesh.Triangles.GetEnumerator();

        for (int chunkStart = 0; chunkStart < mesh.Triangles.Count; chunkStart += trianglesInChunk) {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector2> uvs = new List<Vector2>();
            List<int> triangles = new List<int>();

            int chunkEnd = chunkStart + trianglesInChunk;
            for (int i = chunkStart; i < chunkEnd; i++) {
                if (!triangleEnumerator.MoveNext()) {
                    break;
                }

                Triangle triangle = triangleEnumerator.Current;

                // For the triangles to be right-side up, they need
                // to be wound in the opposite direction
                Vector3 v0 = GetPoint3D(triangle.vertices[2].id);
                Vector3 v1 = GetPoint3D(triangle.vertices[1].id);
                Vector3 v2 = GetPoint3D(triangle.vertices[0].id);

                triangles.Add(vertices.Count);
                triangles.Add(vertices.Count + 1);
                triangles.Add(vertices.Count + 2);

                vertices.Add(v0);
                vertices.Add(v1);
                vertices.Add(v2);

                Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0);
                normals.Add(normal);
                normals.Add(normal);
                normals.Add(normal);

                uvs.Add(new Vector2(0.0f, 0.0f));
                uvs.Add(new Vector2(0.0f, 0.0f));
                uvs.Add(new Vector2(0.0f, 0.0f));
            }

            Mesh chunkMesh = new Mesh();
            chunkMesh.vertices = vertices.ToArray();
            chunkMesh.uv = uvs.ToArray();
            chunkMesh.triangles = triangles.ToArray();
            chunkMesh.normals = normals.ToArray();

            GameObject chunk = new GameObject("chunk");
            chunk.AddComponent<MeshFilter>().mesh = chunkMesh;
            chunk.AddComponent<MeshRenderer>().sharedMaterial = sharedMaterial;
            chunk.AddComponent<MeshCollider>().sharedMesh = chunkMesh;


            // Adjust height of terrain
            chunk.transform.position = terrain.transform.position;
            chunk.transform.rotation = terrain.transform.rotation;
            chunk.transform.parent = terrain.transform;

            // Save references to vertex data
            this.vertices = vertices;
        }

        return terrain;
    }


    // Equivalent to calling new Vector3(GetPointLocation(i).x, GetElevation(i), GetPointLocation(i).y)
    public Vector3 GetPoint3D(int index) {
        Vertex vertex = mesh.vertices[index];
        float elevation = elevations[index];
        return new Vector3((float)vertex.x, elevation, (float)vertex.y);
    }
}
