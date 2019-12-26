using UnityEngine;

public class NoteInitializer : MonoBehaviour {

    public void Init(Vector3 localPosition, NoteInfo noteInfo, bool isDouble = false)
    {
        GetComponent<NoteCollisionController>().Init(noteInfo, isDouble);
        GetComponent<Transform>().localPosition = localPosition;
        GetComponent<GameObjectEventManager>().TriggerEvent("NoteInitialized");
    }
}
