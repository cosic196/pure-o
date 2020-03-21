using System;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;

public class KoreographerEventManager : MonoBehaviour {
    
    [SerializeField]
    private string _enemyDeathEventId;
    [SerializeField]
    private string _playerMoveEventId;
    [SerializeField]
    private string _enemyMoveEventId;
    private SimpleMusicPlayer _simpleMusicPlayer;

    void Start () {
        _simpleMusicPlayer = GetComponent<SimpleMusicPlayer>();
        Koreographer.Instance.RegisterForEvents(_enemyDeathEventId, TriggerEnemyDeath);
        Koreographer.Instance.RegisterForEvents(_playerMoveEventId, TriggerPayerMove);
        Koreographer.Instance.RegisterForEvents(_enemyMoveEventId, TriggerEnemiesMoved);
        EventManager.StartListening("LevelStarted", Play);
    }

    private void TriggerEnemiesMoved(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("EnemiesMoved");
    }

    private void TriggerPayerMove(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("MoveToNextPoint");
    }

    // TODO : Delete after testing
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            EventManager.TriggerEvent("EnemiesDie");
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            EventManager.TriggerEvent("EnemiesMoved");
        }
    }

    private void Play()
    {
        EventManager.TriggerEvent("KoreographerStarted");
        _simpleMusicPlayer.Play();
    }

    private void TriggerEnemyDeath(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("EnemiesDie");
    }
}
