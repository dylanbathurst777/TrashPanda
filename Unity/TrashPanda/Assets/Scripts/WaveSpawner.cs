using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState {  SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;
       public int randomTrain;
        
    }

    public Wave[] waves;
    public int daysAlive = 0;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;
    public int waveCountdownint;
    private float searchCountdown = 1f;

    public  Enemy enemyscript;

    public UIM uiMan;


    public SpawnState state = SpawnState.COUNTING;

	
	void Start ()
    {
       
        uiMan = FindObjectOfType<UIM>();
        uiMan.sunImage.gameObject.SetActive(true);
        uiMan.moonImage.gameObject.SetActive(false);
        waveCountdown = timeBetweenWaves;
        waveCountdownint = Convert.ToInt32(waveCountdown); 
        uiMan.DaylightText.text =  waveCountdownint.ToString();
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawnpoints referencede");
        }

    }
	
	
	void Update ()
    {
        waveCountdownint = Convert.ToInt32(waveCountdown);
        uiMan.DaylightText.text = waveCountdownint.ToString();

        if (state == SpawnState.WAITING)
        {
            uiMan.sunImage.gameObject.SetActive(false);
            uiMan.moonImage.gameObject.SetActive(true);
            //check if enemys are still alive;
            if (!EnemyIsAlive())
            {
                //begin new wave Change to day time;

                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
    
        }

		if(waveCountdown <= 0)
        {
               if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));

            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
	}
    void WaveCompleted()
    {
        Debug.Log("WaveCompleted");


        waveCountdown = timeBetweenWaves;

        state = SpawnState.COUNTING;
        uiMan.sunImage.gameObject.SetActive(true);
        uiMan.moonImage.gameObject.SetActive(false);

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            enemyscript.maxHealth = enemyscript.maxHealth * 2;
            Debug.Log("AllwavesComplete! Looping");
        }
        else
        {
            nextWave++;
            daysAlive++;
        }
      

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
              
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < _wave.count; i++)
        {
           _wave.randomTrain = UnityEngine.Random.RandomRange(0, 5);
           
            SpawnEnemy(_wave.enemy[_wave.randomTrain]);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);
        //spawnEnemy

       
        Transform _sp = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, transform.rotation);
       
    }
}
