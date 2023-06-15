using Fusion;
using UnityEngine;

public class bullet : NetworkBehaviour
{
    public int Damage { get; set; } = 10;
    public float Lifetime { get; set; } = 3f;

    private void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health health = other.GetComponent<Health>();
            scoreadd score = other.GetComponent<scoreadd>();
            if(score != null)
            {
                score.AddScoreRpc(1);
            }
            if (health != null)
            {
                health.DealDamageRpc(Damage);
            }
        }

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        // Destroy the bullet object
        Destroy(gameObject);
    }
}