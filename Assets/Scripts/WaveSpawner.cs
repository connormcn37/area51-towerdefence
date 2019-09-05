using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform destination;

    public float timeBetweenWaves = 5f;
    public float spawnDelay = 0.5f;
    private float countdown = 2f;
    private int waveNumber = 1;
    public Transform spawnPoint;
    public Text waveCountdownText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown < 0f){
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Ceil(countdown).ToString();
    }

    IEnumerator SpawnWave(){
        for (int i=0; i<waveNumber; i++){
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
        waveNumber++;
    }

    void SpawnEnemy(){
        // GameObject e = (GameObject)
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        //e.GetComponent<Enemy>().PushDestination(destination.position);
    }
}
