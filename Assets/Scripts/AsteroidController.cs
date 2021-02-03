using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public float minVelocity;
    public float maxVelocity;
    public float maxTorque;
    
    public Rigidbody2D rb;
    public int imageWidth; //px
    public int imageHeight; //px

    public AsteroidHelper.Stages stage;
    public GameObject[] potentialChildren = new GameObject[3];
    
    private TeleportableBehaviour _tb;
    private ExplodableBehaviour _eb;
    private GameObject[] _children = new GameObject[2]; //2 possible asteroid links after collision
    private GameObject _explosion;

    public void Start()
    {
        //add teleportable behaviour
        _tb = new TeleportableBehaviour(transform, imageWidth, imageHeight);
        
        _eb = gameObject.AddComponent<ExplodableBehaviour>();
        _eb.Transform = transform;
        _eb.Bounty = AsteroidHelper.GetBounty(stage);
        
        //add random forces
        Vector2 velocity = new Vector2(Random.Range(minVelocity, maxVelocity) * SceneHelper.GetRandomMultiplier(), 
            Random.Range(minVelocity, maxVelocity) * SceneHelper.GetRandomMultiplier());
        float torque = Random.Range(-maxTorque, maxTorque);
        rb.AddForce(velocity);
        rb.AddTorque(torque);

        //link to the particles
        _explosion = ResourcesLoader.GetExplosion();
    }

    public void Update()
    {
        _tb.CheckBorders();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            
            if (stage > AsteroidHelper.Stages.Small)
            {
                //create links to smaller asteroids if required
                for (int i = 0; i < _children.Length; i++)
                {
                    _children[i] = potentialChildren[(int)Random.Range(0, potentialChildren.Length)];
                    Instantiate(_children[i], position, rotation);
                }
            }
        }
        
        _eb.Collider = other;
        _eb.ExplodeIfRequired();
    }
}