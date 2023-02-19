using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float sprintFOV = 80f;
    public float walkFOV = 60f;
    public float crouchSpeed = 2f;
    public float crouchColliderHeight = 0.5f;

    private CharacterController controller;
    private Camera cam;
    private float baseFOV;
    private bool isSprinting = false;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        baseFOV = cam.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }

        if (isSprinting)
        {
            move *= sprintSpeed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, Time.deltaTime * 8f);
        }
        else
        {
            move *= moveSpeed;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, walkFOV, Time.deltaTime * 8f);
        }

        if (isCrouching)
        {
            move *= crouchSpeed;
            controller.height = crouchColliderHeight;
        }
        else
        {
            controller.height = 2f;
        }

        controller.Move(move * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        float rotX = Input.GetAxis("Mouse X") * 2f;
        float rotY = Input.GetAxis("Mouse Y") * 2f;
        transform.Rotate(Vector3.up, rotX);
        cam.transform.Rotate(Vector3.left, rotY);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
