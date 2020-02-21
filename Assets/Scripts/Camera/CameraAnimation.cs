using EZCameraShake;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {

    void Start() {
        EventManager.StartListening("PlayerDamaged", () => CameraShaker.Instance.ShakeOnce(2f, 2.5f, .01f, .3f));
        EventManager.StartListening("Shot", () => CameraShaker.Instance.ShakeOnce(1f, 3f, 0f, .25f));
	}
}
