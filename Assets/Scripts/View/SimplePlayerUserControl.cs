using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerUserControl : MonoBehaviour {

    private SimplePlayerCharacter player;
    private Transform cam;
    private Vector3 currentForward;
    private Vector3 move;

	// Use this for initialization
	private void Awake() {
		if (Camera.main == null) {
            Debug.LogWarning("Warning: no main camera found.");
        }

        cam = Camera.main.transform;
        player = GetComponent<SimplePlayerCharacter>();
	}

    private void Update() {
        // Check for `gather`
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // setup camera relative direction
        currentForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        move = (v * currentForward + h * cam.right).normalized;

        player.Move(move);
        player.RotateTowards(Input.mousePosition);
    }

    private void FixedUpdate() {
    }
}
