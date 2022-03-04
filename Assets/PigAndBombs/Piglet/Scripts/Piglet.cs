using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Piglet : MonoBehaviour, IDamageable, ICapable
{
    [SerializeField] private Joystick joystickMove;
    [SerializeField] private float _speed;

    [SerializeField] private HealthChangeEvent HealthChangeEvent;
    [SerializeField] private NewStarEvent NewStarEvent;
    [SerializeField] private UnityEvent DeathEvent;
    [SerializeField] private UnityEvent WinEvent;
    [SerializeField] private PositionEvent PositionEvent;
    private Animator _animator;
    private Vector2 _direction;
    private Rigidbody2D _rb;

    private int _health;
    private int _starCount;

    private float _damageTime;
    private bool isAccelerated = false;


    public int Health
    {
        get { return _health; }
        set
        {
            int oldhealth = _health;
            _health = Mathf.Clamp(value, 0, 100);
            HealthChangeEvent.Invoke(_health, oldhealth<=_health);
        }
    }

    public int StarCount
    {
        get { return _starCount; }
        set
        {
            _starCount = value;
            NewStarEvent.Invoke(_starCount);
            if (_starCount >= 3)
            {
                gameObject.SetActive(false);
                WinEvent.Invoke();
                
            }
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        Health = 100;
        StarCount = 0;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {

        _direction.x = joystickMove.Horizontal();
        _direction.y = joystickMove.Vertical();

        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
        _animator.SetFloat("Speed", _direction.sqrMagnitude);
       
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
        PositionEvent.Invoke(gameObject.transform);
    }


    public void Damage()
    {
        if (Time.time> _damageTime )
        {
            float cooldownRate = 2f;
            _damageTime = Time.time+ cooldownRate;
            Health -= 34;
            if (Health == 0)
            {
                DeathEvent.Invoke();
                gameObject.SetActive(false);
            }
        }
    }

    public void Aceleration()
    {
        if (!isAccelerated)
        {
            isAccelerated = true;
            StartCoroutine(AcelerationRoutine(4f));
        }
    }

    IEnumerator AcelerationRoutine(float waitTime)
    {
        _speed *= 2f;
        yield return new WaitForSeconds(waitTime);
        _speed = 2f;
        isAccelerated = false;
    }

    public void Healing()
    {
        Health += 34;
        
    }

    public void GiveStar()
    {
        StarCount += 1;
    }
}

[System.Serializable]
public class HealthChangeEvent: UnityEvent<int, bool> { }
[System.Serializable]
public class NewStarEvent : UnityEvent<int> { }
[System.Serializable]
public class PositionEvent : UnityEvent<Transform> { }
