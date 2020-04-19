using TMPro;
using UnityEngine;

public class OptionsGraphicsController : MonoBehaviour, IOption {

    [SerializeField]
    TextMeshProUGUI _graphicsValueText;
    private string[] _qualitySettings;
    private int _currentQualitySetting;

    void Start () {
        _qualitySettings = QualitySettings.names;
        Reset();
	}

    public void SetHigherGraphics()
    {
        if(_currentQualitySetting == _qualitySettings.Length - 1)
        {
            return;
        }
        _currentQualitySetting++;
        SetText(_currentQualitySetting);
    }

    public void SetLowerGraphics()
    {
        if (_currentQualitySetting == 0)
        {
            return;
        }
        _currentQualitySetting--;
        SetText(_currentQualitySetting);
    }

    public void Apply()
    {
        QualitySettings.SetQualityLevel(_currentQualitySetting);
    }

    public void Reset()
    {
        _currentQualitySetting = QualitySettings.GetQualityLevel();
        SetText(_currentQualitySetting);
    }

    private void SetText(int qualityLevel)
    {
        _graphicsValueText.text = _qualitySettings[qualityLevel];
    }
}
