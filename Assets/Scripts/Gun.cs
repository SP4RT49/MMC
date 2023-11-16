using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform firePoint; // Endroit où le projectile sera instancié
    public GameObject bullet; // Référence au prefab de la balle
    public AudioSource shootSound;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // "Fire1" est par défaut lié au clic gauche de la souris
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("shoot");

        // Instancier le projectile
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        shootSound.Play(); // Joue le son de tir
    }
}
