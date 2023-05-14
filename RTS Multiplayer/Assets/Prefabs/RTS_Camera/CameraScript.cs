using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CameraScript : MonoBehaviour
{   
    Camera cam;
    public float polarAngle = 45f;

    public float radialDistance = 20f;

    public float movementTime = 0;
    public float movementSpeed = 0;
    public float rotationSpeed = 0;
    [SerializeField] int border = 5;

    public Vector3 newPosition;
    public float zoomAmount = 5;
    public Vector3 newZoom;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        cam = Camera.main;
        newZoom = cam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(InputManager.Instance.currentState != Selection.BUILDING)
        {
            cam.transform.position = getNewCameraPosition();
            cam.transform.LookAt(transform.position);

            // moving camera
            if (!Input.GetMouseButton(2)) // check if not rotating camera with MMB
            {
                MoveCamera(border);
            }

            // rotating camera
            if (Input.GetMouseButtonDown(2))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetMouseButton(2)) // MMB
            {
                // horizontally
                if (Input.GetAxis("Mouse X") != 0)
                {
                    transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * rotationSpeed, 0));
                }

                // vertically
                if (Input.GetAxis("Mouse Y") != 0)
                {
                    polarAngle = Mathf.Clamp(polarAngle + Input.GetAxis("Mouse Y") * rotationSpeed, 0, 89.9f);
                }
            }

            if (Input.GetMouseButtonUp(2))
            {
                Cursor.lockState = CursorLockMode.Confined;
            }

            // changing distance (zoom)
            if (Input.mouseScrollDelta.y != 0)
            {
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - Input.mouseScrollDelta.y * zoomAmount, 10, 60);
            }
        }
        

    }

    private Vector3 getNewCameraPosition()
    {
        float h = radialDistance * Mathf.Sin(polarAngle * Mathf.Deg2Rad);
        float x = Mathf.Sqrt(Mathf.Pow(radialDistance, 2) - Mathf.Pow(h, 2));

        return transform.position - transform.forward * x + Vector3.up * h;
    }

    private void MoveCamera(int border)
    {
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.y >= Screen.height - border || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // up edge detected
        {
            newPosition += (transform.forward * movementSpeed);
        }

        if (mousePosition.y <= border || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // down edge detected
        {
            newPosition += (transform.forward * -movementSpeed);
        }

        if (mousePosition.x <= border || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // left edge detected
        {
            newPosition += (transform.right * -movementSpeed);
        }

        if (mousePosition.x >= Screen.width - border || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // right edge detected
        {
            newPosition += (transform.right * movementSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

    }
}