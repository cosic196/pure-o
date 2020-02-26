using SonicBloom.Koreo;
using UnityEngine;

public class NoteMovementController : MonoBehaviour {

    private Transform _transform;
    private Koreography _playingKoreo;
    private KoreographyEvent _trackedEvent;
    private float _speedMultiplier = 1f;

    private void Update()
    {
        Move();
        if (_speedMultiplier > 0)
        {
            if (_transform.localPosition.y <= 0f)
            {
                _transform.localPosition = new Vector3(_transform.localPosition.x, 0f);
            }
        }
        else
        {
            if (_transform.localPosition.y >= 0f)
            {
                _transform.localPosition = new Vector3(_transform.localPosition.x, 0f);
            }
        }
    }

    private void Move()
    {
        float samplesPerUnit = _playingKoreo.SampleRate / LevelStats.Reference.NoteSpeed;
        float y = -((_playingKoreo.GetLatestSampleTime() - _trackedEvent.StartSample) / samplesPerUnit);
        //if (Mathf.Abs(y) <= samplesPerUnit)
        //    return;
        _transform.localPosition = new Vector3(_transform.localPosition.x, _speedMultiplier * y, _transform.localPosition.z);
    }

    public void Init(KoreographyEvent koreographyEvent, float speedMultiplier = 1f)
    {
        _transform = GetComponent<Transform>();
        _playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);
        _trackedEvent = koreographyEvent;
        _speedMultiplier = speedMultiplier;
        Move();
    }
}
