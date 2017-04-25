using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeometry {
    
    static public Mesh CreatePlaneMesh() {

        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] tris = new int[6] { 0, 1, 2, 2, 1, 3 };

        // Vertix layout
        // 0 ----- 1
        // |    /  |
        // |   /   |
        // |  /    |
        // 2 ----- 3

        verts[0] = -Vector3.right + Vector3.forward;
        verts[1] = Vector3.right + Vector3.forward;
        verts[2] = -Vector3.right - Vector3.forward;
        verts[3] = Vector3.right - Vector3.forward;

        uvs[0] = new Vector2(0.0f, 1.0f);
        uvs[1] = new Vector2(1.0f, 1.0f);
        uvs[2] = new Vector2(0.0f, 0.0f);
        uvs[3] = new Vector2(1.0f, 0.0f);

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;

        mesh.RecalculateNormals();

        return mesh;
    }

    static public GameObject CreatePlane() {
        GameObject plane = new GameObject();

        MeshRenderer renderer = plane.AddComponent<MeshRenderer>();
        MeshFilter filter = plane.AddComponent<MeshFilter>();
        filter.mesh = CreatePlaneMesh();

        return plane;
    }
}
