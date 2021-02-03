using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float velocity; //degrees per second
    public float torque; //degrees per second
    
    public Rigidbody2D spaceshipRb;
    public int imageWidth; //px
    public int imageHeight; //px
    
    public GameObject bullet;
    public float bulletForce;
    public float bulletLifeTime; //seconds

    private float _velocity;
    private float _torque;
    private float _velocityInput;
    private float _torqueInput;
    
    private Vector2 _spaceshipSize; //world units
    private Vector2 _screenSize; //world units
    private float _pixelSize; //world units

    private TeleportableBehaviour _tb;
    
    public void GameOver()
    {
        CancelInvoke();
    }

    public void Reload()
    {
        //add teleportable behaviour
        _tb = new TeleportableBehaviour(transform, imageWidth, imageHeight);
    }

    public void Start()
    {
        Reload();
    }
    
    public void Update()
    {
        HandleInputs();
        _tb.CheckBorders();
    }
    
    public void FixedUpdate()
    {
        //update movement && rotation velocity
        spaceshipRb.AddRelativeForce(_velocityInput * velocity * Vector2.up);
        transform.Rotate(-torque * Time.deltaTime * _torqueInput * Vector3.forward);
    }

    public void Disappear()
    {
        PlayerState.State = PlayerState.States.Dead;
        SetImmortal();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Appear()
    {
        PlayerState.State = PlayerState.States.Alive;
        Invoke(nameof(SetMortal), Player.ImmortalityDuration);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Respawn(float delay)
    {
        Invoke(nameof(Reset), delay - 0.01f);
        Invoke(nameof(Appear), delay);
    }
    
    private void HandleInputs()
    {
        if (Input.GetKey("escape"))
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        //no handling if game is inactive
        if (!GameState.IsActive() || !PlayerState.IsActive())
        {
            return;
        }
        
        //update velocity inputs
        _velocityInput = Input.GetAxis("Vertical");
        _torqueInput = Input.GetAxis("Horizontal");
        
        //handle fire
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * bulletForce);
            Destroy(newBullet, bulletLifeTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Bullet")
        {
            PlayHitSound();
            Player.LoseLife();
        }
    }
    
    private void SetImmortal()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = ResourcesLoader.GetFilledSpaceshipSprite();
    }

    private void SetMortal()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = ResourcesLoader.GetSpaceshipSprite();
    }
    
    private void PlayHitSound()
    {
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Reset()
    {
        var tf = transform;
        spaceshipRb.velocity = Vector3.zero;
        spaceshipRb.angularVelocity = 0;
        tf.position = Vector3.zero;
        tf.rotation = Quaternion.identity;
        tf.Rotate(Vector3.zero, 0f);
    }
}