using UnityEngine;

public class InputController : MonoBehaviour {

    [SerializeField]
    private string _moveLeftKey;
    [SerializeField]
    private string _moveRightKey;
    [SerializeField]
    private string _shootKey;
    [SerializeField]
    private string _pauseKey;

    private void Start()
    {
        EventManager.StartListening("Unpaused", OnUnpaused);
    }

    void Update () {
        //Pausing
        if(Input.GetKeyDown(_pauseKey))
        {
            EventManager.TriggerEvent("Paused");
            return;
        }

        //Shooting
		if(Input.GetKeyDown(_shootKey))
        {
            EventManager.TriggerEvent("PressedShoot");
        }

        //Moving
        if (Input.GetKeyDown(_moveRightKey) && !Input.GetKey(_moveLeftKey))
        {
            EventManager.TriggerEvent("PressedRight");
        }
        else if (Input.GetKeyDown(_moveLeftKey) && !Input.GetKey(_moveRightKey))
        {
            EventManager.TriggerEvent("PressedLeft");
        }
        else if (Input.GetKeyUp(_moveLeftKey))
        {
            if (Input.GetKey(_moveRightKey))
            {
                EventManager.TriggerEvent("PressedRight");
            }
            else
            {
                EventManager.TriggerEvent("PressedCenter");
            }
        }
        else if (Input.GetKeyUp(_moveRightKey))
        {
            if (Input.GetKey(_moveLeftKey))
            {
                EventManager.TriggerEvent("PressedLeft");
            }
            else
            {
                EventManager.TriggerEvent("PressedCenter");
            }
        }
    }

    private void OnUnpaused()
    {
        if(!Input.GetKey(_moveRightKey) && !Input.GetKey(_moveLeftKey))
        {
            EventManager.TriggerEvent("PressedCenter");
        }
        else if(Input.GetKey(_moveRightKey))
        {
            EventManager.TriggerEvent("PressedRight");
        }
        else if (Input.GetKey(_moveLeftKey))
        {
            EventManager.TriggerEvent("PressedLeft");
        }
    }
}
