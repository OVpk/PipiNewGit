using UnityEngine;

public class Projectile : MonoBehaviour
{
    private PoolingSystem poolingSystem;
    private Rigidbody rb;

    public void Init(PoolingSystem pool, PlayerController.PlayerID playerID)
    {
        rb = GetComponent<Rigidbody>();
        poolingSystem = pool;
        tag = playerID == PlayerController.PlayerID.Player1 ? "P1" : "P2";
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag is "P1" or "P2") return;
        poolingSystem.AddToPool(this);
    }

    public void Launch(Vector3 force)
    {
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(force, ForceMode.VelocityChange);
        }
    }
}
