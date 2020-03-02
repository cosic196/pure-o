using UnityEngine;

[AddComponentMenu("Camera/Simple Smooth Mouse Look ")]
public class FpsCameraMovementController : MonoBehaviour
{
    private Transform _transform;

    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor;
    public Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 smoothing = new Vector2(3, 3);
    public Vector2 targetDirection;
    public Vector2 targetCharacterDirection;

    // Assign this if there's a parent object controlling motion, such as a Character Controller.
    // Yaw rotation will affect this object instead of the camera if set.
    public GameObject characterBody;

    void Start()
    {
        _transform = GetComponent<Transform>();
#if UNITY_ANDROID || UNITY_IOS
        Input.gyro.enabled = true;
#endif
        // Set sensitivity from player prefs
        SetSensitivity();
        EventManager.StartListening("SensitivityChanged", SetSensitivity);

        // Set target direction to the camera's initial orientation.
        targetDirection = _transform.localRotation.eulerAngles;

        // Set target direction for the character body to its inital state.
        if (characterBody)
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;
    }

    private void SetSensitivity()
    {
        float prefSensitivity = PlayerPrefs.GetFloat("Sensitivity");
        if(prefSensitivity == 0)
        {
            prefSensitivity = 0.5f;
        }
        sensitivity = new Vector2(prefSensitivity, prefSensitivity);
    }

    void Update()
    {
#if UNITY_ANDROID || UNITY_IOS
        _transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y * sensitivity.y, 0);
        _transform.Rotate(-Input.gyro.rotationRateUnbiased.x * sensitivity.x, 0, 0);
        _transform.localEulerAngles = new Vector3(_transform.localEulerAngles.x, _transform.localEulerAngles.y, 0f);
        return;
#endif
        // Ensure the cursor is always locked when set
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;

        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // Then clamp and apply the global y value.
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        _transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;

        // If there's a character body that acts as a parent to the camera
        if (characterBody)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            characterBody.transform.localRotation = yRotation * targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, _transform.InverseTransformDirection(Vector3.up));
            _transform.localRotation *= yRotation;
        }
    }
}