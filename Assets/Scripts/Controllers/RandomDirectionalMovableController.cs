using UnityEngine;
using UnityEngine.AI;

public class RandomDirectionalMovableController : NavMeshMovementController
{
    private readonly float _timeToChangeDirection = 2f;
    private readonly float _moveRadius = 5f;

    private float _timer;

    public RandomDirectionalMovableController(
        IDirectionalMovable movable,
        NavMeshQueryFilter queryFilter) : base(movable, queryFilter)
    {
        SetNewRandomTarget();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= _timeToChangeDirection)
        {
            SetNewRandomTarget();
            _timer = 0f;
        }

        base.UpdateLogic(deltaTime);
    }

    private void SetNewRandomTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _moveRadius;
        randomDirection += Movable.Position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _moveRadius, QueryFilter))
            SetTargetPosition(hit.position);
    }
}
