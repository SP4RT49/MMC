using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float sprintSpeed = 24f;
    public float jumpHeight = 3f; // Hauteur du saut
    public float gravityValue = -9.81f; // Valeur de la gravité

    private float currentSpeed;
    private Vector3 playerVelocity; // Vitesse verticale du joueur
    private bool groundedPlayer; // Si le joueur est sur le sol

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Mouvement horizontal
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Gérer la course
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Gérer le saut
        if (Input.GetKey(KeyCode.Space) && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // Appliquer la gravité sur le joueur
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
