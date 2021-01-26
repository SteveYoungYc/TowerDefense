using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;
    
    private static List<List<GameObject>> enemyClone;
    
    private int enemyNum = 10;
    private int bornPointsNum = 3;
    private Vector3[] bornPoints;
    
    private Vector3 destination;
    //public delegate EnemyIndexInfo EnemyDeathAction();

    //public static event EnemyDeathAction EnemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        enemyClone = new List<List<GameObject>>();
        bornPoints = new Vector3[bornPointsNum];

        destination = new Vector3(-24.83f, 0.99f, -58.83f);
        bornPoints[0] = new Vector3(86.16f, 2.99f, 22.39f);
        bornPoints[1] = new Vector3(44.11f, 3.17f, 23.33f);
        bornPoints[2] = new Vector3(92.40f, 3.17f, -20.53f);

        for (int i = 0; i < bornPointsNum; i++)
        {
            enemyClone.Add(new List<GameObject>());

            for (int j = 0; j < enemyNum; j++)
            {
                enemyClone[i].Add(Instantiate(enemyPrefab, bornPoints[i], Quaternion.identity));
                enemyClone[i][j].tag = "Enemy";
                enemyClone[i][j].AddComponent<NavMeshAgent>();
                enemyClone[i][j].AddComponent<EnemyProperty>();
                enemyClone[i][j].GetComponent<EnemyProperty>().GetInfo(i, j);
                
                enemyClone[i][j].GetComponent<Animator>().runtimeAnimatorController = animatorController;
                enemyClone[i][j].GetComponent<NavMeshAgent>().destination = destination;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void OneEnemyDie(EnemyIndexInfo index)
    {
        enemyClone[index.i][index.j] = null;
    }
}