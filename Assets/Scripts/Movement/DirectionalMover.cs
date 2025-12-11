using UnityEngine;

public class DirectionalMover
{
    private CharacterController _characterController;

    private float _movementSpeed;

    private Vector3 _currentDirection;

    public DirectionalMover(CharacterController characterController, float movementSpeed)
    {
        _characterController = characterController;
        _movementSpeed = movementSpeed;
    }

    public Vector3 CurrentVelocity { get; private set; }

    public void SetInputDirection(Vector3 direction) => _currentDirection = direction;


    public void Update(float deltaTime, float speedModifier = 1)
    {
        CurrentVelocity = _currentDirection.normalized * (_movementSpeed * speedModifier);

        _characterController.Move(CurrentVelocity * deltaTime);

        //ConstrainToGround();
    }

    private void ConstrainToGround()
    {
        Vector3 pos = _characterController.transform.position;
        pos.y = 0f;
        _characterController.transform.position = pos;
    }
}
