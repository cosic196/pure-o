using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _eventName;
    private AudioSource _audioSource;
    
	void Start () {
        _audioSource = GetComponent<AudioSource>();
        EventManager.StartListening(_eventName, () => _audioSource.Play());
	}
}
