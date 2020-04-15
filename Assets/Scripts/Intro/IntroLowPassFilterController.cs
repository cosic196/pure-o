using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioLowPassFilter))]
public class IntroLowPassFilterController : MonoBehaviour
{

    private AudioLowPassFilter _lowPass;
    [SerializeField]
    [Range(500, 10000)]
    private float _minValue;
    [SerializeField]
    [Range(10000, 22000)]
    private float _maxValue;
    [SerializeField]
    private AnimationCurve _curve;

    void Start()
    {
        _lowPass = GetComponent<AudioLowPassFilter>();
        EventManager.StartListening("HpChanged", AdjustFilter);
        EventManager.StartListening("BrainLostLife", SetFilterToMin);
    }

    private void AdjustFilter(string hpSlashMaxHp)
    {
        float currentHp = float.Parse(hpSlashMaxHp.Split('/')[0]);
        float maxHp = float.Parse(hpSlashMaxHp.Split('/')[1]);
        _lowPass.cutoffFrequency = Mathf.Lerp(_minValue, _maxValue, _curve.Evaluate(currentHp / maxHp));
    }

    private void SetFilterToMin(string _)
    {
        _lowPass.cutoffFrequency = _minValue;
    }
}
