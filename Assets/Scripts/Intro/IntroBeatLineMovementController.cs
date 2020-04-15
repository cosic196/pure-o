using SonicBloom.Koreo;
using UnityEngine;

public class IntroBeatLineMovementController : MonoBehaviour
{

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
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (_transform.localPosition.y >= 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Move()
    {
        float samplesPerUnit = _playingKoreo.SampleRate / LevelStats.Reference.NoteSpeed;
        float y;
        if (_trackedEvent.StartSample < _playingKoreo.GetLatestSampleTime())
        {
            y = -((_playingKoreo.GetLatestSampleTime() - _playingKoreo.SourceClip.samples - _trackedEvent.StartSample) / samplesPerUnit);
        }
        else
        {
            y = -((_playingKoreo.GetLatestSampleTime() - _trackedEvent.StartSample) / samplesPerUnit);
        }
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
