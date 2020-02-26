using System.Collections;
using UnityEngine;

public class LevelStarter : MonoBehaviour {

	void Start () {
        StartCoroutine(StartLevel());
	}

    private IEnumerator StartLevel()
    {
        yield return null;
        EventManager.TriggerEvent("LevelStarted");
    }
}
