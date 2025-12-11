using UnityEngine;
using UnityEngine.AI;

public class ControllersManager : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private ClickPointerView _clickPointerView;

    private Controller _playerCharacterController;
    private Controller _aiCharacterController;
    private Controller _playerAiCharacterSwitchController;

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

        _playerCharacterController = new CompositeController(
            new TargetDirectionalMovableController(_character, _inputController, queryFilter),
            new AlongMovableVelocityRotatableController(_character, _character));

        _playerAiCharacterSwitchController = new PlayerAiCharacterSwitchController(
            _aiCharacterController,
            _playerCharacterController,
            _inputController,
            _switchTime);

        _inputController.Enable();
        _playerAiCharacterSwitchController.Enable();
    }

    private void Update()
    {
        _inputController.Update(Time.deltaTime);
        _playerAiCharacterSwitchController.Update(Time.deltaTime);
    }
}
