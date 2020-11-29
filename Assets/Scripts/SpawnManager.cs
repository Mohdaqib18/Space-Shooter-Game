using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
  
    [SerializeField]
    private GameObject[] _powerUp;

    // Start is called before the first frame update
 


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

        

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7, 0);
            GameObject newEnemy =  Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {

        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.4f, 9.4f), 7, 0);
            int randomPowerUp = Random.Range(0, 4 );
            Instantiate(_powerUp[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,8f));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
