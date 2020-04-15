using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _triggerName;
    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        EventManager.StartListening(_triggerName, Play);
    }

    private void Play()
    {
        _audioSource.Play();
        enabled = false;
    }

    private void OnDisable()
    {
        EventManager.StopListening(_triggerName, Play);
    }
}
