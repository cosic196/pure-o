using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonController : MonoBehaviour {

	public void Quit()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if(activeScene == "GameLoader")
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
