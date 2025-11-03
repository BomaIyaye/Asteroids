using UnityEngine;
/// <summary>
/// Spawns asteroids into the game world.
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] GameObject asteroidPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get Radius of an asteroid
        GameObject asteroid = Instantiate(asteroidPrefab) as GameObject;
        Collider2D asteroidcollider = asteroid.GetComponent<Collider2D>();
        float asteroidRadius= asteroidcollider.GetComponent<CircleCollider2D>().radius;
        Destroy(asteroid);

        //Get height and width of the screen
        float screenHeight = ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom;
        float screenWidth = ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft;

        //Spawn asteroid at the right of the screen
        asteroid = Instantiate(asteroidPrefab) as GameObject;
        Vector3 rightSideAsteroidLocation = new Vector3((ScreenUtils.ScreenRight + asteroidRadius),(ScreenUtils.ScreenBottom +(screenHeight / 2)), 0);
        asteroid.GetComponent<Asteroid>().Initialze(Direction.Right, rightSideAsteroidLocation);

        //Spawn asteroid at the left of the screen
        asteroid = Instantiate(asteroidPrefab) as GameObject;
        Vector3 leftSideAsteroidLocation = new Vector3((ScreenUtils.ScreenLeft - asteroidRadius),(ScreenUtils.ScreenBottom +(screenHeight / 2)), 0);
        asteroid.GetComponent<Asteroid>().Initialze(Direction.Left, leftSideAsteroidLocation);

        //Spawn asteroid at the top of the screen
        asteroid = Instantiate(asteroidPrefab) as GameObject;
        Vector3 topSideAsteroidLocation = new Vector3((ScreenUtils.ScreenLeft +(screenWidth / 2)),(ScreenUtils.ScreenTop + asteroidRadius), 0);
        asteroid.GetComponent<Asteroid>().Initialze(Direction.Down, topSideAsteroidLocation);

        //Spawn asteroid at the bottom of the screen
        asteroid = Instantiate(asteroidPrefab) as GameObject;
        Vector3 bottomSideAsteroidLocation = new Vector3((ScreenUtils.ScreenLeft +(screenWidth / 2)),(ScreenUtils.ScreenBottom - asteroidRadius), 0);
        asteroid.GetComponent<Asteroid>().Initialze(Direction.Up, bottomSideAsteroidLocation);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
