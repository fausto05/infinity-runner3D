using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public GameObject tallObstaclePrefab;
    public float tallObstacleChance = 0.2f;
    private void Start()
    {
        groundSpawner = GameObject.FindFirstObjectByType<GroundSpawner>();
    }
    
    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }
    
    
    public void SpawnObstacle()
    {
        // Elegir que obstaculo generar
        GameObject obstacleToSpawn = obstaclePrefab;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance)
        {
            obstacleToSpawn = tallObstaclePrefab;
        }
        
        // Elegir un punto random para generar el obstaculo
        int obstacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;

        // Generar el obstaculo en la posicion  
        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
    }

    public void SpawnCoins()
    {
        int coinsToSpawn = 3;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform); 
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }
    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), 
                                    Random.Range(collider.bounds.min.y, collider.bounds.max.y), 
                                    Random.Range(collider.bounds.min.z, collider.bounds.max.z)); 
        if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }

        point.y = 1;
        return point;
    }
}
