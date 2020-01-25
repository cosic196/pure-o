using SonicBloom.Koreo;
using SonicBloom.Koreo.Players;
using UnityEngine;

public class KoreographerEventManager : MonoBehaviour {

    [SerializeField]
    private string _noteEventId;
    [SerializeField]
    private string _lineEventId;
    [SerializeField]
    private string _enemyDeathEventId;
    private SimpleMusicPlayer _simpleMusicPlayer;

	void Start () {
        Koreographer.Instance.RegisterForEvents(_noteEventId, TriggerNote);
        Koreographer.Instance.RegisterForEvents(_lineEventId, TriggerLine);
        Koreographer.Instance.RegisterForEvents(_enemyDeathEventId, TriggerEnemyDeath);
        _simpleMusicPlayer = GetComponent<SimpleMusicPlayer>();
    }

    // TODO : Delete after testing
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _simpleMusicPlayer.Play();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            EventManager.TriggerEvent("EnemiesDie");
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            EventManager.TriggerEvent("EnemiesMoved");
        }
    }

    private void TriggerEnemyDeath(KoreographyEvent koreoEvent)
    {
        EventManager.TriggerEvent("EnemiesDie");
    }

    void TriggerLine(KoreographyEvent evt)
    {
        EventManager.TriggerEvent("SpawnBeatLine");
    }

    void TriggerNote(KoreographyEvent evt)
    {
        if(evt.HasIntPayload())
        {
            int payload = evt.GetIntValue();
            if(payload == 0)
            {
                EventManager.TriggerEvent("SpawnNoteCenter");
            }
            else if(payload > 0)
            {
                EventManager.TriggerEvent("SpawnNoteRight");
            }
            else if(payload < 0)
            {
                EventManager.TriggerEvent("SpawnNoteLeft");
            }
        }
    }
}
