using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration {

    public ProceduralTerrain terrainGenerator;

    // Reference to the World data
    private World world;

    // Contains all terrain objects
    private GameObject terrainContainer;

    // Contains all useable and interactable game objects
    private GameObject objectsContainer;

    public List<ResourceObject> resourceObjectPool;

    public WorldGeneration(World world) {
        this.world = world;
        this.terrainGenerator = new ProceduralTerrain((int)world.size.x, (int)world.size.y);

        this.objectsContainer = new GameObject("objects");

        this.resourceObjectPool = new List<ResourceObject>(world.resources.Count);
    }
        
    public void GenerateResources() {
        // Take the current chunk mesh and distribute resources

        // Create a real terrain 2D map and list of vertices as Vector3s
        for (int i = 0; i < world.resources.Count; i++) {
            int randomPos = Random.Range(1, terrainGenerator.vertices.Count);

            Resource resource = world.resources[i];
            ResourceObject resourceObj = new ResourceObject(resource, terrainGenerator.vertices[randomPos], objectsContainer);

            resourceObjectPool.Add(resourceObj);

            Debug.Log("Kind: " + resource.Kind + ", Size: " + resource.Size + ", Life: " + resource.Life);
        }
    }

    public void GenerateTerrain() {
        // Generate a chunk
        terrainContainer = terrainGenerator.Generate();
        // Translate terrain from data space to world space
        //   centered on (0,0,0)
        //terrainContainer.transform.position = new Vector3(-(world.size.x / 2), 0, -(world.size.y / 2));

        // Bump the terrain y-scale to make the terrain more interesting for now
        //Vector3 scale = terrainContainer.transform.localScale;
        //scale.y = 10.0f;
        //terrainContainer.transform.localScale = scale;
    }
}

