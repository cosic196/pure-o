using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsResolutionController : MonoBehaviour, IOption {

    [SerializeField]
    TextMeshProUGUI _resolutionValueText;
    [SerializeField]
    Image _fullscreenValueImage;

    private Resolution[] _resolutions;
    private Resolution _selectedResolution;
    private FullScreenMode _selectedFullScreenMode;

    void Start () {
        _resolutions = Screen.resolutions.Where(x => x.refreshRate >= 59).ToArray();
        Reset();
	}

    public void SetHigherResolution()
    {
        if(Array.IndexOf(_resolutions, _selectedResolution) == _resolutions.Length - 1)
        {
            return;
        }
        _selectedResolution = _resolutions[Array.IndexOf(_resolutions, _selectedResolution) + 1];
        WriteResolution(_selectedResolution);
    }

    public void SetLowerResolution()
    {
        if (Array.IndexOf(_resolutions, _selectedResolution) == 0)
        {
            return;
        }
        _selectedResolution = _resolutions[Array.IndexOf(_resolutions, _selectedResolution) - 1];
        WriteResolution(_selectedResolution);
    }

    public void ChangeFullScreenMode()
    {
        if(_selectedFullScreenMode == FullScreenMode.ExclusiveFullScreen)
        {
            _selectedFullScreenMode = FullScreenMode.Windowed;
            _fullscreenValueImage.enabled = false;
        }
        else
        {
            _selectedFullScreenMode = FullScreenMode.ExclusiveFullScreen;
            _fullscreenValueImage.enabled = true;
        }
    }

    public void Apply()
    {
        Screen.SetResolution(_selectedResolution.width, _selectedResolution.height, _selectedFullScreenMode);
    }

    public void Reset()
    {
        _fullscreenValueImage.enabled = Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen ? true : false;
        WriteResolution(Screen.currentResolution);
        _selectedResolution = Screen.currentResolution;
        _selectedFullScreenMode = Screen.fullScreenMode;
    }

    private void WriteResolution(Resolution resolution)
    {
        _resolutionValueText.text = resolution.width + " x " + resolution.height + " <color=#AAA>" + resolution.refreshRate + "hZ</color>";
    }
}
