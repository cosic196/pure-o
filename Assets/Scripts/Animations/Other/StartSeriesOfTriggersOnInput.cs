using System.Collections.Generic;
using UnityEngine;

public class StartSeriesOfTriggersOnInput : MonoBehaviour {

    [SerializeField]
    private List<ListOfTriggers> _triggers;
    int _index = 0;
	
	void Update () {
		if(Input.anyKeyDown)
        {
            if(_index < _triggers.Count)
            {
                foreach (var trigger in _triggers[_index].triggers)
                {
                    EventManager.TriggerEvent(trigger);
                }
                _index++;
            }
        }
	}

    [System.Serializable]
    private class ListOfTriggers
    {
        public List<string> triggers;
    }
}
