using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ActiveLineColoring : MonoBehaviour {

    [SerializeField]
    private string _triggerToActivate;
    [SerializeField]
    private List<string> _triggersToDeactivate;
    [SerializeField]
    private Color _activeColor;
    [SerializeField]
    private float _animationSpeed;
    [SerializeField]
    private AnimationCurve _animationCurve;
    private Image _image;
    private Color _inactiveColor;
    private float _timer = 0f, _timerAdd = 0f;
    
	void Start () {
        _image = GetComponent<Image>();
        _inactiveColor = _image.color;
        EventManager.StartListening(_triggerToActivate, ColorActive);
        foreach(var trigger in _triggersToDeactivate)
        {
            EventManager.StartListening(trigger, ColorInactive);
        }
    }

    private void ColorActive()
    {
        if(_timer < 0f)
        {
            _timer = 0f;
        }
        _timerAdd = _animationSpeed * CustomTime.GetDeltaTime();
    }

    private void ColorInactive()
    {
        if(_timer > 1f)
        {
            _timer = 1f;
        }
        _timerAdd = -(_animationSpeed * CustomTime.GetDeltaTime());
    }

    void Update () {
		if(_timer <= 1f && _timer >= 0f)
        {
            _timer += _timerAdd;
            _image.color = Color.Lerp(_inactiveColor, _activeColor, _animationCurve.Evaluate(_timer));
        }
	}
}