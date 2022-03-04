using UnityEngine;

public class Farmer : MonoBehaviour, IDamageable
{
    [SerializeField] private float _speed;
    [SerializeField] private HealthChangeEvent HealthChangeEvent;

    private Vector2 _direction;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _target;

    private float _directionChooseTime;
    private float _damageTime;
    private int _health;
    private enum State { Walking,Chasing};
    private State _state;
    public int Health
    {
        get { return _health; }
        set
        {
            int oldhealth = _health;
            _health = Mathf.Clamp(value, 0, 100);
            HealthChangeEvent.Invoke(_health, oldhealth <= _health);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Health = 100;
        _state = State.Walking;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        switch (_state)
        {
            case (State.Walking):
                Debug.Log("Walking");
                Walking();
                break;
            case (State.Chasing):
                Debug.Log("Chasing");
                Chasing();
                break;
        }
        
    }



    void FixedUpdate()
    {
        
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
    }

    private void CheckState()
    {
        Vector3 dPos = _target.position - transform.position;

        if (dPos.sqrMagnitude <= 140)
        {
            _state = State.Chasing;
        }
        else
        {
            _state = State.Walking;
        }
    }

    private void Chasing()
    {
        Vector3 dPos = _target.position - transform.position;
        dPos.Normalize();
        _direction.x = dPos.x*10;
        _direction.y = dPos.y * 10;

        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
        _animator.SetFloat("Speed", _direction.sqrMagnitude);
    }

    private void Walking()
    {
        if (Time.time > _directionChooseTime)
        {
            float cooldownRate = 2f;
            _directionChooseTime = Time.time + cooldownRate;
            _direction.x = Random.Range(-1, 2);
            _direction.y = Random.Range(-1, 2);

        }

        _animator.SetFloat("Horizontal", _direction.x);
        _animator.SetFloat("Vertical", _direction.y);
        _animator.SetFloat("Speed", _direction.sqrMagnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable hit))
        {
            hit.Damage();

        }

        Debug.Log("Collision");
    }

    public void OnTargerPositionChange(Transform pos)
    {
        _target = pos;
    }

    public void Damage()
    {
        if (Time.time > _damageTime)
        {
            float cooldownRate = 2f;
            _damageTime = Time.time + cooldownRate;
            Health -= 34;
            if (Health == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
