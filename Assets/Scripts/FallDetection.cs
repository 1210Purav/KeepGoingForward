using UnityEngine;

public class FallDetection : MonoBehaviour
{
    public Transform player;
    public float respawnHeight = 0.7f;
    public Quaternion respawnRotation = Quaternion.identity;

    private Rigidbody rb;

    private float lastSafeZ = -8.82f;  // Start with initial platform Z

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update last safe Z only if player is standing safely above Y threshold
        if (player.position.y >= 0.52f)
        {
            lastSafeZ = player.position.z;
        }

        if (player.position.y < -5f)
        {
            // Calculate dynamic respawn Z: use last safe Z, but if they somehow moved too far back, clamp it
            float respawnZ = Mathf.Max(lastSafeZ, -8.82f);

            // Optionally: Pull them slightly forward to avoid edge respawn
            respawnZ += 3f;

            player.position = new Vector3(0f, respawnHeight, respawnZ);
            player.rotation = respawnRotation;

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
