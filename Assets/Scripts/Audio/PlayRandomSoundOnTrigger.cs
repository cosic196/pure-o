using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSoundOnTrigger : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _audioClips;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private string _trigger;

    void Start()
    {
        EventManager.StartListening(_trigger, PlayRandomSound);
    }

    private void PlayRandomSound()
    {
        _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Count - 1)]);
    }
}
