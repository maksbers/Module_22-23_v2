using UnityEngine;

public class BombView : MonoBehaviour
{
    private readonly int IsActivatedKey = Animator.StringToHash("IsActivated");

    [SerializeField] private ParticleSystem _explosionEffect;

    private Bomb _bomb;
    private Animator _animator;

    private bool _isActivated = false;
    private float _timer;


    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
        _animator = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _bomb.DamageRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated)
            return;

        if (other.GetComponent<IDamageable>() != null)
        {
            _isActivated = true;
            _animator.SetTrigger(IsActivatedKey);
            _timer = 0f;
        }
    }

    private void Update()
    {
        if (_isActivated == false)
            return;

        RunExplosionProcess();
    }

    private void RunExplosionProcess()
    {
        _timer += Time.deltaTime;

        if (_timer >= _bomb.Delay)
        {
            PlayEffect();
            Explode();
        }
    }

    private void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _bomb.DamageRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(_bomb.DamageAmount);
            }
        }

        Destroy(gameObject);
    }

    private void PlayEffect()
    {
        ParticleSystem instantiatedEffect = GameObject.Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        instantiatedEffect.Play();
    }
}
