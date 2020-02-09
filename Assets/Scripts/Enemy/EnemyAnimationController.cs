using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
[RequireComponent(typeof(Renderer))]
public class EnemyAnimationController : MonoBehaviour {

    private GameObjectEventManager _gameObjectEventManager;
    private Renderer _renderer;

    private MaterialPropertyBlock _propertyBlock;
    private float _timer = 0f;
    [SerializeField]
    private float _speed;
    [SerializeField]
    AnimationCurve _animationCurve;
    [SerializeField]
    private float _damagedCrossSize;
    private float _startCrossSize;
    private string _crossSizeVarName = "_Size";
    private bool _shot = false;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _renderer = GetComponent<Renderer>();

        _propertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propertyBlock);
        _startCrossSize = _renderer.sharedMaterial.GetFloat(_crossSizeVarName);

        _gameObjectEventManager.StartListening("Died", AnimateDeath);
        _gameObjectEventManager.StartListening("Damaged", Damaged);
	}

    private void Update()
    {
        AnimateDamaged();
    }

    private void Damaged(string hp)
    {
        if(!_shot)
        {
            _shot = true;
        }
    }

    private void AnimateDamaged()
    {
        if(!_shot)
        {
            return;
        }
        if(_timer < 1f)
        {
            _timer += CustomTime.GetDeltaTime() * _speed;
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat(_crossSizeVarName, Mathf.Lerp(_startCrossSize, _damagedCrossSize, _animationCurve.Evaluate(_timer)));
            _renderer.SetPropertyBlock(_propertyBlock);
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetFloat(_crossSizeVarName, _damagedCrossSize);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }

    private void AnimateDeath()
    {
        _renderer.enabled = false;
    }
	
}
