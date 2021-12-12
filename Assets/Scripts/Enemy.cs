using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _transform;
    [SerializeField] private Sprite _defaultW;
    [SerializeField] private Sprite _defaultE;
    [SerializeField] private Sprite _defaultN;
    [SerializeField] private Sprite _defaultS;
    [SerializeField] private Sprite _damagedW;
    [SerializeField] private Sprite _damagedE;
    [SerializeField] private Sprite _damagedN;
    [SerializeField] private Sprite _damagedS;
    private List<Vector2> _direction = new List<Vector2> { Vector2.up,Vector2.down
                                                                        ,Vector2.left,Vector2.right};
    [SerializeField] private List<Sprite> _defaultSpr;
    [SerializeField] private List<Sprite> _damagedSpr;
 
    private Vector2 _currentDir;
    [SerializeField] private float _moveSpeed;
    private bool _isDamaged;
    private void Start()
    {
        _defaultSpr = new List<Sprite> { _defaultN, _defaultS, _defaultW, _defaultE };
        _damagedSpr = new List<Sprite> { _damagedN, _damagedS, _damagedW, _damagedE };
        ChangeDirection();

    }
    private void FixedUpdate()
    {
        MoveAround();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Explosion")
        {
            _spriteRenderer.sprite = _damagedSpr[_direction.IndexOf(_currentDir)];
            _isDamaged = true;

        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Undestroyable" || collision.collider.tag == "Destroyable")
        {
            ChangeDirection();

        }
    }
    //public void OnCollisionExit2D(Collision2D collision)
    //{

    //    if (collision.collider.tag == "Undestroyable" || collision.collider.tag == "Destroyable")
    //    {
    //        _currentDir = _direction[UnityEngine.Random.Range(1, 4)];
    //    }
    //}

    public void MoveAround()
    {
        float posX = _currentDir.x * _moveSpeed * Time.deltaTime;
        float posY = _currentDir.y * _moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(posX, posY, 0));
        if (UnityEngine.Random.Range(0, 60) == 5)
        {
            ChangeDirection();


        }
    }
    public void ChangeDirection()
    {
        int randDir = UnityEngine.Random.Range(0,4);
        while (_currentDir ==_direction[randDir])
        {
            randDir = UnityEngine.Random.Range(0,4);

        }
        _currentDir = _direction[randDir];

        if (_isDamaged)
        {
            _spriteRenderer.sprite = _damagedSpr[randDir];
        }
        else
        {
            _spriteRenderer.sprite = _defaultSpr[randDir];
        }

        
    }
}