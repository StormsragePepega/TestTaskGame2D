using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRend;
    [SerializeField] public FixedJoystick joystick;
    [SerializeField] private Transform _transform;
    public Sprite W;
    public Sprite N;
    public Sprite E;
    public Sprite S;
    [SerializeField] private GameObject _bomb;
    [SerializeField] private int _capacityBomb;
    [SerializeField] private int _powerBomb=3;


    [SerializeField] private float _movementSpeed=2f;
    private Vector2 _moveInput;
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(joystick.Horizontal * _movementSpeed, joystick.Vertical * _movementSpeed, 0);
        if (Math.Abs(joystick.Vertical) > Math.Abs(joystick.Horizontal))
        {
            if (joystick.Vertical > 0)
            {
                _spriteRend.sprite = N;
            }
            else if (joystick.Vertical < 0)
            {
                _spriteRend.sprite = S;
            }
        }
        else
        {
            if (joystick.Horizontal < 0)
            {
                _spriteRend.sprite = W;
            }
            else if (joystick.Horizontal > 0)
            {
                _spriteRend.sprite = E;
            }
        }
    }



    public void AddBomb( int value)
    {
        _capacityBomb += value;
    }
    public void AddPower(int value)
    {
        _powerBomb += value;
    }
    public int GetPower()
    {
        return this._powerBomb;
    }
    public int GetBomb()
    {
        return _capacityBomb;
    }
    public void DropBomb()
    {
        if (_bomb)
        { //Check if bomb prefab is assigned first
          

            
            Instantiate(_bomb, AimBomb(new Vector3(_transform.position.x,_transform.position.y,0)),_bomb.transform.rotation);

        }
    }
    private Vector3 AimBomb(Vector3 place)//presicion for bomb placement
    {
        float placementPositionY = _transform.position.y;
        float placementPositionX = _transform.position.x;
        if (_transform.position.x % 1 > 0.52f)
        {
            placementPositionX = Mathf.RoundToInt(placementPositionX) - 0.5f;
        }
        else if (_transform.position.y % 1 < 0.48f)
        {
            placementPositionX = Mathf.RoundToInt(placementPositionX) + 0.5f;
        }
        else if(_transform.position.x < 0 && _transform.position.x<(-0.52f))
        {
            placementPositionX = Mathf.RoundToInt(placementPositionX) + 0.5f;
        }
        else if(_transform.position.x  < 0 && _transform.position.x > (-0.48f))
        {
            placementPositionX = Mathf.RoundToInt(placementPositionX) - 0.5f;
        }
        else
        {
            placementPositionX = _transform.position.x;
        }
        if (_transform.position.y % 1 > 0.52f)
        {
            placementPositionY = Mathf.RoundToInt(placementPositionY) - 0.5f;
        }
        else if (_transform.position.y % 1 < 0.48f)
        {
            placementPositionY = Mathf.RoundToInt(placementPositionY) + 0.5f;
        }
        else if (_transform.position.y < 0 && _transform.position.y < (-0.52f))
        {
            placementPositionY = Mathf.RoundToInt(placementPositionY) + 0.5f;
        }
        else if (_transform.position.y < 0 && _transform.position.y > (-0.48f))
        {
            placementPositionY = Mathf.RoundToInt(placementPositionY) - 0.5f;
        }
        else
        {
            placementPositionY = _transform.position.y;
        }
        return new Vector3(placementPositionX,
                placementPositionY, 0);
    }
}
