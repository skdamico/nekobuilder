using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    World world;

	// Use this for initialization
	void Start () {
		world = new World();

        /*
        // Create GameObjects for each tile
        for (int x = 0; x < world.Width; x++) {
            for (int z = 0; z < world.Depth; z++) {
                Tile tileModel = world.GetTileAt(new Vector3(x, 0, z));

                GameObject tileObj = ProceduralGeometry.CreatePlane();
                tileObj.name = "tile_" + x + "_" + z;
                tileObj.transform.position = tileModel.Location;
                tileObj.transform.SetParent(this.transform, true);
                // TODO:
                // Add a material
                // Add a mesh collider

                tileModel.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tileObj); });
            }
        }

        // TODO: bake all mesh tiles into one for draw?
        world.RandomizeTiles();
        */

        // Generate the terrain
        ProceduralTerrain gen = new ProceduralTerrain(world.Width, world.Depth);
        GameObject terrain = gen.Generate();

        // translate the terrain over to 0, 0, 0
        Vector3 worldSize = new Vector3(world.Width, 0, world.Depth);
        terrain.transform.Translate(-(worldSize / 2));
	}

	// Update is called once per frame
	void Update () {

	}

    void OnTileTypeChanged(Tile tile, GameObject tileObj) {

        if (tile.Type == Tile.TileType.Ground) {
            tileObj.GetComponent<MeshRenderer>().enabled = true;
        }
        else if (tile.Type == Tile.TileType.Empty) {
            tileObj.GetComponent<MeshRenderer>().enabled = false;
        }
        else {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type");
        }
    }
}
