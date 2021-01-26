using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;
    private static GameObject[,] enemyClone;
    private int enemyNum = 10;
    private static Animator[,] enemyAnimator;

    private static NavMeshAgent[,] navAgents;
    private Camera mainCamera;

    private int bornPointsNum = 3;
    private Vector3 destination;

    private Vector3[] bornPoints;

    //public delegate EnemyIndexInfo EnemyDeathAction();

    //public static event EnemyDeathAction EnemyDeath;
    // Start is called before the first frame update
    void Start()
    {
        enemyClone = new GameObject[bornPointsNum, enemyNum];
        enemyAnimator = new Animator[bornPointsNum, enemyNum];
        navAgents = new NavMeshAgent[bornPointsNum, enemyNum];
        bornPoints = new Vector3[bornPointsNum];

        destination = new Vector3(-24.83f, 0.99f, -58.83f);
        bornPoints[0] = new Vector3(86.16f, 2.99f, 22.39f);
        bornPoints[1] = new Vector3(44.11f, 3.17f, 23.33f);
        bornPoints[2] = new Vector3(92.40f, 3.17f, -20.53f);

        for (int i = 0; i < bornPointsNum; i++)
        {
            for (int j = 0; j < enemyNum; j++)
            {
                enemyClone[i, j] = Instantiate(enemyPrefab, bornPoints[i], Quaternion.identity);
                enemyClone[i, j].tag = "Enemy";
                enemyClone[i, j].AddComponent<NavMeshAgent>();
                enemyClone[i, j].AddComponent<EnemyProperty>();
                enemyClone[i, j].GetComponent<EnemyProperty>().GetInfo(i, j);

                enemyAnimator[i, j] = enemyClone[i, j].GetComponent<Animator>();
                enemyAnimator[i, j].runtimeAnimatorController = animatorController;

                navAgents[i, j] = enemyClone[i, j].GetComponent<NavMeshAgent>();
                navAgents[i, j].destination = destination;
            }
        }

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FindPath();
    }

    private void FindPath()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map"));
            if (isCollider)
            {
                foreach (var nav in navAgents)
                {
                    nav.SetDestination(hit.point);
                }
            }
        }

        for (int i = 0; i < bornPointsNum; i++)
        {
            for (int j = 0; j < enemyNum; j++)
            {
                enemyAnimator[i, j].SetBool("startRun",
                    !(Vector3.Distance(enemyClone[i, j].transform.position, navAgents[i, j].destination) < 0.3 * enemyNum));
            }
        }
    }

    public static void OneEnemyDie(EnemyIndexInfo index)
    {
        
    }
}