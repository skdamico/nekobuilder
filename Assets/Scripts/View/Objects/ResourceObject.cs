using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObject {

    GameObject view;

    // Reference to the underlying model
    Resource resourceData;
	
    public ResourceObject(Resource resourceData, Vector3 position, GameObject parent) {
        this.resourceData = resourceData;

        view = Resources.Load<GameObject>("Prefabs/ResourcePrefab");

        MonoBehaviour.Instantiate(view, position, Quaternion.identity, parent.transform);
    }
}
