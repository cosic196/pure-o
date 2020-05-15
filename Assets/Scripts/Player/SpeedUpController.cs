using UnityEngine;

public class SpeedUpController : MonoBehaviour
{
    [SerializeField]
    private KeyCode _speedUpKey;
    [SerializeField]
    [Range(1f, 10f)]
    private float _speedUpAmount;
    private bool _paused = false;

    private void Start()
    {
        EventManager.StartListening("Paused", Pause);
        EventManager.StartListening("Unpaused", Unpause);
    }

    void Update()
    {
        if(_paused)
        {
            return;
        }
        if (Input.GetKeyDown(_speedUpKey))
        {
            Enable();
        }
        if (Input.GetKeyUp(_speedUpKey))
        {
            Disable();
        }
    }

    private void Enable()
    {
        CustomTime._speedUpTimeScale = _speedUpAmount;
        Time.timeScale += _speedUpAmount;
    }

    private void Disable()
    {
        if (CustomTime._speedUpTimeScale == 0f)
        {
            return;
        }
        CustomTime._speedUpTimeScale = 0f;
        Time.timeScale -= _speedUpAmount;
    }

    private void Unpause()
    {
        _paused = false;
    }

    private void Pause()
    {
        _paused = true;
        Disable();
    }
}
