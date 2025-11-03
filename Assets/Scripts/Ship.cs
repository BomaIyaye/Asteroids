using UnityEngine;

/// <summary>
/// This class represents a ship in the game.
/// </summary>
public class Ship : MonoBehaviour
{
    #region Fields

    Rigidbody2D ShipRb2D;//The Rigidbody2D component attached to the ship
    Vector2 thrustDirection = new Vector2(0, 1);//The direction of the thrust applied to the ship
    float thrustForce = 7f;//The force of the thrust applied to the ship
    const double RotateDegreesPerSecond = 90;

    [SerializeField] GameObject prefabBullet;

    #endregion



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShipRb2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //calculate rotation Amount and apply rotation
        float rotationInput = Input.GetAxis("Rotate");
        if (rotationInput != 0) {
            float rotationAmount = (float)(RotateDegreesPerSecond * Time.deltaTime);
            if (rotationInput < 0) {
                rotationAmount *= -1;
            }
            transform.Rotate(Vector3.forward, rotationAmount);
            //change thrust direction to match ship rotation
            float zRotation = transform.eulerAngles.z * Mathf.Deg2Rad;
            thrustDirection.x = Mathf.Cos(zRotation);
            thrustDirection.y = Mathf.Sin(zRotation);
        }
        //fire bullet when fire button is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            GameObject bullet = Instantiate(prefabBullet) as GameObject;
            bullet.transform.position = this.transform.position;
            bullet.GetComponent<Bullet>().ApplyForce(thrustDirection);
        }
    }
    /// <summary>
    /// Called at a fixed time interval to perform physics-related updates.
    /// </summary>
    /// <remarks>This method is typically used to apply consistent physics calculations, such as forces or
    /// velocity updates,  ensuring smooth and deterministic behavior regardless of frame rate. It is invoked
    /// automatically by the Unity engine.</remarks>
    void FixedUpdate()
    {
        if (Input.GetAxis("Thrust") != 0) {
            ShipRb2D.AddForce(thrustDirection * thrustForce,ForceMode2D.Force);
        }
    }

    //destroys ship when ship collides with an asteroid
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid")) {
            Destroy(gameObject);
        }
    }

}
