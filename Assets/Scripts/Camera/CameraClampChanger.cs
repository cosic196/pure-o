using System.Collections.Generic;
using UnityEngine;

public class CameraClampChanger : MonoBehaviour
{
    [SerializeField]
    private FpsCameraMovementController _fpsCameraMovementController;
    [SerializeField]
    private List<Vector2> _clampSeries;
    [SerializeField]
    private string _trigger;
    private int _counter = 0;

    void Start()
    {
        if(!string.IsNullOrEmpty(_trigger))
        {
            EventManager.StartListening(_trigger, ChangeClamp);
        }
    }

    public void ChangeClamp()
    {
        if(_counter >= _clampSeries.Count)
        {
            return;
        }
        _fpsCameraMovementController.clampInDegrees = _clampSeries[_counter];
        _counter++;
    }
}
