using System.Collections.Generic;
using UnityEngine;

public class EnableOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _triggerName;
    [SerializeField]
    private List<GameObject> _objectsToEnable;
    [SerializeField]
    private float _delay = 0f;
    [SerializeField]
    private bool _useUnscaledTime = true;
    [SerializeField]
    private bool _playOnAwake = false;
    private float _timer;

	void Start () {
        _timer = _delay;
        if (_playOnAwake)
            StartEnable();
        if(!string.IsNullOrEmpty(_triggerName))
            EventManager.StartListening(_triggerName, StartEnable);
	}
	
	void Update () {
		if(_timer < _delay)
        {
            if(_useUnscaledTime)
            {
                _timer += Time.unscaledDeltaTime;
            }
            else
            {
                _timer += CustomTime.GetDeltaTime();
            }
            if(_timer >= _delay)
            {
                foreach (var obj in _objectsToEnable)
                {
                    obj.SetActive(true);
                }
            }
        }
	}

    private void StartEnable()
    {
        if(_delay <= 0)
        {
            foreach (var obj in _objectsToEnable)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            _timer = 0f;
        }
    }
}
