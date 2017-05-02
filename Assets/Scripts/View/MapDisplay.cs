using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var texture = MapGenerator.GenerateMapTexture();
        Debug.Log("Texture size: " + texture.width);

        GetComponent<Renderer>().material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
