using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadThisSceneOnEnteringGame : MonoBehaviour
{
    void Start()
    {
        var saveData = SaveDataManager.Load();
        if (saveData.SceneToLoad != SceneManager.GetActiveScene().name)
        {
            SaveDataManager.ChangeSceneToLoad(SceneManager.GetActiveScene().name);
        }
    }
}
