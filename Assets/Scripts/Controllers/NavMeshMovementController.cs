using UnityEngine;
using UnityEngine.AI;

public abstract class NavMeshMovementController : Controller
{
    private const int MinCornersInPathToMove = 2;
    private const float StoppingDistance = 0.2f;

    protected readonly IDirectionalMovable Movable;
    protected readonly NavMeshQueryFilter QueryFilter;

    private readonly NavMeshPath _pathToTarget;
    private Vector3 _currentTarget;

    private bool _isMoving;

    protected NavMeshMovementController(IDirectionalMovable movable, NavMeshQueryFilter queryFilter)
    {
        Movable = movable;
        QueryFilter = queryFilter;
        _pathToTarget = new NavMeshPath();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_isMoving == false)
            return;

        if (IsValidPath() == false)
            return;

        UpdateMoveDirection();
        
        if (IsTargetReached())
            StopMoving();
    }

    private void UpdateMoveDirection()
    {
        Vector3 direction;

        if (_pathToTarget.status == NavMeshPathStatus.PathComplete && _pathToTarget.corners.Length >= MinCornersInPathToMove)
            direction = (_pathToTarget.corners[1] - _pathToTarget.corners[0]).normalized;
        else
            direction = (_currentTarget - Movable.Position).normalized;

        Movable.SetMoveDirection(direction);
    }

    private bool IsTargetReached()
    {
        float distance = CalculateDistanceToTarget();

        return distance <= StoppingDistance;
    }

    private float CalculateDistanceToTarget()
    {
        if (_pathToTarget.corners.Length < MinCornersInPathToMove)
        {
            return Vector3.Distance(Movable.Position, _currentTarget);
        }

        return NavMeshUtils.GetPathLength(_pathToTarget);
    }

    private bool IsValidPath()
    {
        return NavMeshUtils.TryGetPath(Movable.Position, _currentTarget, QueryFilter, _pathToTarget);
    }

    protected void SetTargetPosition(Vector3 target)
    {
        _currentTarget = target;
        _isMoving = true;
    }

    protected void StopMoving()
    {
        _isMoving = false;
        Movable.SetMoveDirection(Vector3.zero);
    }
}
