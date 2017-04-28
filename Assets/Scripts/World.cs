using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    public Vector2 size;
    public List<Resource> resources;


    public World(Vector2 size) {
        this.size = size;

        // Generate resources
        resources = new List<Resource>();
        for (int i = 0; i < Random.Range(10, 20); i++) {
            Resource resource = new Resource((Resource.ResourceKind)Random.Range(0, 3), 
                                             (Resource.ResourceSize)Random.Range(0, 3));
            resources.Add(resource);
        }

        Debug.Log("World created with width: " + size.x + ", depth: " + size.y);
    }
}
