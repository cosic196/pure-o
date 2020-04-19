using UnityEngine;

public class MapController : MonoBehaviour {

    MapPointController[] mapPointControllers;

	void Start () {
        mapPointControllers = GetComponentsInChildren<MapPointController>(true);
        SaveData saveData = SaveDataManager.Load();

        for (int i = 0; i < mapPointControllers.Length; i++)
        {
            foreach (var level in saveData.Levels)
            {
                if(level.Name == mapPointControllers[i]._levelName)
                {
                    if(level.Completed)
                    {
                        mapPointControllers[i].gameObject.SetActive(true);
                        mapPointControllers[i].Completed();
                    }
                    else
                    {
                        mapPointControllers[i].gameObject.SetActive(true);
                    }
                }
            }
        }
	}
}
