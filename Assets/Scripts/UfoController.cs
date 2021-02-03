using UnityEngine;
using Random = UnityEngine.Random;

public class UfoController : MonoBehaviour
{
    public float baseSpeed;
    public float speedModifier;
    public float slowPhaseDuration; //seconds
    public float fastPhaseDuration; //seconds
    public float rateOfFire; //bullets per second
    public int bounty;
    
    private float _speed;
    public int imageWidth;
    public int imageHeight;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private TeleportableBehaviour _tb;
    private ExplodableBehaviour _eb;

    public static void Spawn()
    {
        var position = SceneHelper.GetSpawnPosition(Random.Range(3, 6));
        position.Set(position.x * SceneHelper.GetRandomMultiplier(), position.y); // spawn from left side as well
        var ufo = ResourcesLoader.GetUFO();
        Instantiate(ufo, position, Quaternion.identity);
    }
    public void Start()
    {
        _eb = gameObject.AddComponent<ExplodableBehaviour>();
        _eb.Transform = transform;
        _eb.Bounty = bounty;
        
        _tb = new TeleportableBehaviour(transform, imageWidth, imageHeight);

        _rb = gameObject.GetComponent<Rigidbody2D>();
        MoveHorizontally();

        Shoot();
    }
    
    void Update()
    {
        _tb.CheckBorders();
    }

    public void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(ResourcesLoader.GetBullet(), transform.position, transform.rotation);
        newBullet.tag = "EnemyBullet";
        var spaceshipController = SceneHelper.GetSpaceship().GetComponent<SpaceshipController>();
        var bulletForce = spaceshipController.bulletForce;
        var bulletLifeTime = spaceshipController.bulletLifeTime;
        newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(
            new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * bulletForce);
        Destroy(newBullet, bulletLifeTime);
        
        Invoke(nameof(Shoot), 1 / rateOfFire);
    }

    private void MoveHorizontally()
    {
        _speed = baseSpeed;
        Vector2 position = transform.position;
        Vector2 playerPosition = SceneHelper.GetSpaceship().transform.position;
        _direction = new Vector2(playerPosition.x - position.x, 0).normalized;
        
        Invoke(nameof(MoveToPlayer), slowPhaseDuration);
    }

    private void MoveToPlayer()
    {
        _speed *= speedModifier;
        Vector2 position = transform.position;
        Vector2 playerPosition = SceneHelper.GetSpaceship().transform.position;
        _direction = (playerPosition - position).normalized;
        
        Invoke(nameof(MoveHorizontally), fastPhaseDuration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _eb.Collider = other;
        _eb.ExplodeIfRequired();
    }
}
