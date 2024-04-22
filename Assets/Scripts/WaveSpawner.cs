using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 20f;
    private float countDown = 2f;

    public Text waveCountDownText;
    public Text waves;

    public int waveIndex = 0;
    private int EnemyCount = 0;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 || countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }else
        {
            countDown -= Time.deltaTime;
        }

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.0}", countDown);
        waves.text = "Wave: " + waveIndex.ToString() + " ";
    }

    IEnumerator SpawnWave()
    {
        EnemyCount++;
        waveIndex++;
        for (int i = 0; i < EnemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
