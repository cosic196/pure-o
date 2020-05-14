using UnityEngine;

public class BIllboard2D : MonoBehaviour
{
    private Transform _transform, _camTransform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _camTransform = Camera.main.transform;
    }

    void Update()
    {
        var target = _camTransform.position;
        target.y = transform.position.y;
        _transform.LookAt(target);
    }
}
