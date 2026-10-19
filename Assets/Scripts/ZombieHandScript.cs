using UnityEditor.UI;
using UnityEngine;

public class ZombieHandScript : MonoBehaviour
{
    [SerializeField] Transform rightController;
    [SerializeField] Transform zombieHand;

    void Start()
    {
        
    }

    void Update()
    {
        zombieHand.transform.position = rightController.transform.position;
        zombieHand.transform.rotation = rightController.transform.rotation;
    }
}
