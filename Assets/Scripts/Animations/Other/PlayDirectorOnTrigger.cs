using UnityEngine;
using UnityEngine.Playables;

public class PlayDirectorOnTrigger : MonoBehaviour {

    [SerializeField]
    private string _triggerName;
    [SerializeField]
    private PlayableDirector _playableDirector;

	void Start () {
        EventManager.StartListening(_triggerName, Play);
	}

    private void Play()
    {
        _playableDirector.Play();
        enabled = false;
    }

    private void OnDisable()
    {
        EventManager.StopListening(_triggerName, Play);
    }
}
