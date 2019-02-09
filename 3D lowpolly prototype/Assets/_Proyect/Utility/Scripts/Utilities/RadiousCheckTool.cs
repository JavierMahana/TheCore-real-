using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RadiousCheckTool 
{

    public static bool ColliderInsideAnArea( Vector3 starPosition, float radious, LayerMask colliderLayer)
    {
        Collider[] enemies = Physics.OverlapSphere(starPosition, radious, colliderLayer);
        if (enemies.Length == 0)
        {
            return false;
            
        }
        return true;
    }
    public static bool ColliderInsideAnArea(Vector3 starPosition, float radious, LayerMask colliderLayer, out Collider[] results)
    {
        results = Physics.OverlapSphere(starPosition, radious, colliderLayer);
        if (results.Length == 0)
        {
            return false;

        }
        return true;
    }

}
