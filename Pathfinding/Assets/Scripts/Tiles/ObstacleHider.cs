using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHider : MonoBehaviour
{
    [SerializeField] ObstaclePool _obstaclePool;
    [SerializeField] TileMapSetter _tileMapSetter;

    public void HideObstaclesOutsideMap()
    {
        List<Obstacle> obstacles= new List<Obstacle>( _obstaclePool.GetAllActiveObstacles());
        foreach(Obstacle obstacle in obstacles)
        {
            if(obstacle.transform.position.x >_tileMapSetter.GridSize.x-1 || obstacle.transform.position.z > _tileMapSetter.GridSize.y-1) obstacle.ReturnToPool();
        }
    }
}
