using UnityEngine;

public class NoteMovementController : MonoBehaviour {

    private Transform _transform;
    private Vector3 _goal, _currentPosition;
    private float _distance;

    private void OnEnable()
    {
        _distance = 0f;
        _transform = GetComponent<Transform>();
        _currentPosition = _transform.localPosition;
        _goal = new Vector3(_transform.localPosition.x, 0f, 0f);
    }

    void Update () {
        _distance += LevelStats.Reference.NoteSpeed * CustomTime.GetDeltaTime();
        _transform.localPosition = Vector3.MoveTowards(_currentPosition, _goal, _distance);
    }
}
