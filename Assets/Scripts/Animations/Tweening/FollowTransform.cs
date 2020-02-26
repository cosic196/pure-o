using UnityEngine;

public class FollowTransform : MonoBehaviour {

    [SerializeField]
    private Transform _transformToFollow;
    [SerializeField]
    private float _smoothStep;
    private Transform _transform;

	void Start () {
        _transform = GetComponent<Transform>();
	}
	
	void Update () {
        if(Vector3.Distance(_transform.position, _transformToFollow.position) < 0.00001f)
        {
            return;
        }
        _transform.rotation = Quaternion.Slerp(_transform.rotation, _transformToFollow.rotation, CustomTime.GetDeltaTime() * _smoothStep);
        _transform.position = Vector3.Lerp(_transform.position, _transformToFollow.position, CustomTime.GetDeltaTime() * _smoothStep);
	}
}
