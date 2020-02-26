using UnityEngine;

public class DeathController : MonoBehaviour {

    [SerializeField]
    private float _slowdownSpeed;
    [SerializeField]
    private AnimationCurve _animationCurve;
    private float _timer = 1f;

	void Start () {
        _timer = 1f;
        EventManager.StartListening("PlayerDied", Death);
	}

    private void Death()
    {
        _timer = 0f;
    }

    private void Update()
    {
        if(_timer < 1f)
        {
            Time.timeScale = Mathf.Lerp(1f, 0f, _animationCurve.Evaluate(_timer));
            _timer += Time.unscaledDeltaTime;
        }
        if(_timer > 1f)
        {
            _timer = 1f;
            Time.timeScale = 0f;
        }
    }
}
