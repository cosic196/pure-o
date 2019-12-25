using UnityEngine;

public class CameraMovementController : MonoBehaviour {

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform _crosshairTransform;
    [SerializeField]
    private float _cameraMaxMovement;
    private Vector3 _helperVector, _cameraInvertedRotation;
    private Transform _cameraTransform;

    void Start () {
        _cameraTransform = _camera.gameObject.GetComponent<Transform>();
        _cameraInvertedRotation = Vector3.zero;
        _helperVector = new Vector3(0.5f, 0.5f);
	}
	
	void Update () {
        _cameraInvertedRotation = (_camera.ScreenToViewportPoint(_crosshairTransform.position) - _helperVector) * _cameraMaxMovement;
        _cameraTransform.localEulerAngles = new Vector3(-_cameraInvertedRotation.y, _cameraInvertedRotation.x);
    }
}