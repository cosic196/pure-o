using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsAudioController : MonoBehaviour, IOption {

    [SerializeField]
    private TextMeshProUGUI _volumeValueText;
    [SerializeField]
    private AudioMixer _audioMixer;

    private float _currentVolume;
    private float _increaseValue = 8f;

    void Start () {
        Reset();
	}

    private void SetText(float volume)
    {
        _volumeValueText.text = ((volume + 80) / 80 * 100) + "%";
    }

    public void SetHigherVolume()
    {
        if (_currentVolume + _increaseValue > 0)
        {
            _currentVolume = 0;
        }
        else
        {
            _currentVolume += _increaseValue;
        }
        _audioMixer.SetFloat("Volume", _currentVolume);
        SetText(_currentVolume);
    }

    public void SetLowerVolume()
    {
        if (_currentVolume - _increaseValue < -80)
        {
            _currentVolume = -80;
        }
        else
        {
            _currentVolume -= _increaseValue;
        }
        _audioMixer.SetFloat("Volume", _currentVolume);
        SetText(_currentVolume);
    }

    public void Apply()
    {
        PlayerPrefs.SetFloat("Volume", _currentVolume);
    }

    public void Reset()
    {
        var volume = PlayerPrefs.GetFloat("Volume", 0);
        SetText(volume);
        _audioMixer.SetFloat("Volume", volume);
        _currentVolume = volume;
    }
}
