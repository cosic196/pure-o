using UnityEngine;

public class ContinueButtonFunctionality : MonoBehaviour {

	public void Continue()
    {
        EventManager.TriggerEvent("UnpauseStarted");
    }

    public void ContinueToNextScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EventManager.TriggerEvent("FadeOut", "LoadNextLevel");
    }
}
