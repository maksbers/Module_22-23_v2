using UnityEngine;

public class Character : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _injuredMoveSpeedModifier = 0.3f;
    [SerializeField] private float _injuredThreshold = 0.3f;

    private HealthSystem _healthSystem;

    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private LayerMask _groundLayer;

    private bool _isDamageTaken;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public LayerMask GroundLayer => _groundLayer;

    public Vector3 Position => transform.position;

    public float Health => _healthSystem.CurrentHealth;
    public float MaxHealth => _healthSystem.MaxHealth;
    public float InjuredThreshold => _injuredThreshold;
    public bool IsDead => _healthSystem.IsDead;


    private void Awake()
    {
        _healthSystem = new HealthSystem(_maxHealth);

        _mover = new DirectionalMover(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    private void Update()
    {
        if (IsDead)
            return;

        float speedModifier = CalculateSpeedModifier();

        _mover.Update(Time.deltaTime, speedModifier);
        _rotator.Update(Time.deltaTime);
    }

    private float CalculateSpeedModifier()
    {
        if (Health <= MaxHealth * _injuredThreshold)
            return _injuredMoveSpeedModifier;

        return 1f;
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public float TakeDamage(float damageAmount)
    {
        _isDamageTaken = true;

        return _healthSystem.TakeDamage(damageAmount);
    }

    public bool TryGetDamageTaken()
    {
        if (_isDamageTaken == false)
            return false;

        _isDamageTaken = false;
        return true;
    }
}
