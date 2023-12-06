using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f; // Vitesse de la balle
    public float damage = 10f;
    public Rigidbody rb; // Référence au Rigidbody de la balle

    void Start()
    {
        Debug.Log("start");

        rb.velocity = transform.forward * speed; // Lance la balle vers l'avant

        // Détruit la balle après 5 secondes si elle n'a rien touché
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("touch enemy");

            // Logique de dommage ici
            Destroy(collision.gameObject);
            // Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {

            Debug.Log("touch boss");
            NPCController enemy = collision.collider.GetComponent<NPCController>();
            if (enemy != null)
            {
                // Infliger des dégâts à l'ennemi
                enemy.TakeDamage(damage);
            }
        }

        Debug.Log("destroy");
        // Logic de ce qui se passe lors d'une collision
        Destroy(gameObject); // Détruit la balle après collision
    }
}
