using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TriggerAnimationOnTrigger : MonoBehaviour {

    private Animator _animator;
    [SerializeField]
    private string _eventTrigger;
    [SerializeField]
    private string _animationTrigger;

	void Start () {
        _animator = GetComponent<Animator>();
        EventManager.StartListening(_eventTrigger, TriggerAnimation);
	}

    private void TriggerAnimation()
    {
        _animator.SetTrigger(_animationTrigger);
    }
}
