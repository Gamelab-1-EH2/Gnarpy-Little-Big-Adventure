using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Paralax : MonoBehaviour
{

    [SerializeField]Transform _toFollow;
    [SerializeField]Parallax_SO _so;
    [SerializeField] float _offset;
    [SerializeField] float _backgroundSize;
    [SerializeField] float _backgroundspeed;


    private float _oldCameraPos;
    private Transform[] _background;
    private int _right, _left;


    private void Awake()
    {
        _oldCameraPos=_toFollow.position.x;
        _background=this.GetComponentsInChildren<Transform>();
        _left = 0;
        _right = _background.Length - 1;
    }

    private void Update()
    {
        float deltaX = _toFollow.position.x - _oldCameraPos;
        Vector3 move = Vector3.right * deltaX / _backgroundspeed;
        transform.Translate(move);
        _oldCameraPos = _toFollow.position.x;

        if (_oldCameraPos < _background[_left].position.x + _offset)
        {
            ScrollLeft();
        }
        else if (_oldCameraPos > _background[_right].position.x - _offset)
        {
            ScrollRight();
        }
    }
    void ScrollLeft()
    {
        _background[_right].position = Vector3.right * (_background[_left].position.x - _backgroundSize);
        _left = _right;
        _right--;

        if (_right < 0)
            _right = _background.Length - 1;
    }
    void ScrollRight()
    {
        _background[_left].position = Vector3.right * (_background[_right].position.x + _backgroundSize);
        _right = _left;
        _left++;

        if (_left == _background.Length)
            _left = 0;
    }


}
