using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile {

    public enum TileType { Empty, Ground }

    private TileType _type = TileType.Empty;
    public TileType Type {
        get { return _type; }
        set {
            TileType oldType = _type;

            if (value != oldType) {
                _type = value;

                if (tileTypeChangedCallback != null) {
                    tileTypeChangedCallback(this);
                }
            }
        }
    }

    LooseObject looseObject;
    InstalledObject installedObject;

    // World context and such
    World world;
    public Vector3 Location { get; protected set; }

    private Action<Tile> tileTypeChangedCallback;

    public Tile(World world, Vector3 location) {
        this.world = world;
        this.Location = location;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
        tileTypeChangedCallback += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback) {
        tileTypeChangedCallback -= callback;
    }
}
