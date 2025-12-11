using UnityEngine;

public class PlayerAiCharacterSwitchController : Controller
{
    private Controller _aiController;
    private Controller _playerController;
    private InputController _inputController;
    private float _switchTime;

    private Controller _currentController;

    public PlayerAiCharacterSwitchController(
        Controller aiController,
        Controller playerController,
        InputController inputController,
        float switchTime)
    {
        _aiController = aiController;
        _playerController = playerController;
        _inputController = inputController;
        _switchTime = switchTime;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _inputController.Update(Time.deltaTime);

        Controller targetController;

        if (IsManualControlActive())
            targetController = _playerController;
        else
            targetController = _aiController;

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
