using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Vérifier si la caméra principale a été trouvée
        if (mainCamera == null)
        {
            Debug.LogError("La caméra principale n'a pas été trouvée. Assurez-vous d'avoir une caméra principale dans votre scène.");
        }
    }

    void Update()
    {
        // Vérifier si la caméra principale est définie
        if (mainCamera != null)
        {
            // Faire en sorte que le Slider soit toujours face à la caméra
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        }
    }
}
