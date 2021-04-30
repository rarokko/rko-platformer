using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float moveSpeed = 10f;
    public Vector3 offset;

    private Vector2 movement;
    Vector3 forward, right;

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * movement.x;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * movement.y;
    }

    void InitializeMovement()
    {
        forward = UnityEngine.Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
}
