using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Vitesse de la balle
    public Rigidbody rb; // Référence au Rigidbody de la balle
    public Transform colliedCheck;
    public LayerMask colliedMask;
    public float colliedDistance;

    bool isCollied;

    void Start()
    {
        Debug.Log("start");

        rb.velocity = transform.forward * speed; // Lance la balle vers l'avant
    }
   
    void OnCollisionEnter(Collision collision)
    {
        isCollied = Physics.CheckSphere(colliedCheck.position, colliedDistance, colliedMask);
        // Logic de ce qui se passe lors d'une collision
        if (isCollied)
        {
            Debug.Log("collied");
            Destroy(gameObject); // Détruit la balle après collision
        }
    }
}
