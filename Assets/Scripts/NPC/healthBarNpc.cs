using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Camera PlayerCamera;

    void Start()
    {
        // Vérifier si la caméra principale a été trouvée
        if (PlayerCamera == null)
        {
            Debug.LogError("La caméra principale n'a pas été trouvée. Assurez-vous d'avoir une caméra principale dans votre scène.");
        }
    }

    void Update()
    {
        // Vérifier si la caméra principale est définie
        if (PlayerCamera != null)
        {
            // Faire en sorte que le Slider soit toujours face à la caméra
            transform.LookAt(transform.position + PlayerCamera.transform.rotation * Vector3.forward, PlayerCamera.transform.rotation * Vector3.up);
        }
    }
}
