using UnityEngine;

public class ExplodableBehaviour : MonoBehaviour
{
    public Collider2D Collider { get; set; }
    public Transform Transform { get; set; }
    public int Bounty { get; set; }

    public void ExplodeIfRequired()
    {
        if (Collider == null)
        {
            return;
        }
        
        if (Collider.tag == "Bullet")
        {
            if (GameState.IsActive()) //don't add points if game is over
            {
                Player.AddPoints(Bounty);
            }
            Destroy(Collider.gameObject); //destroy bullet on collision
            Vector3 position = Transform.position;
            Quaternion rotation = Transform.rotation;
            var explosion = Instantiate(ResourcesLoader.GetExplosion(), position, rotation);
            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
    }
}