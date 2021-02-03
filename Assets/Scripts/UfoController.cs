using UnityEngine;
using Random = UnityEngine.Random;

public class UfoController : MonoBehaviour
{
    public float baseSpeed;
    public float speedModifier;
    public float slowPhaseDuration; //seconds
    public float fastPhaseDuration; //seconds
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
    }
    
    void Update()
    {
        _tb.CheckBorders();
    }

    public void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * _speed * Time.fixedDeltaTime);
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
