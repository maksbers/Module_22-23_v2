using UnityEngine;
using UnityEngine.AI;

public class ControllersSwitcher : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private ClickPointerView _clickPointerView;

    private Controller _manualCharacterController;
    private Controller _aiCharacterController;
    private Controller _currentController;

    private GroundClickRaycaster _groundClickRaycaster;
    private InputController _inputController;

    private float _switchTime = 3f;


    private void Awake()
    {
        _groundClickRaycaster = new GroundClickRaycaster(_character.GroundLayer);
        _inputController = new InputController(_groundClickRaycaster);

        _clickPointerView.Initialize(_inputController);

        NavMeshQueryFilter queryFilter = new NavMeshQueryFilter();
        queryFilter.agentTypeID = 0;
        queryFilter.areaMask = NavMesh.AllAreas;

        _aiCharacterController = new CompositeController(
            new RandomDirectionalMovableController(_character, queryFilter),
            new AlongMovableVelocityRotatableController(_character, _character));

        _manualCharacterController = new CompositeController(
            new TargetDirectionalMovableController(_character, _inputController, queryFilter),
            new AlongMovableVelocityRotatableController(_character, _character));

        _inputController.Enable();
        _manualCharacterController.Enable();
        _aiCharacterController.Enable();
    }

    private void Update()
    {
        _inputController.Update(Time.deltaTime);

        Controller targetController;

        if (IsManualControlActive())
            targetController = _manualCharacterController;
        else
            targetController = _aiCharacterController;

        if (_currentController != targetController)
            SwitchController(targetController);

        _currentController.Update(Time.deltaTime);
    }

    private void SwitchController(Controller newController)
    {
        if (_currentController != null)
            _currentController.Disable();

        _currentController = newController;
        _currentController.Enable();
    }

    private bool IsManualControlActive() => _inputController.TimeSinceLastClick < _switchTime;
}
