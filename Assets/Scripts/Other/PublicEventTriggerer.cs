using UnityEngine;

public class PublicEventTriggerer : MonoBehaviour {

	public void TriggerEvent(string eventName)
    {
        EventManager.TriggerEvent(eventName);
    }
}
