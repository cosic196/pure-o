using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(GameObjectEventManager))]
public class NoteDoubleCollisionController : MonoBehaviour {

    private GameObjectEventManager _gameObjectEventManager;
    [SerializeField]
    private ShootController.Position _position;
    public NoteInfo NoteInfo { get; private set; }

    public void Init(int id)
    {
        NoteInfo = new NoteInfo
        {
            id = id,
            position = _position
        };
    }

    private void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NoteOutOfRangeTrigger")
        {
            _gameObjectEventManager.TriggerEvent("NoteOutOfRange");
        }
    }
}
