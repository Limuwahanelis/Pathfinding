using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Search;

[CreateAssetMenu(menuName ="Obstacle Tile Type")]
public class ObstacleTileType : TileType
{
    public GameObject ObstaclePrefab => _obstaclePrefab;
    [SerializeField, SearchContext("t:Obstacle")] GameObject _obstaclePrefab;
}
