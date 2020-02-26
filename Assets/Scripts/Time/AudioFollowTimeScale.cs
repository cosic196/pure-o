using UnityEngine;

public class AudioFollowTimeScale : MonoBehaviour {

    private AudioSource _audioSource;
    
	void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		if(_audioSource.pitch != Time.timeScale)
        {
            _audioSource.pitch = Time.timeScale;
        }
	}
}
