using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class EnemyAppearController : MonoBehaviour {

    [Space(20)]
    [SerializeField]
    private float _appearIndex;

    [Space(20)]

    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private List<Renderer> _renderers;
    [SerializeField]
    private List<Collider> _colliders;
    [SerializeField]
    private float _timeToAppear;
    [SerializeField]
    private float _warmUpTime;
    private float _appearTimer, _warmUpTimer;
    private GameObjectEventManager _gameObjectEventManager;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        UnityEditor.Handles.BeginGUI();
        var restoreTextColor = GUI.color;
        var restoreBackColor = GUI.backgroundColor;

        GUI.color = Color.green;
        GUI.backgroundColor = Color.magenta;

        var view = UnityEditor.SceneView.currentDrawingSceneView;
        if (view != null && view.camera != null)
        {
            Vector3 screenPos = view.camera.WorldToScreenPoint(transform.position);
            if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
            {
                GUI.color = restoreTextColor;
                UnityEditor.Handles.EndGUI();
                return;
            }
            Vector2 size = GUI.skin.label.CalcSize(new GUIContent(_appearIndex.ToString()));
            var r = new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y);
            GUI.Box(r, _appearIndex.ToString(), EditorStyles.numberField);
            GUI.Label(r, _appearIndex.ToString());
            GUI.color = restoreTextColor;
            GUI.backgroundColor = restoreBackColor;
        }
        UnityEditor.Handles.EndGUI();
    }
#endif

    void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].enabled = false;
        }
        for (int i = 0; i < _colliders.Count; i++)
        {
            _colliders[i].gameObject.layer = 2;
        }
        _appearTimer = _timeToAppear;
        _warmUpTimer = 0f;
        EventManager.StartListening("EnemiesAppearedKoreo", StartAppearingIfOnIndex);
    }

    private void StartAppearingIfOnIndex(string indexString)
    {
        if(float.Parse(indexString) == _appearIndex)
        {
            gameObject.SetActive(true);
            _particleSystem.Play();
            _appearTimer = 0;
            EventManager.StopListening("EnemiesAppearedKoreo", StartAppearingIfOnIndex);
            _gameObjectEventManager.TriggerEvent("Appeared", _appearIndex.ToString());
        }
    }

    private void Appear()
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].enabled = true;
        }
        for (int i = 0; i < _colliders.Count; i++)
        {
            _colliders[i].gameObject.layer = 0;
        }
    }

	void Update () {
		if(_appearTimer < _timeToAppear)
        {
            _appearTimer += CustomTime.GetDeltaTime();
        }
        else if(_appearTimer > _timeToAppear)
        {
            Appear();
            _appearTimer = _timeToAppear;
        }
        if(_warmUpTimer < _warmUpTime)
        {
            _warmUpTimer += CustomTime.GetDeltaTime();
        }
        else if(_warmUpTimer > _warmUpTime)
        {
            gameObject.SetActive(false);
            _warmUpTimer = _warmUpTime;
        }
	}
}
