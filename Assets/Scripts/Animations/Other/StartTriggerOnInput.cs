using System.Collections.Generic;
using UnityEngine;

public class StartTriggerOnInput : MonoBehaviour {

    [SerializeField]
    private List<string> _triggers;
	
	void Update () {
		if(Input.anyKey)
        {
            foreach (var trigger in _triggers)
            {
                EventManager.TriggerEvent(trigger);
            }
        }
	}
}
