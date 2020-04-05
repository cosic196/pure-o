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
    private string _enemyAppearEventId;
    [SerializeField]
    private string _enemyDisappearEventId;
    [SerializeField]
    private string _otherEventId;
    private SimpleMusicPlayer _simpleMusicPlayer;

    void Start () {
        _simpleMusicPlayer = GetComponent<SimpleMusicPlayer>();
        Koreographer.Instance.RegisterForEvents(_enemyDeathEventId, TriggerEnemyDeath);
        Koreographer.Instance.RegisterForEvents(_playerMoveEventId, TriggerPayerMove);
        Koreographer.Instance.RegisterForEvents(_enemyAppearEventId, TriggerEnemiesAppeared);
        Koreographer.Instance.RegisterForEvents(_enemyDisappearEventId, TriggerEnemiesDisappeared);
        Koreographer.Instance.RegisterForEvents(_otherEventId, TriggerOther);
        EventManager.StartListening("LevelStarted", Play);
    }

    private void TriggerEnemiesDisappeared(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("EnemiesDisappearedKoreo", koreoEvent.GetTextValue());
    }

    private void TriggerOther(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent(koreoEvent.GetTextValue());
    }

    private void TriggerEnemiesAppeared(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("EnemiesAppearedKoreo", koreoEvent.GetFloatValue().ToString());
    }

    private void TriggerPayerMove(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("MoveToNextPoint", koreoEvent.GetFloatValue().ToString());
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
