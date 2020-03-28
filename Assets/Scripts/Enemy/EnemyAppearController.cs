using System.Collections.Generic;
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
    private float _timer;

	void Start () {
        for (int i = 0; i < _renderers.Count; i++)
        {
            _renderers[i].enabled = false;
        }
        for (int i = 0; i < _colliders.Count; i++)
        {
            _colliders[i].gameObject.layer = 2;
        }
        _timer = _timeToAppear;
        EventManager.StartListening("EnemiesAppearedKoreo", StartAppearingIfOnIndex);
    }

    private void StartAppearingIfOnIndex(string indexString)
    {
        if(float.Parse(indexString) == _appearIndex)
        {
            _particleSystem.Play();
            _timer = 0;
            EventManager.StopListening("EnemiesAppearedKoreo", StartAppearingIfOnIndex);
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
		if(_timer < _timeToAppear)
        {
            _timer += CustomTime.GetDeltaTime();
        }
        else if(_timer > _timeToAppear)
        {
            Appear();
            _timer = _timeToAppear;
        }
	}
}
