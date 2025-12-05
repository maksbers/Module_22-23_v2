using UnityEngine;

public class GroundClickRaycaster
{
    private LayerMask _groundLayer;

    public GroundClickRaycaster(LayerMask groundLayer)
    {
        _groundLayer = groundLayer;
    }

    public Vector3 GetGroundPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            Vector3 point = hit.point;
            point.y = 0f;
            return point;
        }

        return Vector3.zero;
    }
}
