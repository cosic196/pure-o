using UnityEngine;

public class PlaySoundWithRandomPitchOnTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    [Range(0f, 1f)]
    private float _maxPitchDifference;
    [SerializeField]
    private string _trigger;

    private float _startingPitch;

    void Start()
    {
        _startingPitch = _audioSource.pitch;
        EventManager.StartListening(_trigger, PlaySound);
    }

    private void PlaySound()
    {
        _audioSource.pitch = _startingPitch + Random.Range(-_maxPitchDifference, _maxPitchDifference);
        _audioSource.PlayOneShot(_audioSource.clip);
    }
}
