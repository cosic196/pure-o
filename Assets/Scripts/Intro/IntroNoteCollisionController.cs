using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(GameObjectEventManager))]
public class IntroNoteCollisionController : MonoBehaviour
{

    private GameObjectEventManager _gameObjectEventManager;
    public NoteInfo NoteInfo { get; private set; }
    [SerializeField]
    private float _timeThreshold;
    private float _timer;
    private bool _isDouble = false;
    private bool _isShot = false;

    public void Init(NoteInfo noteInfo, bool isDouble = false)
    {
        NoteInfo = noteInfo;
        _isDouble = isDouble;
    }

    private void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        EventManager.StartListening("NoteShot", NoteShot);
        EventManager.StartListening("StartSpawningRight", MoveToTheRight);
        EventManager.StartListening("StartSpawningLeft", MoveToTheLeft);
    }

    private void MoveToTheLeft()
    {
        NoteInfo.position = ShootController.Position.Left;
    }

    private void MoveToTheRight()
    {
        NoteInfo.position = ShootController.Position.Right;
    }

    private void OnEnable()
    {
        _timer = _timeThreshold;
        _isShot = false;
    }

    private void Update()
    {
        if (_timer < _timeThreshold)
        {
            _timer += CustomTime.GetDeltaTime();
        }
        else if (_timer > _timeThreshold)
        {
            _timer = _timeThreshold;
            if (!_isShot && !_isDouble)
            {
                EventManager.TriggerEvent("NoteOutOfRange", JsonUtility.ToJson(NoteInfo));
            }
        }
    }

    private void NoteShot(string noteInfoJson)
    {
        if (JsonUtility.FromJson<NoteInfo>(noteInfoJson).id == NoteInfo.id)
        {
            _isShot = true;
            if (!_isDouble)
            {
                _gameObjectEventManager.TriggerEvent("NoteShot");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NoteInRangeTrigger")
        {
            if (!_isDouble)
            {
                EventManager.TriggerEvent("NoteInRange", JsonUtility.ToJson(NoteInfo));
            }
        }
        else if (collision.gameObject.tag == "NoteOutOfRangeTrigger")
        {
            _gameObjectEventManager.TriggerEvent("NoteOutOfRange");
            _timer = 0f;
        }
    }
}