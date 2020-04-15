using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevelOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _levelName;
    [SerializeField]
    private string _trigger;

	void Start () {
        EventManager.StartListening(_trigger, () =>
        {
            SceneManager.LoadScene(_levelName);
        });
	}
}
