using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileObjectPlacer : MonoBehaviour
{
    public UnityEvent<MapTile> OnTilePlaced;
    public bool IsGoalPlaced=>_isGoalPlaced;
    public bool IsStartPlaced=>_isStartPlaced;
    public PathTargetTile GoalTile=>_goalTile;
    public PathTargetTile StartTile=>_startTile;
    [SerializeField] TileSelection _tileSelector;
    [SerializeField] TileType _startTileType;
    [SerializeField] TileType _goalTileType;
    [SerializeField] TileType _removeTile;
    [SerializeField] PathTargetTile _goalTile;
    [SerializeField] PathTargetTile _startTile;
    [SerializeField] ObstaclePool _obstaclePool;
    [SerializeField] TileMapSetter _map;
    private TileType _tileToPlace;
    private bool _isGoalPlaced;
    private bool _isStartPlaced;
    // Start is called before the first frame update
    void Start()
    {
        _goalTile.OnTileRemoved.AddListener(RemoveGoalTile);
        _startTile.OnTileRemoved.AddListener(RemoveStartTile);
        _tileToPlace = _removeTile;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetTileTypeToPlace(TileType type)
    {
        _tileToPlace = type;
    }
    public void PlaceTile()
    {
        if (!_tileSelector.IsHittingMap) return;

        if(_tileSelector.SelectedTile !=null) _tileSelector.SelectedTile.RemoveTile();
        if (_tileToPlace == _removeTile) return;

        if (_tileToPlace == _goalTileType)
        {
            _goalTile.RemoveTile();
            _goalTile.transform.position = _tileSelector.SelectedTilePos;
            _goalTile.gameObject.SetActive(true);
            _isGoalPlaced = true;
            OnTilePlaced?.Invoke(_goalTile);
        }
        else if (_tileToPlace == _startTileType) 
        {
            _startTile.RemoveTile();
            _startTile.transform.position = _tileSelector.SelectedTilePos;
            _startTile.gameObject.SetActive(true);
            _isStartPlaced = true;
            OnTilePlaced?.Invoke(_startTile);
        }
        else
        {
           _obstaclePool.SetCurrentObstacleType((ObstacleTileType)_tileToPlace);
            MapTile tile = _obstaclePool.GetObstacle();
            tile.transform.position = _tileSelector.SelectedTilePos;
            OnTilePlaced?.Invoke(tile);
        }
        
    }
    public void HideTilesOutsideMap()
    {
        if (_goalTile.transform.position.x > _map.GridSize.x - 1 || _goalTile.transform.position.z > _map.GridSize.y - 1) _goalTile.RemoveTile();
        if (_startTile.transform.position.x > _map.GridSize.x - 1 || _startTile.transform.position.z > _map.GridSize.y - 1) _startTile.RemoveTile();
    }
    private void RemoveGoalTile()=>_isGoalPlaced = false;
    private void RemoveStartTile()=>_isStartPlaced = false;

    private void OnDestroy()
    {
        _goalTile.OnTileRemoved.RemoveListener(RemoveGoalTile);
        _startTile.OnTileRemoved.RemoveListener(RemoveStartTile);
    }

}

