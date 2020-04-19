using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _levelName;
    [SerializeField]
    private string _trigger;

	void Start () {
        EventManager.StartListening(_trigger, LoadLevelAsync);
        EventManager.StartListening(_trigger, LoadLevelAsyncWithParam);
	}

    private void LoadLevelAsync()
    {
        EventManager.TriggerEvent("StartedLoadingNextLevel");
        SceneManager.LoadSceneAsync(_levelName);
    }

    private void LoadLevelAsyncWithParam(string levelName)
    {
        EventManager.TriggerEvent("StartedLoadingNextLevel");
        SceneManager.LoadSceneAsync(levelName);
    }
}
