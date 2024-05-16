using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

public class ObstaclePool : MonoBehaviour
{
    [SerializeField] List<ObstacleTileType> _obstacleTileTypes;
    private List<ObjectPool<Obstacle>> _obstaclePools;
    private ObstacleTileType _currentObstacleTileType;
    // Start is called before the first frame update
    void Start()
    {
        _obstaclePools = new List<ObjectPool<Obstacle>>(_obstacleTileTypes.Count);
        for(int i=0;i<_obstacleTileTypes.Count;i++) 
        {
            _obstaclePools.Add(new ObjectPool<Obstacle>(CreateObstacle, OnTakeObstacleFromPool, OnReturnObstacleToPool));
        }
    }
    public void SetCurrentObstacleType(ObstacleTileType obstacleTileType)
    {
        _currentObstacleTileType=obstacleTileType;
    }
    public Obstacle GetObstacle()
    {
        int poolIndex = _obstacleTileTypes.FindIndex((x) => x == _currentObstacleTileType);
        return _obstaclePools[poolIndex].Get();
    }

    Obstacle CreateObstacle()
    {
        int poolIndex = _obstacleTileTypes.FindIndex((x) => x == _currentObstacleTileType);
        Obstacle obstacle = Instantiate(_currentObstacleTileType.ObstaclePrefab).GetComponent<Obstacle>();
        obstacle.SetPool(_obstaclePools[poolIndex]);
        return obstacle;
    }
    public void OnTakeObstacleFromPool(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(true);
    }
    public void OnReturnObstacleToPool(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
    }
}
