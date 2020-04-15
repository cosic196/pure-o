using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SynthPlayer : MonoBehaviour {

    [SerializeField]
    private List<PlayedOnEvent> _audioEventPairs;
    private AudioSource _audioSource;

	void Start () {
        _audioSource = GetComponent<AudioSource>();
        foreach (var ev in _audioEventPairs)
        {
            EventManager.StartListening(ev.Event, () => { _audioSource.PlayOneShot(ev.AudioClip); });
        }
	}
}

[Serializable]
public class PlayedOnEvent
{
    public AudioClip AudioClip;
    public string Event;
}
