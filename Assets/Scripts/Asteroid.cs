using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Controls the behavior of an asteroids in the game.
/// </summary>
public class Asteroid : MonoBehaviour
{
    const float minForce = 1f;
    const float maxForce = 3f;

    private float angle;
    Vector2 moveDirection;
    Rigidbody2D asteroidRb2D;

    [SerializeField] private Sprite magentaAsteroid;
    [SerializeField] private Sprite greenAsteroid;
    [SerializeField] private Sprite whiteAsteroid;


    void Awake()
    {
        // Cache the Rigidbody2D immediately so StartMoving() can be called right after Instantiate()
        asteroidRb2D = GetComponent<Rigidbody2D>();
        if (asteroidRb2D == null)
            Debug.LogError("Asteroid requires a Rigidbody2D on " + gameObject.name);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Randomly select an asteroid sprite
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteIndex = Random.Range(0, 3);
        if (spriteIndex == 0) {
            spriteRenderer.sprite = magentaAsteroid;
        } else if (spriteIndex == 1){
            spriteRenderer.sprite = greenAsteroid;
        }else {
            spriteRenderer.sprite = whiteAsteroid;
        }

        

    }
    public void Initialze(Direction direction, Vector3 asteroidLocation) {
        transform.position = asteroidLocation;
        //make the asteroid move in a random direction with a random force
        asteroidRb2D = GetComponent<Rigidbody2D>();
        float RandomAngle = Random.Range(0f,30f) * Mathf.Deg2Rad;
        float baseAngle;
        switch (direction) {
            case Direction.Up:
                baseAngle = 90f * Mathf.Deg2Rad;
                break;
            case Direction.Left:
                baseAngle = 0f * Mathf.Deg2Rad;
                break;
            case Direction.Down:
                baseAngle = 270f * Mathf.Deg2Rad;
                break;
            case Direction.Right:
                baseAngle = 180f * Mathf.Deg2Rad;
                break;
            default:
                baseAngle = 0f * Mathf.Deg2Rad;
                break;
        }
        angle = baseAngle + RandomAngle;
        StartMoving(angle);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            Destroy(collision.gameObject);
            if (transform.localScale.x < 0.5)
            {
                Destroy(this.gameObject);
            }
            else {
                //shrink the asteroid
                Vector3 asteroidScale = transform.localScale;
                asteroidScale = asteroidScale / 2;
                transform.localScale = asteroidScale;
                //shrink the collider
                asteroidRb2D.GetComponent<CircleCollider2D>().radius = asteroidRb2D.GetComponent<CircleCollider2D>().radius / 2;
                GameObject asteroid1 = Instantiate(this.gameObject);
                asteroid1.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI)); // Fix: Access the Asteroid component
                GameObject asteroid2 = Instantiate(this.gameObject);
                asteroid2.GetComponent<Asteroid>().StartMoving(Random.Range(0, 2 * Mathf.PI)); // Fix: Access the Asteroid component

                Destroy(this.gameObject); // Fix: Destroy the GameObject, not the script
            }
                
        }
    }
    public void StartMoving(float moveAngle)
    {
        //convert to unit vector
        moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        //apply random force in that direction
        float forceMagnitude = Random.Range(minForce, maxForce);
        asteroidRb2D.AddForce(moveDirection * forceMagnitude, ForceMode2D.Impulse);
    }
}
