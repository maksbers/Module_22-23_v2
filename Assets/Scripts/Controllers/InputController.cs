using UnityEngine;

public class InputController : Controller
{
    private GroundClickRaycaster _raycaster;

    private Vector3 _targetPosition;
    private bool _hasNewTarget;

    private float _timeSinceLastClick = 0f;

    public InputController(GroundClickRaycaster raycaster)
    {
        _raycaster = raycaster;
    }

    public float TimeSinceLastClick => _timeSinceLastClick;
    public bool HasNewTarget => _hasNewTarget;
    public Vector3 TargetPosition => _targetPosition;


    protected override void UpdateLogic(float deltaTime)
    {
        _timeSinceLastClick += deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            _timeSinceLastClick = 0f;

            Vector3 point = _raycaster.GetGroundPoint();

            if (point != Vector3.zero)
            {
                _targetPosition = point;
                _hasNewTarget = true;
            }
        }
    }

    public Vector3 TakeTarget()
    {
        _hasNewTarget = false;
        return _targetPosition;
    }
}
