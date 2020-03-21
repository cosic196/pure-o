using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class EnemyAnimationController : MonoBehaviour {

    private GameObjectEventManager _gameObjectEventManager;
    private Renderer[] _renderers;

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
        _renderers = GetComponentsInChildren<Renderer>();

        _propertyBlock = new MaterialPropertyBlock();
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].GetPropertyBlock(_propertyBlock);
        }
        _startCrossSize = _renderers[0].sharedMaterial.GetFloat(_crossSizeVarName);

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
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetFloat(_crossSizeVarName, Mathf.Lerp(_startCrossSize, _damagedCrossSize, _animationCurve.Evaluate(_timer)));
                _renderers[i].SetPropertyBlock(_propertyBlock);
            }
        }
        else if (_timer > 1f)
        {
            _timer = 1f;
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].GetPropertyBlock(_propertyBlock);
                _propertyBlock.SetFloat(_crossSizeVarName, _damagedCrossSize);
                _renderers[i].SetPropertyBlock(_propertyBlock);
            }
        }
    }

    private void AnimateDeath()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].enabled = false;
        }
    }
	
}
