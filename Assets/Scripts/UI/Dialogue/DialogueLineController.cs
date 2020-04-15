using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class DialogueLineController : MonoBehaviour {

    private LineRenderer _lineRenderer;
    [SerializeField]
    private Transform _basePointTransform;
    [SerializeField]
    private Transform _targetPointTransform;

	void Start () {
        _lineRenderer = GetComponent<LineRenderer>();
	}
	
	void Update () {
        _lineRenderer.SetPosition(0, _basePointTransform.position);
        _lineRenderer.SetPosition(1, _targetPointTransform.position);
	}
}
