using UnityEngine;

public class StopPlayAnimatorOnTrigger : MonoBehaviour {

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _trigger;
    
	void Start () {
		if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
        EventManager.StartListening(_trigger, () =>
        {
            _animator.enabled = false;
        });
	}
}
