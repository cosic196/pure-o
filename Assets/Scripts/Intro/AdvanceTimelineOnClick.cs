using UnityEngine;
using UnityEngine.Playables;

public class AdvanceTimelineOnClick : MonoBehaviour {

    [SerializeField]
    private string _pausePipelineEvent;
    [SerializeField]
    private PlayableDirector _playableDirector;
    
	void Start () {
        _playableDirector.Play();
        EventManager.StartListening("EndIntro", EndIntro);
        EventManager.StartListening(_pausePipelineEvent, () =>
        {
            _playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        });
	}

    private void EndIntro()
    {
        EventManager.TriggerEvent("FadeOutToBlack", "LoadNextLevel");
        EventManager.TriggerEvent("FadeOutMusic");
    }

    void Update () {
        if (Input.anyKeyDown)
        {
            if(_playableDirector.state == PlayState.Playing)
            _playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }
}
