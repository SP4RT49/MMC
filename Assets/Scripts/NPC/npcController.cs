using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NPCController : MonoBehaviour
{
    public Transform[] patrolLimits;
    public float moveSpeed = 3f;
    public float minMoveDistance = 3f;
    public float minPauseTime = 2f;
    public float maxPauseTime = 10f;
    public float followPlayerDistance = 5f;
    public float maxVisionDistance = 10f;
    public float visionAngle = 45f;
    public bool showPath = true;
    public bool showVisionCone = true;
    public Material pathMaterial;

    public float maxHealth = 100f;
    private float currentHealth;

    private Vector3 targetPosition;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private LineRenderer lineRenderer;
    private Vector3 lastPathPoint;
    private Transform playerTransform;

    public Slider healthSlider; // Référence au Slider dans l'interface utilisateur Unity
    public float damage = 20f;

    void Start()
    {
        SetRandomDestination();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = pathMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = showPath;

        AddPointToPath(transform.position);
        lastPathPoint = transform.position;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        currentHealth = maxHealth;

        // Assurez-vous que healthSlider est assigné dans l'inspecteur Unity
        if (healthSlider != null)
        {
            // Mettre à jour la valeur maximale du slider
            healthSlider.maxValue = maxHealth;
            // Mettre à jour la valeur actuelle du slider
            healthSlider.value = currentHealth;
        }
        else
        {
            Debug.LogError("Assignez le Slider de la barre de vie dans l'inspecteur Unity.");
        }
    }

    void Update()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer < visionAngle && distanceToPlayer < maxVisionDistance || distanceToPlayer < followPlayerDistance)
        {
            targetPosition = playerTransform.position;
            isPaused = false;
            pauseTimer = 0f;
        }

        if (isPaused)
        {
            pauseTimer -= Time.deltaTime;

            if (pauseTimer <= 0f)
            {
                isPaused = false;
                SetRandomDestination();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        MoveWithObstacleAvoidance();

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isPaused = true;
            pauseTimer = Random.Range(minPauseTime, maxPauseTime);
            SetRandomDestination();
        }

        if (showPath)
        {
            AddPointToPath(transform.position);
            lastPathPoint = transform.position;
        }
    }

    void MoveWithObstacleAvoidance()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Lance un rayon devant l'ennemi pour détecter les obstacles
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            // Vérifie si le rayon a frappé un mur
            if (hit.collider.CompareTag("Wall")) // Assurez-vous que les murs ont le tag "Wall"
            {
                Debug.Log("Mur détecté, ajustement du chemin");
                // Ajustez targetPosition ou changez la direction de l'ennemi ici
                // Par exemple, vous pouvez appeler SetRandomDestination pour changer de cible
                SetRandomDestination();
                return;
            }
        }

        // Déplacement vers la position cible si aucun obstacle n'est détecté
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180f * Time.deltaTime);
    }

    void SetRandomDestination()
    {
        float randomX = Random.Range(patrolLimits[0].position.x, patrolLimits[1].position.x);
        float randomZ = Random.Range(patrolLimits[0].position.z, patrolLimits[1].position.z);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    void AddPointToPath(Vector3 point)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }

    void OnDrawGizmos()
    {
        if (showVisionCone)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, visionAngle / 2, 0) * transform.forward * maxVisionDistance);
            Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -visionAngle / 2, 0) * transform.forward * maxVisionDistance);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            LoadScene();
        }

        // Mettre à jour la barre de vie
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
    }

    void OnTriggerEnter(Collider other)
    {
        // Détecter si l'objet avec lequel le boss entre en collision est le joueur
        if (other.CompareTag("Player"))
        {
            PlayerMotor player = other.GetComponent<PlayerMotor>();
            if (player != null)
            {
                Debug.Log("Attack player");
                // Appeler une méthode sur le script du joueur pour lui infliger des dégâts
                player.TakeDamage(damage);
            }
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(3);
    }
}
