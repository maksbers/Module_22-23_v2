using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private float _runningThreshold = 0.1f;
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int DieKey = Animator.StringToHash("Die");
    private readonly int IsAttacked = Animator.StringToHash("IsAttacked");

    private const string InjuredLayerName = "InjuredLayer";
    private int _injuredLayerIndex;

    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    private bool _isDead;

    private void Awake()
    {
        _injuredLayerIndex = _animator.GetLayerIndex(InjuredLayerName);
    }

    private void Update()
    {
        UpdateHealth();

        if (_character.IsDead)
        {
            if (_isDead == false)
            {
                _isDead = true;
                _animator.SetTrigger(DieKey);
                StopRunning();
            }
            return;
        }

        if (_character.TryGetDamageTaken())
        {
            _animator.SetTrigger(IsAttacked);
        }

        if (_character.CurrentVelocity.magnitude > _runningThreshold)
            StartRunning();
        else
            StopRunning();
    }

    private void UpdateHealth()
    {
        float injuredWeight = 0f;

        if (_character.Health <= _character.MaxHealth * _character.InjuredThreshold)
        {
            injuredWeight = 1f;
        }

        _animator.SetLayerWeight(_injuredLayerIndex, injuredWeight);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);
    }
}
