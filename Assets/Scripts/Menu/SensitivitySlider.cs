using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SensitivitySlider : MonoBehaviour {

    private Slider _slider;

	void Start () {
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat("Sensitivity", 1f);
        _slider.onValueChanged.AddListener(delegate { SetSensitivity(); TriggerSensitivityEvent(); });
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
