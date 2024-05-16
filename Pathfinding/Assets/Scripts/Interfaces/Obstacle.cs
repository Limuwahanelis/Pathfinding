using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Obstacle: MapTile
{
    protected IObjectPool<Obstacle> _pool;

    public void SetPool(IObjectPool<Obstacle> pool) => _pool = pool;
    public void ReturnToPool() => _pool.Release(this);
}
