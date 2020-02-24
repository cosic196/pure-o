using UnityEngine;

public class TimeScaler : MonoBehaviour {

    public AudioSource _audioSource;
    public KeyCode keycodetest;
    
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
        {
            Time.timeScale += 0.1f;
            _audioSource.pitch += 0.1f;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale -= 0.1f;
            _audioSource.pitch -= 0.1f;
        }
	}
}
