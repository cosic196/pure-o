using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SlowDownPostProcessing : MonoBehaviour
{
    [SerializeField]
    private float _saturation;
    [SerializeField]
    private float _contrast;

    private ColorGrading _colorGrading;
    private ChromaticAberration _chromaticAberration;
    private PostProcessVolume _postProcessVolume1, _postProcessVolume2;
    private GameObjectEventManager _gameObjectEventManager;

    void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();

        _colorGrading = ScriptableObject.CreateInstance<ColorGrading>();
        _colorGrading.enabled.Override(true);
        _colorGrading.saturation.Override(0f);
        _colorGrading.contrast.Override(0f);

        _chromaticAberration = ScriptableObject.CreateInstance<ChromaticAberration>();
        _chromaticAberration.enabled.Override(true);
        _chromaticAberration.fastMode.Override(true);
        _chromaticAberration.intensity.Override(0f);
        
        _postProcessVolume1 = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _colorGrading);
        _postProcessVolume2 = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _chromaticAberration);

        _gameObjectEventManager.StartListening("Animate", Animate);
    }

    private void Animate(string timerString)
    {
        float timer = float.Parse(timerString);

        _colorGrading.saturation.value = Mathf.Lerp(_saturation, 0f, timer);
        _colorGrading.contrast.value = Mathf.Lerp(_contrast, 0f, timer);
        _chromaticAberration.intensity.value = Mathf.Lerp(1f, 0f, timer);
    }

    private void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(_postProcessVolume1, true);
        RuntimeUtilities.DestroyVolume(_postProcessVolume2, true);
    }
}
