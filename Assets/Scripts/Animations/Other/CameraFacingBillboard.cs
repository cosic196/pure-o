using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    private void Start()
    {
        if(_camera == null)
        {
            _camera = Camera.main;
        }
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
            _camera.transform.rotation * Vector3.up);
    }
}