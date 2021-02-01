using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float HP;
    public bool isDead;

    //private HitState state;

    private EnemyIndex enemyIndex;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Camera mainCamera;

    private GameObject head;
    public Vector3 aimPoint;

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
        isDead = false;

        //enemyIndex = new EnemyIndex(); 这一条语句后于GetIndex中的
        animator = GetComponent<Animator>();
        animator.SetBool("die", false);

        navMeshAgent = GetComponent<NavMeshAgent>();

        head = transform.Find("Z_Head").gameObject;

        head.tag = "Head";
        
        Turret.ShootAction += Hit;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            AnimationControl();
        }
        else
        {
            StopMove();
            StartCoroutine(Die());
        }
        //aimPoint = head.transform.position + skinnedMeshRenderer.bounds.center;
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

    private void StopMove()
    {
        navMeshAgent.SetDestination(transform.position);
    }

    private void AnimationControl()
    {
        animator.SetBool("startRun", !(Vector3.Distance(transform.position, navMeshAgent.destination) < 1));
    }

    private void Hit(int type, Vector3 pos)
    {
        if (pos != transform.position) return;
        if (type == 1)
        {
            HP -= Time.deltaTime * 20;
        }

        if (HP <= 0)
        {
            isDead = true;
            EnemyManager.OneEnemyDie(enemyIndex);
            animator.SetBool("die", true);
            Turret.ShootAction -= Hit;
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2.0f);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Z_FallingForward"))
        {
            gameObject.SetActive(false);
        }
    }

    public void GetIndex(int i, int j)
    {
        enemyIndex = new EnemyIndex(i, j);
        //numInfo.bornPointsNum = bornPointsNum;
        //numInfo.enemyPointsNum = enemyNum;
    }
}
