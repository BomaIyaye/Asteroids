using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    float ColliderRadius;//The radius of the ship's collider
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ColliderRadius = GetComponent<CircleCollider2D>().radius;
    }

    void OnBecameInvisible()
    {
        Vector2 position = transform.position;

        // check left, right, top, and bottom sides
        if (position.x + ColliderRadius < ScreenUtils.ScreenLeft ||
            position.x - ColliderRadius > ScreenUtils.ScreenRight)
        {
            position.x *= -1;
        }
        if (position.y - ColliderRadius > ScreenUtils.ScreenTop ||
            position.y + ColliderRadius < ScreenUtils.ScreenBottom)
        {
            position.y *= -1;
        }

        // move ship
        transform.position = position;
    }
}
