using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ShootAnimationController : MonoBehaviour {

    private Animator _animator;
    [SerializeField]
    private string _animationTrigger;
    [SerializeField]
    private ParticleSystem _particleSystem;

	void Start () {
        _animator = GetComponent<Animator>();
        EventManager.StartListening("Shot", StartAnimation);
	}

    private void StartAnimation()
    {
        _animator.SetTrigger(_animationTrigger);
        _particleSystem.Play();
    }
}
