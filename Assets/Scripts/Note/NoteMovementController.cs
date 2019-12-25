using UnityEngine;

public class NoteMovementController : MonoBehaviour {

    private Transform _transform;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Vector3 _goal;
    private Vector3 _normalizedDirection;

    public float Speed
    {
        get
        {
            return _speed;
        }
    }

    public Vector3 Goal
    {
        get
        {
            return _goal;
        }
    }

	void Start () {
        _transform = GetComponent<Transform>();
        _normalizedDirection = (_goal - _transform.localPosition).normalized;
	}
	
	void Update () {
        _transform.localPosition += _normalizedDirection * _speed * CustomTime.GetDeltaTime();
    }
}
