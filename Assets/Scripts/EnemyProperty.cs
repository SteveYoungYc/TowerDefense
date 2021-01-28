using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyProperty : MonoBehaviour
{
    public float HP;

    private HitState state;

    private EnemyIndex enemyIndexInfo;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Camera mainCamera;

    private GameObject head;
    public Vector3 aimPoint;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private enum HitState
    {
        None,
        Laser,
        Bullet
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = 100f;
        
        enemyIndexInfo = new EnemyIndex();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        head = transform.Find("Z_Head").gameObject;
        skinnedMeshRenderer = head.GetComponent<SkinnedMeshRenderer>();
        
        head.tag = "Head";

        TurretTurn.ShootAction += Hit;
        
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimationControl();
        aimPoint = head.transform.position + skinnedMeshRenderer.bounds.center;
        //print(aimPoint);
    }

    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map"));
            if (isCollider)
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }
    }

    private void AnimationControl()
    {
        animator.SetBool("startRun", !(Vector3.Distance(transform.position, navMeshAgent.destination) < 1));
    }

    private void Hit(int type, Vector3 pos)
    {
        if(pos != transform.position) return;
        
        if (IsDead())
        {
            EnemyManager.OneEnemyDie(enemyIndexInfo);
            gameObject.SetActive(false);
            TurretTurn.ShootAction -= Hit;
        }
        
        if (type == 1)
        {
            HP -= Time.deltaTime * 20;
        }
    }
    
    private bool IsDead()
    {
        return HP <= 0;
    }

    public void GetInfo(int i, int j)
    {
        enemyIndexInfo.i = i;
        enemyIndexInfo.j = j;
        //numInfo.bornPointsNum = bornPointsNum;
        //numInfo.enemyPointsNum = enemyNum;
    }
}
