using UnityEngine;

public class FocusController : MonoBehaviour
{
    private GameObjectEventManager _gameObjectEventManager;

    void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _gameObjectEventManager.StartListening("PressedZoom", Focus);
        _gameObjectEventManager.StartListening("UnpressedZoom", UnFocus);
    }

    private void UnFocus()
    {
        EventManager.TriggerEvent("Unfocused");
    }

    private void Focus()
    {
        EventManager.TriggerEvent("Focused");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Focusable")))
        {
            EventManager.TriggerEvent("FocusedAnObject", hit.transform.name);
        }
    }
}
