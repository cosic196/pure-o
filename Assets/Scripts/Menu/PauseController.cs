﻿using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    [SerializeField]
    private List<MonoBehaviour> _componentsToDisable;
    [SerializeField]
    private List<MonoBehaviour> _componentsToEnable;
    [SerializeField]
    private List<AudioSource> _audioSourcesToPause;

    private float _timeScaleBeforePause;

    void Start () {
        EventManager.StartListening("Paused", Pause);
        EventManager.StartListening("Unpaused", Unpause);
    }
	
	private void Pause()
    {
        _timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0f;
        CustomTime.SetPaused();
        foreach(var component in _componentsToDisable)
        {
            component.enabled = false;
        }
        foreach (var component in _componentsToEnable)
        {
            component.enabled = true;
        }
        foreach(var audioSource in _audioSourcesToPause)
        {
            audioSource.Pause();
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Unpause()
    {
        foreach (var component in _componentsToDisable)
        {
            component.enabled = true;
        }
        foreach (var component in _componentsToEnable)
        {
            component.enabled = false;
        }
        foreach(var audioSource in _audioSourcesToPause)
        {
            audioSource.UnPause();
        }
        Cursor.visible = false;
        Time.timeScale = _timeScaleBeforePause;
        CustomTime.SetUnpaused();
    }
}
