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
        Koreographer.Instance.RegisterForEventsWithTime(_playerMoveEventId, TriggerPlayerMove);
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

    private void TriggerPlayerMove(KoreographyEvent koreoEvent, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if(koreoEvent.IsOneOff())
        {
            EventManager.TriggerEvent("MoveToNextPoint", koreoEvent.GetFloatValue().ToString());
        }
        else
        {
            if (koreoEvent.StartSample < sampleTime - sampleDelta &&
            koreoEvent.EndSample > sampleTime)
            {
                EventManager.TriggerEvent("MoveToNextPointSpan", koreoEvent.GetValueOfCurveAtTime(sampleTime).ToString());
            }
            else
            {
                if (koreoEvent.StartSample >= sampleTime - sampleDelta)
                {
                    EventManager.TriggerEvent("MoveToNextPointSpanStart", koreoEvent.GetValueOfCurveAtTime(sampleTime).ToString());
                }
                if (koreoEvent.EndSample <= sampleTime)
                {
                    EventManager.TriggerEvent("MoveToNextPointSpanEnd");
                }
            }
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
