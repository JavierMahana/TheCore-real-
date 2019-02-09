using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions 
{
    public static float DistanceXZ(this Vector3 position, Vector3 other)
    {
        Vector2 pos2D = new Vector2(position.x, position.z);
        Vector2 other2D = new Vector2(other.x, other.z);

        return Vector2.Distance(pos2D, other2D);
    }

    public static float SqrDistanceXZ(this Vector3 position, Vector3 other)
    {
        Vector2 pos2D = new Vector2(position.x, position.z);
        Vector2 other2D = new Vector2(other.x, other.z);

        return pos2D.SqrDistance(other2D);
    }
    public static float SqrDistanceXZ(this Vector3 position, Vector2 otherXZ)
    {
        Vector2 pos2D = new Vector2(position.x, position.z);

        return pos2D.SqrDistance(otherXZ);
    }



    public static Collider ClosestColliderXZ(this Vector3 pos, Collider[] others)
    {
        
        Collider closestEnemy = others[0];
        Debug.Assert(closestEnemy != null);

        float sqrMinDistance = pos.SqrDistanceXZ(closestEnemy.transform.position);

        for (int i = 1; i < others.Length; i++)
        {
            Collider check = others[i];
            float checkSqrDist = pos.SqrDistanceXZ(check.transform.position);
            if (checkSqrDist < sqrMinDistance)
            {
                closestEnemy = check;
                sqrMinDistance = checkSqrDist;
            }
        }
        return closestEnemy;
    }
    public static Collider ClosestColliderInAreaXZ(this Vector3 position, float radious, LayerMask layerMask)
    {
        Vector2 pos2D = new Vector2(position.x, position.z);
        Collider[] enemies = Physics.OverlapSphere(position, radious, layerMask);
        if (enemies.Length == 0)
        {
            return null;
        }
        Collider closestEnemy = enemies[0];
        Vector2 enemyPos = new Vector2(closestEnemy.transform.position.x, closestEnemy.transform.position.z);
        float minSqrDist = pos2D.SqrDistance(enemyPos);

        for (int i = 1; i < enemies.Length; i++)
        {
            enemyPos = new Vector2(enemies[i].transform.position.x, enemies[i].transform.position.z);
            float sqrDist = pos2D.SqrDistance(enemyPos);
            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                closestEnemy = enemies[i];
            }
        }
        
        return closestEnemy;
    }
}
