using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Vitesse de la balle
    public Rigidbody rb; // Référence au Rigidbody de la balle

    void Start()
    {
        Debug.Log("start");

        rb.velocity = transform.forward * speed; // Lance la balle vers l'avant
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("destroy");
        // Logic de ce qui se passe lors d'une collision
        Destroy(gameObject); // Détruit la balle après collision
    }
}
