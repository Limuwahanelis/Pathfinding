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
    private List<Obstacle> _allActiveObstacles;

    private void Awake()
    {
        _allActiveObstacles = new List<Obstacle>();
        _obstaclePools = new List<ObjectPool<Obstacle>>(_obstacleTileTypes.Count);
        for (int i = 0; i < _obstacleTileTypes.Count; i++)
        {
            _obstaclePools.Add(new ObjectPool<Obstacle>(CreateObstacle, OnTakeObstacleFromPool, OnReturnObstacleToPool));
        }
    }

    // Start is called before the first frame update
    void Start()
    {

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
        _allActiveObstacles.Add(obstacle);
    }
    public void OnReturnObstacleToPool(Obstacle obstacle)
    {
        obstacle.gameObject.SetActive(false);
        _allActiveObstacles.Remove(obstacle);
    }
    public List<Vector2Int> GetAllActiveObstaclePositions()
    {
        List<Vector2Int> positions= new List<Vector2Int>();
        foreach(Obstacle obstacle in _allActiveObstacles)
        {
            positions.Add(new Vector2Int((int)obstacle.transform.position.x, (int)obstacle.transform.position.z));
        }
        return positions;
    }
    public List<Obstacle> GetAllActiveObstacles()
    {
        return _allActiveObstacles;
    }
}
