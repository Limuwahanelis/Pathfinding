using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] TileMapSetter _tileMapSetter;
    [SerializeField] float _minXpos;
    [SerializeField] float _maxXpos;
    [SerializeField] float _minZpos;
    [SerializeField] float _maxZpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMoveSpeed(float newSpeed)
    {
        _moveSpeed = newSpeed;
    }
    public void Move(Vector2 direction)
    {
        transform.Translate(direction.x * Time.deltaTime * _moveSpeed, 0, direction.y * Time.deltaTime * _moveSpeed);
        //if(transform.position.y<0.05f)
        //{
        //    transform.position = new Vector3(transform.position.x,0.05f,transform.position.z);
        //}
        //if(transform.position.x< _minXpos)
        //{
        //    transform.position = new Vector3(_minXpos, transform.position.y, transform.position.z);
        //}
        //if (transform.position.z < _minZpos)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, _minZpos);
        //}
        //if (transform.position.x > _maxXpos)
        //{
        //    transform.position = new Vector3(_maxXpos, transform.position.y, transform.position.z);
        //}
        //if (transform.position.z > _maxZpos)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y, _maxZpos);
        //}
    }
}
