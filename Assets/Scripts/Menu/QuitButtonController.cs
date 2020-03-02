using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonController : MonoBehaviour {

	public void Quit()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1f;
        }
    }
}
