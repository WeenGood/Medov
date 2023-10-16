using UnityEngine;

public static class Extentions
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }
        float radius = 0.25f;
        float distance = 0.9f;

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);

        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, float radius, float distance)
    {
        if (rigidbody.isKinematic)
        {
            return false;
        }

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);

        return hit.collider != null && hit.rigidbody != rigidbody;
    }

    public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
    {
        Vector2 direction = other.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
    }
}
