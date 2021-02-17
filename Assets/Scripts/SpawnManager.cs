using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerups;

    private float _enemySpawnTimer = 5f;

    private float _powerUpMin = 5f;
    private float _powerUpMax = 9f;

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartSpawning(){
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            //It will set a new random position on the x and set the position on the y to 7.5f (Enemy position)
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f),7.5f,0);
            //Spawn an enemy on a random position and store it in newEnemy
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            //Set the newEnemy as a child to the EnemyContainer
            newEnemy.transform.SetParent(_enemyContainer.transform);
            //reduce enemy spawn timer everytime it enters this loop until 0.5f
            if(_enemySpawnTimer > 0.5f){
                _enemySpawnTimer -= 0.1f;
            }
            yield return new WaitForSeconds(_enemySpawnTimer);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f),7.5f,0);
            int randomPowerup = Random.Range(0,_powerups.Length);
            Instantiate(_powerups[randomPowerup], posToSpawn, Quaternion.identity);
            //PowerUp spawn timer goes up every time you enter the loop by 0.05f
            _powerUpMin += 0.05f;
            _powerUpMax += 0.05f;
            yield return new WaitForSeconds(Random.Range(_powerUpMin,_powerUpMax));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
