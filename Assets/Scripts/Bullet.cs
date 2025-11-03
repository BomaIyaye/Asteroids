using UnityEngine;
/// <summary>
/// Controls Bullet behaviour from the ship
/// </summary>

public class Bullet : MonoBehaviour
{
    const int bulletLifeTime= 2;
    private Timer deathTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathTimer = gameObject.AddComponent<Timer>();    
        deathTimer.Duration = bulletLifeTime;
        deathTimer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer != null && deathTimer.Finished) {
            Destroy(this.gameObject);
        }
        
    }

    /// <summary>
    /// appliess force to the bullet causing it to move
    /// </summary>
    /// <param name="forceDirection">direction of the force to be applied</param>
    public void ApplyForce(Vector2 forceDirection) { 
        const float forceMagnitude = 20f;
        Rigidbody2D bulletRb = GetComponent<Rigidbody2D>();
        bulletRb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
    }
}
