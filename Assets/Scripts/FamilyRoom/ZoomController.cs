using UnityEngine;

public class ZoomController : MonoBehaviour
{
    [SerializeField]
    private KeyCode _zoomKey;
    private GameObjectEventManager _gameObjectEventManager;

    void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(_zoomKey))
        {
            _gameObjectEventManager.TriggerEvent("PressedZoom");
        }
        if(Input.GetKeyUp(_zoomKey))
        {
            _gameObjectEventManager.TriggerEvent("UnpressedZoom");
        }
    }
}
