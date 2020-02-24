using UnityEngine;

public class ContinueButtonFunctionality : MonoBehaviour {

	public void Continue()
    {
        EventManager.TriggerEvent("UnpauseStarted");
    }
}
