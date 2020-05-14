using UnityEngine;

public class ChangeAudioSourceLevelByValueOnTrigger : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    [Range(-1f, 1f)]
    private float _value;
    [SerializeField]
    private string _trigger;

    void Start()
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if(!string.IsNullOrEmpty(_trigger))
        {
            EventManager.StartListening(_trigger, ChangeVolume);
        }
    }

    private void ChangeVolume()
    {
        _audioSource.volume += _value;
    }
}
