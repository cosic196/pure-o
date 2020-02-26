using UnityEngine;

public class DeathMenuController : MonoBehaviour {

	void Update () {
		if(Input.anyKeyDown)
        {
            EventManager.TriggerEvent("Restarted");
        }
	}
}
