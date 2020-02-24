using UnityEngine;

public class MenuInputController : MonoBehaviour {

    [SerializeField]
    private string _unpauseKey;
	
	void Update () {
		if(Input.GetKeyDown(_unpauseKey))
        {
            EventManager.TriggerEvent("UnpauseStarted");
        }
    }
}
