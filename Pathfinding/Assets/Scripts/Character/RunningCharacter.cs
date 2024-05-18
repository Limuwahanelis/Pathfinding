using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningCharacter : MonoBehaviour
{
    //[SerializeField] AStarPathfinding _pathFinding;
    //[SerializeField] TileMapSetter _map;
    [SerializeField] float _speed;
    [SerializeField] GameObject _characterModel;
    private List<Vector3> _positionsToVisit;
    private int _currentPositionIndex;
    private Vector3 _startPos;
    private float _lerp = 0;
    private float _timetoReachTarget;
    private bool _run;
    // Start is called before the first frame update
    void Start()
    {
        _characterModel.SetActive(false);
        //_pathFinding.OnPathCreated.AddListener( SetPath);
        _positionsToVisit = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_run) return;
        transform.position = Vector3.Lerp(_startPos, _positionsToVisit[_currentPositionIndex], _lerp / _timetoReachTarget);
        _lerp =_lerp+ Time.deltaTime;
        if(_lerp / _timetoReachTarget>1)
        {
            if (_currentPositionIndex >= _positionsToVisit.Count-1) 
            {
                _currentPositionIndex = 0;
                transform.position = _positionsToVisit[_currentPositionIndex];
                //_startPos = _positionsToVisit[0];
            }
            _startPos = _positionsToVisit[_currentPositionIndex];
            _currentPositionIndex++;
            transform.LookAt(_positionsToVisit[_currentPositionIndex]);
            _timetoReachTarget = Vector3.Distance(_startPos, _positionsToVisit[_currentPositionIndex]) / _speed;
            _lerp = 0;
        }

    }

    public void SetPath(List<Vector2Int> path)
    {
        if (path.Count == 0) return;
        _run = true;
        _characterModel.SetActive(true);
        _positionsToVisit.Clear();
        
        for(int i=0;i< path.Count;i++) 
        {
            _positionsToVisit.Add( new Vector3(path[i].x, transform.position.y, path[i].y));
        }
        _currentPositionIndex = 1;
        _startPos = _positionsToVisit[0];
        transform.position = _startPos;
        _timetoReachTarget = Vector3.Distance(_startPos, _positionsToVisit[_currentPositionIndex])/_speed;
        transform.LookAt(_positionsToVisit[1]);
        gameObject.SetActive(true);
    }
    public void HideCharacter()
    {
        _run = false;
        _characterModel.SetActive(false);
    }
    //public void CheckIfPathIsOnMap()
    //{
    //    foreach (Vector3 pos in _positionsToVisit)
    //    {
    //        if (pos.x > _map.GridSize.x - 1 || pos.z > _map.GridSize.y - 1)
    //        {
    //            _run = false;
    //            _characterModel.SetActive(false);
    //        }
    //    }
    //}
    private void OnDestroy()
    {
        //_pathFinding.OnPathCreated.RemoveListener(SetPath);
    }
}
