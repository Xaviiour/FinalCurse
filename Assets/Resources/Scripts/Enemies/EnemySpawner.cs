using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterReferences;
    [SerializeField] private GameObject rightPos;
    [SerializeField] private GameObject leftPos;
    //[SerializeField] private EnemyData data;
    private GameObject spawnedEnemy;
    private GameObject player;
    private Health h;
    private EnemySpawner e;

    private int randomIndex;

    private void Start()
    {
        StartCoroutine(SpawnEnemiesR());
        StartCoroutine(SpawnEnemiesL());
    }

    IEnumerator SpawnEnemiesR()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 5));

            randomIndex = Random.Range(0, monsterReferences.Length);

            spawnedEnemy = Instantiate(monsterReferences[randomIndex]);


            spawnedEnemy.transform.position = rightPos.transform.position;
            spawnedEnemy.GetComponent<EnemyLogic>();

            if (spawnedEnemy.transform.position.x >= 310)
            {
                yield break;
            }
        }
    }

    IEnumerator SpawnEnemiesL()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, monsterReferences.Length);

            spawnedEnemy = Instantiate(monsterReferences[randomIndex]);


            spawnedEnemy.transform.position = leftPos.transform.position;
            spawnedEnemy.GetComponent<EnemyLogic>();

            if (spawnedEnemy.transform.position.x >= 310)
            {
                if (spawnedEnemy.transform.position.x <= 5)
                {
                    yield break;
                }
                yield break;
            }

        }
    }
}