using UnityEngine;

public class HideCursorOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _trigger;
    [SerializeField]
    private bool _hideOnAwake;

	void Start () {
		if(!string.IsNullOrEmpty(_trigger))
        {
            EventManager.StartListening(_trigger, DisableCursor);
        }
        if(_hideOnAwake)
        {
            DisableCursor();
        }
	}

    private void DisableCursor()
    {
        Cursor.visible = false;
    }
}
