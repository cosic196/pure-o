using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {

    [SerializeField]
    private string _firstLevel;
    [SerializeField]
    private string _levelSelectLevel;

	void Start () {
        var saveData = SaveDataManager.Load();
        if(saveData.Levels.Count == 0)
        {
            SceneManager.LoadScene(_firstLevel);
        }
        else
        {
            SceneManager.LoadScene(_levelSelectLevel);
        }
    }
}
