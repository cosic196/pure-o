using UnityEngine;

public class SparksSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _sparksPrefab;

	void Start () {
        EventManager.StartListening("HitAWall", CreateSparks);
	}

    private void CreateSparks(string hitInfo)
    {
        string[] splitHitInfo = hitInfo.Split(',');
        Vector3 hitPosition = new Vector3(float.Parse(splitHitInfo[0]), float.Parse(splitHitInfo[1]), float.Parse(splitHitInfo[2]));
        Vector3 hitNormal = new Vector3(float.Parse(splitHitInfo[3]), float.Parse(splitHitInfo[4]), float.Parse(splitHitInfo[5]));

        var sparks = Instantiate(_sparksPrefab, hitPosition, Quaternion.LookRotation(hitNormal));
    }
}
