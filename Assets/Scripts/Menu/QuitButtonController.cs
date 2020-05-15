using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonController : MonoBehaviour {

	public void Quit()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if(activeScene == "LevelSelectScene" ||
            activeScene == "BrainScene" || activeScene == "GameLoader")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene("GameLoader");
            Time.timeScale = 1f;
        }
    }
}
