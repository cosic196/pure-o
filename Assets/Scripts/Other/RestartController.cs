using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour {

    private void Start()
    {
        EventManager.StartListening("Restarted", Restart);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
