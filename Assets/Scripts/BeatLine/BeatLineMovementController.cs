using UnityEngine;

public class BeatLineMovementController : MonoBehaviour {

    private Transform _transform;
    private Vector3 _currentPos;
    private float _timer = 0f;

	void OnEnable () {
        _transform = GetComponent<Transform>();
        _currentPos = _transform.localPosition;
	}
	
	void Update () {
        _transform.localPosition = Vector3.MoveTowards(_currentPos, Vector3.zero, _timer);
        _timer += LevelStats.Reference.NoteSpeed * CustomTime.GetDeltaTime();
        if(_transform.localPosition == Vector3.zero)
        {
            gameObject.SetActive(false);
        }
	}
}
