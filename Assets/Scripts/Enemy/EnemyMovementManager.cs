using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementManager : MonoBehaviour {

    [SerializeField]
    private List<EnemyWaypointList> _enemyWaypoints;
    private int _currentPoint = 0;

    void Start () {
        EventManager.StartListening("EnemiesMoved", EnemyMove);
	}

    private void EnemyMove()
    {
        if(_enemyWaypoints.Count <= _currentPoint)
        {
            return;
        }
        foreach(var waypoint in _enemyWaypoints[_currentPoint].enemyWaypointsList)
        {
            waypoint.StartMoving();
        }
        _currentPoint++;
    }
}

[System.Serializable]
public class EnemyWaypointList
{
    public List<EnemyWaypoint> enemyWaypointsList;
}
