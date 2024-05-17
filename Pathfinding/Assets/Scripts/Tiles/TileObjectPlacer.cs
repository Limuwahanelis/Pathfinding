using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObjectPlacer : MonoBehaviour
{
    public bool IsGoalPlaced=>_isGoalPlaced;
    public bool IsStartPlaced=>_isStartPlaced;
    public PathTargetTile GoalTile=>_goalTile;
    public PathTargetTile StartTile=>_startTile;
    [SerializeField] TileSelection _tileSelector;
    [SerializeField] TileType _startTileType;
    [SerializeField] TileType _goalTileType;
    [SerializeField] PathTargetTile _goalTile;
    [SerializeField] PathTargetTile _startTile;
    [SerializeField] TileType _removeTile;
    [SerializeField] ObstaclePool _obstaclePool;
    private TileType? _tileToPlace;
    private bool _isGoalPlaced;
    private bool _isStartPlaced;
    // Start is called before the first frame update
    void Start()
    {
        _goalTile.OnTileRemoved.AddListener(RemoveGoalTile);
        _startTile.OnTileRemoved.AddListener(RemoveStartTile);
        _tileToPlace = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(_tileSelector.IsHittingMap) PlaceTile();

        }
    }

    public void SetTileTypeToPlace(TileType type)
    {
        _tileToPlace = type;
    }
    public void PlaceTile()
    {
        Debug.Log(_tileSelector.SelectedTile);

        if(_tileSelector.SelectedTile !=null) _tileSelector.SelectedTile.RemoveTile();
        if (_tileToPlace == _removeTile) return;
        if (_tileToPlace == _goalTileType)
        {
            _goalTile.transform.position = _tileSelector.SelectedTilePos;
            _goalTile.gameObject.SetActive(true);
            _isGoalPlaced = true;
        }
        else if (_tileToPlace == _startTileType) 
        {
            _startTile.transform.position = _tileSelector.SelectedTilePos;
            _startTile.gameObject.SetActive(true);
            _isStartPlaced = true;
        }
        else
        {
            _obstaclePool.SetCurrentObstacleType((ObstacleTileType)_tileToPlace);
            _obstaclePool.GetObstacle().transform.position = _tileSelector.SelectedTilePos;
        }
    }

    private void RemoveGoalTile()=>_isGoalPlaced = false;
    private void RemoveStartTile()=>_isStartPlaced = false;

    private void OnDestroy()
    {
        _goalTile.OnTileRemoved.RemoveListener(RemoveGoalTile);
        _startTile.OnTileRemoved.RemoveListener(RemoveStartTile);
    }

}

