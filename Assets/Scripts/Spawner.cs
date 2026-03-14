using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject collectablePrefab;
    [SerializeField] float spawnInterval = 2f;
    [SerializeField] float spawnRange = 10f; // radio alrededor del spawner

    private float timer = 0f;

    void Start()
    {
        PoolManager.GetInstance().SetPool(collectablePrefab, 15);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        GameObject obj = PoolManager.GetInstance().Get(collectablePrefab);

        // Posición aleatoria alrededor del spawner
        Vector3 randomPos = transform.position + new Vector3(
            Random.Range(-spawnRange, spawnRange),
            0f,
            Random.Range(-spawnRange, spawnRange)
        );

        obj.transform.position = randomPos;
    }
}
