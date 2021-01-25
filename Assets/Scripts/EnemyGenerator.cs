using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AnimatorController animatorController;
    private GameObject[] enemyClone;
    private int enemyNum = 100;
    private Animator[] enemyAnimator;

    private NavMeshAgent[] navAgents;
    private Camera mainCamera;

    private int bornPointsNum = 3;
    private Vector3 destination;

    private Vector3[] bornPoints;

    // Start is called before the first frame update
    void Start()
    {
        enemyClone = new GameObject[enemyNum];
        enemyAnimator = new Animator[enemyNum];
        navAgents = new NavMeshAgent[enemyNum];
        bornPoints = new Vector3[bornPointsNum];

        destination = new Vector3(-24.83f, 0.99f, -58.83f);
        bornPoints[0] = new Vector3(86.16f, 2.99f, 22.39f);
        bornPoints[1] = new Vector3(44.11f, 3.17f, 23.33f);
        bornPoints[2] = new Vector3(92.40f, 3.17f, -20.53f);

        for (int i = 0; i < enemyNum; i++)
        {
            enemyClone[i] = Instantiate(enemyPrefab, bornPoints[0], Quaternion.identity);
            enemyClone[i].tag = "Enemy";
            enemyAnimator[i] = enemyClone[i].GetComponent<Animator>();
            enemyAnimator[i].runtimeAnimatorController = animatorController;
            enemyClone[i].AddComponent<NavMeshAgent>();
            navAgents[i] = enemyClone[i].GetComponent<NavMeshAgent>();
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
            }
        }

        foreach (var nav in navAgents)
        {
            nav.SetDestination(destination);
        }

        for (int i = 0; i < enemyNum; i++)
        {
            enemyAnimator[i].SetBool("startRun",
                !(Vector3.Distance(enemyClone[i].transform.position, navAgents[i].destination) < 0.3 * enemyNum));
        }
    }
}