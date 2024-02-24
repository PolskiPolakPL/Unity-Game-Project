using UnityEngine;

public class SimpleRotatingCamera : MonoBehaviour
{
    [SerializeField] float radialDistance;
    [SerializeField] float polarAngle;
    [SerializeField] float rotationSpeed;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.position = getNewCameraPosition();
        cam.transform.LookAt(transform.position);
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

    }
    private Vector3 getNewCameraPosition()
    {
        float h = radialDistance * Mathf.Sin(polarAngle * Mathf.Deg2Rad);
        float x = Mathf.Sqrt(Mathf.Pow(radialDistance, 2) - Mathf.Pow(h, 2));

        return transform.position - transform.forward * x + Vector3.up * h;
    }
}
