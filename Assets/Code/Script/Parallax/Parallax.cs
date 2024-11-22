using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]Transform _toFollow;
    [SerializeField]Parallax_SO _so;
    [SerializeField] float _backgroundspeed;


    private float _oldCameraPos;
    private Transform[] _background;
    private int _right, _left;


    private void Awake()
    {
        _oldCameraPos=_toFollow.position.x;


        _background = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _background[i] = transform.GetChild(i);
        }

        _left = 0;
        _right = _background.Length - 1;
    }

    private void Update()
    {
        float delta = _toFollow.position.x - _oldCameraPos;
        Vector3 move = Vector3.right * delta / _backgroundspeed;
        transform.Translate(move);
        _oldCameraPos = _toFollow.position.x;

        if (_oldCameraPos < _background[_left].position.x + _so.Offset)
        {
            ScrollLeft();
        }
        else if (_oldCameraPos > _background[_right].position.x - _so.Offset)
        {
            ScrollRight();
        }
    }
    void ScrollLeft()
    {
        _background[_right].position =Vector3.right * (_background[_right].position.x - _so.BackgroundSize);
        _background[_right].position = new Vector3(_background[_right].position.x, transform.position.y, 0);
        _left = _right;
        _right--;

        if (_right < 0)
            _right = _background.Length - 1;
    }
    void ScrollRight()
    {
        _background[_left].position = Vector3.right * (_background[_left].position.x + _so.BackgroundSize);
        _background[_left].position= new Vector3(_background[_left].position.x,transform.position.y,0);
        _right = _left;
        _left++;

        if (_left == _background.Length)
            _left = 0;
    }


}
