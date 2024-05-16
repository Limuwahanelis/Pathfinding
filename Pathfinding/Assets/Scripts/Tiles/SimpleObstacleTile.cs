using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SimpleObstacleTile : Obstacle
{
    public override void RemoveTile()
    {
        ReturnToPool();
    }
}
