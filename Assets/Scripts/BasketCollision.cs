/*using UnityEngine;

public class BasketCollision : MonoBehaviour
{
    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        Debug.Assert(gameManager != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            gameManager.AddPlayerScore(10);

            
        }
    }
}*/

using UnityEngine;

public class BasketCollision : MonoBehaviour
{
    [SerializeField] Transform hoopCenter;
    [SerializeField] ParticleSystem confetti;

    [Header("Distance tiers (meters, horizontal)")]
    [SerializeField] float tier1 = 2f;   // closer than this -> 1 point
    [SerializeField] float tier2 = 4f;   // closer than this -> 2 points
    [SerializeField] float tier3 = 6f;   // closer than this -> 3 points, further -> 5

    [Header("Confetti")]
    [SerializeField] int minConfetti = 30;
    [SerializeField] int maxConfetti = 300;
    [SerializeField] float maxDistanceForConfetti = 8f;

    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        Debug.Assert(gameManager != null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Ball") return;

        BallShot shot = other.GetComponent<BallShot>();
        if (shot == null || shot.scoreDisabled) return;

        // Only count the ball if it's moving downward through the hoop
        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && rb.linearVelocity.y > 0f) return;

        Vector3 from = shot.throwOrigin;
        Vector3 to = hoopCenter.position;
        from.y = 0f;
        to.y = 0f;
        float distance = Vector3.Distance(from, to);

        int points = distance < tier1 ? 1
                   : distance < tier2 ? 2
                   : distance < tier3 ? 3
                   : 5;

        gameManager.AddPlayerScore(points);
        SpawnConfetti(distance);
    }

    void SpawnConfetti(float distance)
    {
        if (confetti == null) return;
        float t = Mathf.InverseLerp(0f, maxDistanceForConfetti, distance);
        confetti.Emit(Mathf.RoundToInt(Mathf.Lerp(minConfetti, maxConfetti, t)));
    }
}