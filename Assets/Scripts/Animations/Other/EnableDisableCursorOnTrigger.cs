using UnityEngine;

public class EnableDisableCursorOnTrigger : MonoBehaviour
{
    enum AdditionalOption
    {
        Auto,
        EnableOnly,
        DisableOnly
    }
    [SerializeField]
    private string _trigger;
    [SerializeField]
    private AdditionalOption _type = AdditionalOption.Auto;

    void Start()
    {
        EventManager.StartListening(_trigger, ChangeCursorEnabled);
    }

    private void ChangeCursorEnabled()
    {
        if(_type == AdditionalOption.EnableOnly)
        {
            EnableCursor();
            return;
        }
        else if(_type == AdditionalOption.DisableOnly)
        {
            DisableCursor();
            return;
        }
        if(!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
        {
            EnableCursor();
        }
        else
        {
            DisableCursor();
        }
    }

    private void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
