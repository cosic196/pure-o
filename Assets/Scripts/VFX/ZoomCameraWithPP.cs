using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ZoomCameraWithPP : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField]
    [Range(1f, 10f)]
    private float _zoom;
    [Space]
    [Header("Look & Feel")]
    [SerializeField]
    private AnimationCurve _animationCurve;
    [SerializeField]
    private float _transitionSpeed;

    private LensDistortion _lensDistortion;
    private PostProcessVolume _postProcessVolume;
    private GameObjectEventManager _gameObjectEventManager;

    private float _transitionTimer = 0f;
    private int _transitionAddMultiplier = 0;

    void Start()
    {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();

        _lensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
        _lensDistortion.enabled.Override(true);
        _lensDistortion.scale.Override(1f);

        _postProcessVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _lensDistortion);

        _gameObjectEventManager.StartListening("PressedZoom", StartZoom);
        _gameObjectEventManager.StartListening("UnpressedZoom", StopZoom);
    }

    private void StartZoom()
    {
        _transitionAddMultiplier = 1;
    }
    
    private void StopZoom()
    {
        _transitionAddMultiplier = -1;
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if(_transitionTimer < 0f)
        {
            _transitionTimer = 0f;
            _transitionAddMultiplier = 0;
        }
        else if(_transitionTimer > 1f)
        {
            _transitionTimer = 1f;
            _transitionAddMultiplier = 0;
        }

        _transitionTimer += Time.deltaTime * _transitionSpeed * _transitionAddMultiplier;
        _lensDistortion.scale.value = Mathf.Lerp(1f, _zoom, _animationCurve.Evaluate(_transitionTimer));
    }

    private void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(_postProcessVolume, true);
    }
}
