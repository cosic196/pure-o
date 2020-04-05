using UnityEngine;

[RequireComponent(typeof(GameObjectEventManager))]
public class EnemyDisappearController : MonoBehaviour {

    private bool _appeared;
    private float _index;
    private GameObjectEventManager _gameObjectEventManager;

	void Start () {
        _gameObjectEventManager = GetComponent<GameObjectEventManager>();
        _appeared = false;
        _gameObjectEventManager.StartListening("Appeared", Appeared);
        EventManager.StartListening("EnemiesDisappearedKoreo", Disappear);
	}

    private void Disappear(string indices)
    {
        if(!_appeared)
        {
            return;
        }
        string[] indicesSplit = indices.Split(',');
        for (int i = 0; i < indicesSplit.Length; i++)
        {
            var indicesRange = indicesSplit[i].Split('-');
            if(indicesRange.Length == 2)
            {
                if(_index >= float.Parse(indicesRange[0]) && _index <= float.Parse(indicesRange[1]))
                {
                    gameObject.SetActive(false);
                }
            }
            else if(indicesRange.Length == 1)
            {
                if(_index == float.Parse(indicesRange[0]))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void Appeared(string index)
    {
        _index = float.Parse(index);
        _appeared = true;
    }
}
