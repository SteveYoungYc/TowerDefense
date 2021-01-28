using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;
    
    private static List<List<GameObject>> enemyClone;
    private static List<Vector3> bornPoints;
    
    private static int enemyNum = 20;

    private static Vector3 destination;

    private static Dictionary<EnemyIndex, float> distanceList;

    public static float minDis;
    public float min;
    public int disCount;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyClone = new List<List<GameObject>>();
        bornPoints = new List<Vector3>();
        distanceList = new Dictionary<EnemyIndex, float>();

        destination = new Vector3(-24.83f, 0.99f, -58.83f);
        bornPoints.Add(new Vector3(86.16f, 2.99f, 22.39f));
        bornPoints.Add(new Vector3(44.11f, 3.17f, 23.33f));
        bornPoints.Add(new Vector3(92.40f, 3.17f, -20.53f));

        for (int i = 0; i < bornPoints.Count; i++)
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
                
                distanceList.Add(new EnemyIndex(i, j), Vector3.Distance(enemyClone[i][j].transform.position, destination));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        min = minDis;
        disCount = distanceList.Count;
    }

    public static void OneEnemyDie(EnemyIndex index)
    {
        //distanceList[index] = Mathf.Infinity;
        //distanceList.OrderBy(kvp => kvp.Value);
        //enemyClone[index.i][index.j] = null;
    }

    public static GameObject MostThreatening()
    {
        UpdateDistance();
        if (distanceList.Count == 0) return null;
        var index = distanceList.OrderBy(kvp => kvp.Value).First();
        minDis = index.Value;
        return enemyClone[index.Key.i][index.Key.j];
    }

    private static void UpdateDistance()
    {
        distanceList = new Dictionary<EnemyIndex, float>();
        for (int i = 0; i < bornPoints.Count; i++)
        {
            for (int j = 0; j < enemyNum; j++)
            {
                if(!enemyClone[i][j].gameObject.activeInHierarchy) continue;
                distanceList.Add(new EnemyIndex(i, j), Vector3.Distance(enemyClone[i][j].transform.position, destination));
            }
        }
    }
}