using UnityEngine;

public class BombView : MonoBehaviour
{
    [SerializeField] private Bomb _bomb;

    private bool _isActivated = false;
    private float _timer;

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
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, _timer / _bomb.Delay);

        if (_timer >= _bomb.Delay)
        {
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
}
