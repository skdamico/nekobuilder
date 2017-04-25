using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    Tile[,] tiles;

    int width;
    public int Width {
        get {
            return width;
        }
    }

    int depth;
    public int Depth {
        get {
            return depth;
        }
    }

    public World(int width = 50, int depth = 50) {
        this.width = width;
        this.depth = depth;

        tiles = new Tile[width, depth];

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                tiles[x, z] = new Tile(this, new Vector3(x, 0, z));
            }
        }

        Debug.Log("World created with width: " + width + ", depth: " + depth);
    }

    public Tile GetTileAt(Vector3 location) {
        if (location.x > width || location.z > depth) {
            Debug.LogError("Tile range out of bounds");
            return null;
        }

        return tiles[(int)location.x, (int)location.z];
    }

    // Testing

    public void RandomizeTiles() {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < depth; z++) {
                if (Random.Range(0, 2) == 0) {
                    tiles[x, z].Type = Tile.TileType.Empty;
                }
                else {
                    tiles[x, z].Type = Tile.TileType.Ground;
                }
            }
        }
    }
}
