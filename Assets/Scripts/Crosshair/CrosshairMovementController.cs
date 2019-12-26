using UnityEngine;

public class CrosshairMovementController : MonoBehaviour
{
    private Transform _transform;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = Input.mousePosition;
    }
}