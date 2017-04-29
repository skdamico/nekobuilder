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
        view = MonoBehaviour.Instantiate(view, position, Quaternion.identity, parent.transform);

        SetupView();
    }


    public void SetupView() {

        // Set color of mesh based on Kind
        MeshRenderer renderer = view.GetComponent<MeshRenderer>(); 

        Material material;
        switch(resourceData.Kind) {
            case (Resource.ResourceKind.Build):
                material = Resources.Load<Material>("Material/ResourceBuildMaterial");
                break;
            case (Resource.ResourceKind.Fuel):
                material = Resources.Load<Material>("Material/ResourceFuelMaterial");
                break;
            case (Resource.ResourceKind.Science):
                material = Resources.Load<Material>("Material/ResourceScienceMaterial");
                break;
            default:
                material = Resources.Load<Material>("Material/ErrorMaterial");
                Debug.LogError("Invalid data for Resource.Kind");
                break;
        }
        renderer.material = material;

        // Set size of mesh based on Life
        UpdateSize();
    }

    public void UpdateView() {
        UpdateSize();
    }

    public void UpdateSize() {
        // Do we show the resource?
        view.SetActive(resourceData.Life > 0);

        // Size: small == 50.0f Life
        float magnitude = resourceData.Life / 50.0f;
        Vector3 scale = view.transform.localScale;
        view.transform.localScale = scale * magnitude;
    }
}
