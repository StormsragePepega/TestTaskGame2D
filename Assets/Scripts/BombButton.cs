using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BombButton : MonoBehaviour,IPointerDownHandler
{
    public GameObject Bomb;
    [SerializeField]private PlayerController _player;
    public virtual void OnPointerDown(PointerEventData eventData) //реализовать через пул объектов
    {
        _player.DropBomb();
    }

}
