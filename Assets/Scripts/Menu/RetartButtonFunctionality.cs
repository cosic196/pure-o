using UnityEngine;

public class RetartButtonFunctionality : MonoBehaviour {

	public void Restart()
    {
        EventManager.TriggerEvent("FadeOut", "Restarted");
    }
}
