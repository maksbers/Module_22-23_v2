using UnityEngine;
using UnityEngine.AI;

public class NavMeshUtils
{
    public static float GetPathLength(NavMeshPath path)
    {
        float length = 0f;

        if (path.corners.Length < 2)
            return length;

        for (int i = 1; i < path.corners.Length; i++)
            length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        
        return length;
    }

    public static bool TryGetPath(Vector3 from, Vector3 to, NavMeshQueryFilter queryFilter, NavMeshPath pathToTarget)
    {
        if (NavMesh.CalculatePath(from, to, queryFilter, pathToTarget))
        {
            if (pathToTarget.status != NavMeshPathStatus.PathInvalid)
                return true;
        }

        return false;
    }
}
