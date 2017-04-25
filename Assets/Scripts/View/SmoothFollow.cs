using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothTime = 0.5f;

    private Vector3 defaultDistance;
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        defaultDistance = transform.position;
    }

    private void Update() {
        Vector3 targetPosition = target.position + defaultDistance;
        transform.position = Vector3.SmoothDamp(transform.position,
                                                targetPosition, 
                                                ref velocity, 
                                                smoothTime);
        transform.LookAt(target.position);
	}
}
