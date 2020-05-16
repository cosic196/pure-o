using UnityEngine;

public class ContinueButtonFunctionality : MonoBehaviour {

    [SerializeField]
    private bool _whiteFadeOut = false;

	public void Continue()
    {
        EventManager.TriggerEvent("UnpauseStarted");
    }

    public void ContinueToNextScene()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if(_whiteFadeOut)
        {
            EventManager.TriggerEvent("WhiteFadeOut", "LoadNextLevel");
        }
        else
        {
            EventManager.TriggerEvent("FadeOut", "LoadNextLevel");
        }
    }
}
