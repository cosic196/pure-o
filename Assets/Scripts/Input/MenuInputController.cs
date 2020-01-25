using UnityEngine;

public class MenuInputController : MonoBehaviour {

    [SerializeField]
    private string _unpauseKey;
    [SerializeField]
    private string _restartKey;
	
	void Update () {
		if(Input.GetKeyDown(_unpauseKey))
        {
            EventManager.TriggerEvent("Unpaused");
        }
        else if(Input.GetKeyDown(_restartKey))
        {
            EventManager.TriggerEvent("Restarted");
        }
    }
}
