using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour {

    private void Start()
    {
        EventManager.StartListening("Restarted", Restart);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
