using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    private Transform spawnPoint;

    public float timeBetweenWaves = 30f;
    private float countDown = 2f;

    public Text waveCountDownText;
    public Text waves;

    public static int waveIndex;
    public int startWaveIndex = 0;
    private int EnemyCount = 0;

    private Transform startPointObject;

    private GameController gameController;

    private void Start()
    {
        waveIndex = startWaveIndex;
        gameController = FindFirstObjectByType<GameController>();
    }

    private void Update()
    {
        // Check if both start and end points are placed
        if (GameController.startPointPlaced && GameController.endPointPlaced)
        {
            startPointObject = GameObject.FindGameObjectWithTag("StartPoint").transform;
            // Start counting down and spawn waves
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 || countDown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countDown = timeBetweenWaves;
            }
            else
            {
                countDown -= Time.deltaTime;
            }
        }

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.0}", countDown);
        waves.text = "Wave: " + waveIndex.ToString() + " ";
    }

    IEnumerator SpawnWave()
    {
        EnemyCount++;
        waveIndex++;
        PlayerStats.round++;
        for (int i = 0; i < EnemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        if (startPointObject != null)
        {
            Instantiate(enemyPrefab, startPointObject.position, startPointObject.rotation);
        }
        else
        {
            Debug.LogError("Spawn point is null. Enemy cannot be spawned.");
        }
    }
}
