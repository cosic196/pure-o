using EZCameraShake;
using UnityEngine;

public class CameraAnimation : MonoBehaviour {

	void Start () {
        EventManager.StartListening("PlayerDamaged", () => CameraShaker.Instance.ShakeOnce(2f, 3f, .01f, .3f));
	}
}
