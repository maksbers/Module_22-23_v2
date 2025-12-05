using UnityEngine;
using UnityEngine.AI;

public class TargetDirectionalMovableController : NavMeshMovementController
{
    private readonly InputController _inputController;
    private Vector3 _cachedTarget;

    public TargetDirectionalMovableController(
        IDirectionalMovable movable,
        InputController inputController,
        NavMeshQueryFilter queryFilter) : base(movable, queryFilter)
    {
        _inputController = inputController;
        _cachedTarget = _inputController.TargetPosition;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_inputController.TargetPosition != _cachedTarget)
        {
            _cachedTarget = _inputController.TargetPosition;
            SetTargetPosition(_cachedTarget);
        }

        base.UpdateLogic(deltaTime);
    }
}
