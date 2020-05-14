using UnityEngine;

public class SlowDownNoteController : MonoBehaviour
{
    [SerializeField]
    private GameObject _slowDownNotePrefab;
    private GameObjectEventManager _gameObjectEventManager;
    private Transform _transform;
    private GameObject _slowDownGO;
    private static int _noteCount = 20;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _slowDownGO = Instantiate(_slowDownNotePrefab, _transform);
    }

    private void OnEnable()
    {
        if(_noteCount > 0)
        {
            _noteCount--;
            return;
        }
        if(TryBecomeASlowDownNote())
        {
            _gameObjectEventManager.StartListening("NoteShot", StartSlowDown);
            _slowDownGO.SetActive(true);
            if(_noteCount == -1)
            {
                _noteCount = UpgradesSlowDownController.NumberOfNotesToSkip;
                return;
            }
            else
            {
                _noteCount = -1;
            }
        }
    }

    private bool TryBecomeASlowDownNote()
    {
        double chance = UpgradesSlowDownController.ChanceOnNoteSpawn;
        double random = GetRandomNumber();
        if(chance >= random)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartSlowDown()
    {
        EventManager.TriggerEvent("UpgradesStartSlowDown");
    }

    private void OnDisable()
    {
        _gameObjectEventManager.StopListening("NoteShot", StartSlowDown);
        _slowDownGO.SetActive(false);
    }

    private static System.Random _rng = new System.Random();
    private static int _rngCounter = 0;
    private static double _currentRng;

    private static double GetRandomNumber()
    {
        if (_rngCounter % 2 == 0)
        {
            _currentRng = _rng.NextDouble() * 100;
        }
        _rngCounter++;
        return _currentRng;
    }
}
