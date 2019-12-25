using UnityEngine;

public class NoteSpawnTester : MonoBehaviour {

    private float _timer = 0f;
    [SerializeField]
    private bool _autoSpawn;

	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            EventManager.TriggerEvent("SpawnNoteLeft");
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            EventManager.TriggerEvent("SpawnNoteRight");
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            EventManager.TriggerEvent("SpawnNoteCenter");
        }

        if(_autoSpawn)
            AutoSpawn();
	}

    private void AutoSpawn()
    {
        if(_timer < 1f)
        {
            _timer += CustomTime.GetDeltaTime();
        }
        else
        {
            int randomNumber = Random.Range(0, 3);
            switch (randomNumber)
            {
                case 0:
                    EventManager.TriggerEvent("SpawnNoteLeft");
                    break;
                case 1:
                    EventManager.TriggerEvent("SpawnNoteRight");
                    break;
                case 2:
                    EventManager.TriggerEvent("SpawnNoteCenter");
                    break;
            }
            _timer = 0f;
        }
    }
}
