using System;
using SonicBloom.Koreo;
using UnityEngine;

public class IntroNoteMovementController : MonoBehaviour
{

    private Transform _transform;
    private Koreography _playingKoreo;
    private KoreographyEvent _trackedEvent;
    private float _speedMultiplier = 1f;
    private bool _spawnedAfterEvent;
    private float _sideNoteXOffset;
    private float _x = 0f;

    private void Start()
    {
        EventManager.StartListening("StartSpawningRight", MoveToTheRight);
        EventManager.StartListening("StartSpawningLeft", MoveToTheLeft);
    }

    private void MoveToTheLeft()
    {
        _x = -_sideNoteXOffset;
    }

    private void MoveToTheRight()
    {
        _x = _sideNoteXOffset;
    }

    private void Update()
    {
        Move();
        if (_transform.localPosition.y * _speedMultiplier < 0f)
        {
            _transform.localPosition = new Vector3(_x, 0f);
            return;
        }
        if (_transform.localPosition.y == 0f)
        {
            return;
        }
    }

    private void Move()
    {
        float samplesPerUnit = _playingKoreo.SampleRate / LevelStats.Reference.NoteSpeed;
        float y;
        if(_trackedEvent.StartSample < _playingKoreo.GetLatestSampleTime() && _spawnedAfterEvent)
        {
            y = -((_playingKoreo.GetLatestSampleTime() - _playingKoreo.SourceClip.samples - _trackedEvent.StartSample) / samplesPerUnit);
        }
        else
        {
            y = -((_playingKoreo.GetLatestSampleTime() - _trackedEvent.StartSample) / samplesPerUnit);
            _spawnedAfterEvent = false;
        }
        //if (Mathf.Abs(y) <= samplesPerUnit)
        //    return;
        _transform.localPosition = new Vector3(_x, _speedMultiplier * y, _transform.localPosition.z);
    }

    private void OnEnable()
    {
        if (_trackedEvent.StartSample < _playingKoreo.GetLatestSampleTime())
            _spawnedAfterEvent = true;
        else
            _spawnedAfterEvent = false;
    }

    public void Init(KoreographyEvent koreographyEvent, float x, float xOffset, float speedMultiplier = 1f)
    {
        _transform = GetComponent<Transform>();
        _playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);
        _trackedEvent = koreographyEvent;
        _speedMultiplier = speedMultiplier;
        _x = x;
        _sideNoteXOffset = xOffset;
        Move();
    }
}
