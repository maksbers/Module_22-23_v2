using UnityEngine;

public class RandomWalkCharacterController : Controller
{
    private Character _character;

    private float _time;
    private float _timeToChangeDirection;

    private Vector3 _inputDirection;

    public RandomWalkCharacterController(Character character, float timeToChangeDirection)
    {
        _character = character;
        _timeToChangeDirection = timeToChangeDirection;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _time += deltaTime;

        if (_time >= _timeToChangeDirection)
        {
            _inputDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

            _time = 0f;
        }

        _character.SetMoveDirection(_inputDirection);
        _character.SetRotationDirection(_inputDirection);
    }
}
