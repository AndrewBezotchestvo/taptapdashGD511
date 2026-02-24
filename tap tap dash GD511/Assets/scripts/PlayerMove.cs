using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5;
    private Rigidbody _rb;
    private Vector3 _movement;
    [SerializeField] private float _gravityScale = 10;
    [SerializeField] private float _jumpForce = 10;

    [SerializeField] private float _minSpeed = 0.1f;
    [SerializeField] private float _maxSpeed = 10f;
    private float _current_speed;
    private Vector3 _previosPosition;

    private bool _direction;
    private bool _isGrounded;
    //_movement - направление движение
    //_rb - компонент для работы с физикой
    //_playerSpeed - скорость персонажа


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movement.x = _playerSpeed;
        _direction = true;
        _isGrounded = true;
    }

    void Update()
    {
        Debug.Log(_current_speed);

        if (transform.position.x + transform.position.z > 5)
        {
            if (_current_speed > _maxSpeed || _current_speed < _minSpeed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //перезагрузка сцены
            }
        }

        //_rb.AddForce(transform.forward * _playerSpeed);
        if (_direction == true)
        {
            _movement.x = _playerSpeed;
            _movement.z = 0;
        }
        else
        {
            _movement.x = 0;
            _movement.z = _playerSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _direction = !_direction;
        }

        if (Input.GetMouseButtonDown(1) && _isGrounded == true)
        {
            _movement.y = _jumpForce;
            _isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        _current_speed = (transform.position - _previosPosition).magnitude / Time.fixedDeltaTime;
        _previosPosition = transform.position;

        _rb.MovePosition(transform.position + _movement * 0.01f);

        if (_isGrounded == false)
        {
            _movement.y -= _gravityScale * 0.01f;
        }
        else
        {
            _movement.y = -1;
        }
    }

//проверка столкновения с объектами
    private void OnCollisionEnter(Collision collision)
    {
        //проверка столкновения с объектам у которого тег Ground
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

}
