using UnityEngine;

public class PauseMenuAnimation : MonoBehaviour {

    private Animator _animator;

	void Start () {
        _animator = GetComponent<Animator>();
        EventManager.StartListening("Paused", Pause);
        EventManager.StartListening("UnpauseStarted", Unpause);
	}

    private void Unpause()
    {
        _animator.SetTrigger("Unpaused");
    }

    private void Pause()
    {
        _animator.SetTrigger("Paused");
    }

    private void UnpauseFinished()
    {
        EventManager.TriggerEvent("Unpaused");
    }
}
