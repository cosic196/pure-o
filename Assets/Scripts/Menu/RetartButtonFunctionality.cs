using UnityEngine;

public class RetartButtonFunctionality : MonoBehaviour {

	public void Restart()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        EventManager.TriggerEvent("FadeOut", "Restarted");
    }
}
