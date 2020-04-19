using UnityEngine;

public class ContinueButtonFunctionality : MonoBehaviour {

	public void Continue()
    {
        EventManager.TriggerEvent("UnpauseStarted");
    }

    public void ContinueToNextScene()
    {
        EventManager.TriggerEvent("FadeOut", "LoadNextLevel");
    }
}
