using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    [SerializeField]
    Vector2 worldSize = new Vector2(50, 50);

    World world;
	
	void Start() {
        // Generate world data
        // TODO:
        //  - Create 2D voronoi map
        //  - Store 'cells' as objects
        //  - Generate heightmap
        //  - Generate resources and prop data
        world = new World(worldSize);

        // Generate resources
        WorldGeneration worldGen = new WorldGeneration(world);
        worldGen.GenerateTerrain();
        worldGen.GenerateResources();
	}
}
