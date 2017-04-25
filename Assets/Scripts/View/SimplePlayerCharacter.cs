using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]

public class SimplePlayerCharacter : MonoBehaviour {

    [SerializeField]
    private float movePower = 6.0f;
    [SerializeField]
    private float maxVelocityChange = 6.0f;
    [SerializeField]
    private float gravity = 10.0f;
    [SerializeField]
    private float rotateSpeed = 0.05f;
    
    private Rigidbody rigidBody;
    private Transform body;
    private bool isGrounded;


    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.useGravity = false;

        body = transform.Find("body");
    }


    /*
     * ACTIONS
     */
    public void Move(Vector3 moveDirection) {
        // Lock velocity to maxVelocity
        Vector3 targetVelocity = moveDirection * movePower;
        targetVelocity = Vector3.ClampMagnitude(targetVelocity, maxVelocityChange);

        // Apply force
        Vector3 velocity = rigidBody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);

        // Manually apply gravity
        Vector3 gravityVector = new Vector3(0, -gravity * rigidBody.mass, 0);
        rigidBody.AddForce(gravityVector);
	}

    public void Gather() {
        // Is the object that is being moused over a "resource"?
        
        // Do one cycle of gather (beam, particles, damage)
    }

    public void RotateTowards(Vector3 direction) {
        Plane playerPlane = new Plane(Vector3.up, body.position);

        Ray ray = Camera.main.ScreenPointToRay(direction);

        float hitDist = 0.0f;
        if (playerPlane.Raycast(ray, out hitDist)) {
            Vector3 targetPoint = ray.GetPoint(hitDist);

            // Get look rotation from mouse to body
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - body.position);

            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, rotateSpeed);
        }
    }

    /*
    private void CheckGroundStatus() {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * groundRayLength));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        isGrounded = Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundRayLength);
        if (isGrounded) {
            groundNormal = hitInfo.normal;
        }
        else {
            groundNormal = Vector3.up;
        }
    }
    */
}
