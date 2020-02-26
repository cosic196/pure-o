using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SensitivitySlider : MonoBehaviour {

    private Slider _slider;

	void Start () {
        _slider = GetComponent<Slider>();
	}
	
	public void SetSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", _slider.value);
    }

    public void TriggerSensitivityEvent()
    {
        EventManager.TriggerEvent("SensitivityChanged");
    }
}
