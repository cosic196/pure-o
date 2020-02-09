using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartController : MonoBehaviour {

    private void Start()
    {
        EventManager.StartListening("Restarted", Restart);
        EventManager.StartListening("PlayerDied", Restart);
    }

    public void Restart()
    {
        CustomTime._customScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
