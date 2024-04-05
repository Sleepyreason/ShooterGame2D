using System.Collections;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnEnemy;
    [SerializeField] private float startSpawnerInterval;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private int maxNumberOfEnemies = 3;
    private float mapWidth = 10f; // Ширина области, в пределах которой будут появляться враги
    private float mapHeight = 10f;
    private int nowTheEnemies = 0;
    private int allSpawnEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (nowTheEnemies < maxNumberOfEnemies && numberOfEnemies >= allSpawnEnemies)
        {
            yield return new WaitForSeconds(startSpawnerInterval); // Ждем указанный интервал перед появлением следующего врага
            Vector3 spawnPosition = new Vector3(Random.Range(-mapWidth / 2, mapWidth / 2), Random.Range(-mapHeight / 2, mapHeight / 2), 0f);

            GameObject enemyPrefab = spawnEnemy[Random.Range(0, spawnEnemy.Length)];
            var item = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            var enemy = item.GetComponent<Enemy>();
            enemy.SetTarget(FindFirstObjectByType<CharacterMovement>().transform);
            enemy.onDeath = DecreaseEnemyCount;

            allSpawnEnemies++;

            nowTheEnemies++;

        }
    }

    public void DecreaseEnemyCount()
    {
        nowTheEnemies--;
    }
}
