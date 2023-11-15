using UnityEngine;

public class npcController : MonoBehaviour
{
    public Transform[] patrolLimits; // Points définissant les limites de la zone de patrouille
    public float moveSpeed = 3f; // Vitesse de déplacement du NPC
    public float minMoveDistance = 3f; // Distance minimale avant de faire une pause
    public float minPauseTime = 2f; // Temps de pause minimum en secondes
    public float maxPauseTime = 10f; // Temps de pause maximum en secondes
    public bool showPath = true; // Afficher le chemin ou non
    public Material pathMaterial; // Matériau du chemin

    private Vector3 targetPosition;
    private bool isPaused = false;
    private float pauseTimer = 0f;
    private LineRenderer lineRenderer;
    private Vector3 lastPathPoint;

    void Start()
    {
        SetRandomDestination();

        // Initialiser le Line Renderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.material = pathMaterial;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = showPath;

        // Ajouter le point de départ au chemin
        AddPointToPath(transform.position);
        lastPathPoint = transform.position;
    }

    void Update()
    {
        if (isPaused)
        {
            // Compter le temps de pause
            pauseTimer -= Time.deltaTime;

            // Si le temps de pause est écoulé, reprendre la patrouille
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
        // Déplacement vers la position cible
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotation vers la position cible
        Vector3 direction = targetPosition - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180f * Time.deltaTime);

        // Si le NPC atteint la position cible, faire une pause aléatoire
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            
                isPaused = true;
                pauseTimer = Random.Range(minPauseTime, maxPauseTime);
            
                SetRandomDestination();
            
        }

        // Mettre à jour le tracé du chemin si la distance minimale est atteinte
        if (showPath)
        {
            AddPointToPath(transform.position);
            lastPathPoint = transform.position;
        }
    }

    void SetRandomDestination()
    {
        // Sélectionne une position aléatoire à l'intérieur des limites
        float randomX = Random.Range(patrolLimits[0].position.x, patrolLimits[1].position.x);
        float randomZ = Random.Range(patrolLimits[0].position.z, patrolLimits[1].position.z);

        targetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }

    void AddPointToPath(Vector3 point)
    {
        // Ajouter un point au tracé du chemin
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }
}
