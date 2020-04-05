using UnityEngine;

public class DrawDirectionGizmo : MonoBehaviour {

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 15;
        Gizmos.DrawRay(transform.position, direction);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(Camera.main.aspect, 1.0f, 1.0f));
        Gizmos.DrawFrustum(Vector3.zero, Camera.main.fieldOfView, 20, Camera.main.nearClipPlane, 1.0f);
    }
}
