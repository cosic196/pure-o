using System.Collections;
using UnityEngine;

public class BrainShaderController : MonoBehaviour {

    [SerializeField]
    private float _maxCrossSize;
    [SerializeField]
    private float _minCrossSize;
    private MaterialPropertyBlock _propertyBlock;
    private Renderer _renderer;
    private string _crossSizeVarName = "_Size";
    private AnimationCurve _curve;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
        _propertyBlock = new MaterialPropertyBlock();
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat(_crossSizeVarName, _minCrossSize);
        _renderer.SetPropertyBlock(_propertyBlock);
        _curve = AnimationCurve.Linear(1, _maxCrossSize, 0, _minCrossSize);
        EventManager.StartListening("HpChanged", ChangeCrossSize);
        EventManager.StartListening("BrainLostLife", SetCrossSizeToMin);
    }

    private void SetCrossSizeToMin(string _)
    {
        StartCoroutine(MinCrossSizeRoutine());
    }

    private IEnumerator MinCrossSizeRoutine()
    {
        yield return 0;
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat(_crossSizeVarName, _minCrossSize);
        _renderer.SetPropertyBlock(_propertyBlock);
        yield return null;
    }

    private void ChangeCrossSize(string hpByMaxHp)
    {
        float hpPercentage = float.Parse(hpByMaxHp.Split('/')[0]) / float.Parse(hpByMaxHp.Split('/')[1]);
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat(_crossSizeVarName, _curve.Evaluate(hpPercentage));
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
