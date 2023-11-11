using UnityEngine;

public class CameraScript : MonoBehaviour
{   
    Camera cam;
    public float polarAngle = 45f;

    public float radialDistance = 20f;

    [Header("Camera Movement")]
    public float movementSpeed = 0.1f;
    public float movementMultiplayer = 1;
    public float rotationSpeed = 2;
    public float movementTime = 5;
    [Header("Camera Border")]
    [SerializeField] bool isBorderActive = false;
    [SerializeField] int left = 1;
    [SerializeField] int right = 1;
    [SerializeField] int top = 1;
    [SerializeField] int bottom = 1;

    [Header("Camera Zoom")]
    [SerializeField] float minZoomDistance = 3;
    [SerializeField] float maxZoomDistance = 20;
    [SerializeField] float zoomAmount = 5;

    Vector3 newPosition;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        startPosition = transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = getNewCameraPosition();
        cam.transform.LookAt(transform.position);

        // moving camera
        //if (!Input.GetMouseButton(2)) // check if not rotating camera with MMB (optional)
        //{
            MoveCamera();
        //}

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
            radialDistance = Mathf.Clamp(radialDistance - Input.mouseScrollDelta.y * zoomAmount, minZoomDistance, maxZoomDistance);
        }

        //porót do pozycji startowej
        if (Input.GetKeyDown(KeyCode.C))
        {
            newPosition = startPosition;
        }
    }

    private Vector3 getNewCameraPosition()
    {
        float h = radialDistance * Mathf.Sin(polarAngle * Mathf.Deg2Rad);
        float x = Mathf.Sqrt(Mathf.Pow(radialDistance, 2) - Mathf.Pow(h, 2));

        return transform.position - transform.forward * x + Vector3.up * h;
    }

    private void MoveCamera()
    {
        Vector3 mousePosition = Input.mousePosition;
        float multiplayedSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            multiplayedSpeed = movementSpeed * movementMultiplayer;
        }
        else
        {
            multiplayedSpeed = movementSpeed;
        }

        if ((mousePosition.y >= Screen.height - top && isBorderActive) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // up edge detected
        {
            newPosition += (transform.forward * multiplayedSpeed);
        }

        if ((mousePosition.y <= bottom && isBorderActive) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // down edge detected
        {
            newPosition += (transform.forward * -multiplayedSpeed);
        }

        if ((mousePosition.x <= left && isBorderActive) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // left edge detected
        {
            newPosition += (transform.right * -multiplayedSpeed);
        }

        if ((mousePosition.x >= Screen.width - right && isBorderActive) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // right edge detected
        {
            newPosition += (transform.right * multiplayedSpeed);
        }

        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
}
