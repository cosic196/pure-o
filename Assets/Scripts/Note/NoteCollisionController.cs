using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(GameObjectEventManager))]
public class NoteCollisionController : MonoBehaviour {

    [SerializeField]
    private ShootController.Position _position;
    private GameObjectEventManager _gameObjectEventManager;
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
        if (collision.gameObject.tag == "NoteInRangeTrigger")
        {
            EventManager.TriggerEvent("NoteInRange", JsonUtility.ToJson(NoteInfo));
        }
        else if (collision.gameObject.tag == "NoteOutOfRangeTrigger")
        {
            EventManager.TriggerEvent("NoteOutOfRange", JsonUtility.ToJson(NoteInfo));
            _gameObjectEventManager.TriggerEvent("NoteOutOfRange");
        }
    }
}