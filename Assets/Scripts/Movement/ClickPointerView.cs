using UnityEngine;

public class ClickPointerView : MonoBehaviour
{
    [SerializeField] private GameObject _pointerPrefab;
    [SerializeField] private float _destroyTime = 1f;

    private InputController _inputController;

    public void Initialize(InputController inputController)
    {
        _inputController = inputController;
    }

    private void Update()
    {
        if (_inputController == null)
            return;

        if (_inputController.HasNewTarget)
            SpawnPointer(_inputController.TakeTarget());
    }

    private void SpawnPointer(Vector3 position)
    {
        GameObject pointer = Instantiate(_pointerPrefab, position, Quaternion.identity);
        Destroy(pointer, _destroyTime);
    }
}
