using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Missile : MonoBehaviour
{
    
    private List<Vector3> pointsList;

    public int state;

    private float circleTime;
    private bool circleFlag;
    private Vector3 worldUp;

    private float verticalHeight = 10f;
    private float speed;
    private readonly float maxSpeed = 2;

    public GameObject bigExplodeEffectPrefab;

    private Vector3 targetPos;
    
    Vector3 lastSpeed;
 
    //旋转的速度，单位 度/秒
    int rotateSpeed = 900;
 
    //目标到自身连线的向量，最终朝向
    Vector3 finalForward;
 
    //自己的forward朝向和mFinalForward之间的夹角
    float angleOffset;
    Vector3 speed3 = new Vector3(0, 0, 20);

    public Vector3 forward;

    private void Awake()
    {
        //transform.position = new Vector3(0, 0, 0);
        pointsList = new List<Vector3>();
        state = 0;
        circleFlag = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
        verticalHeight = 10f;
        //SetTarget(new Vector3(0, 0, 0));
        speed3 = transform.TransformDirection(speed3);
    }

    // Update is called once per frame
    void Update()
    {
        //TrackOrbit();
        Fly();
        forward = transform.forward;
    }

    private void Fly()
    {
        StateSwitch();
        switch (state)
        {
            case -1: break;
            case 0: LinearOrbit(pointsList[0], pointsList[1], speed); break;
            case 1: CircleOrbit(pointsList[1], pointsList[2], speed); break;
            case 2: LinearOrbit(pointsList[2], pointsList[3], speed * 1.5f); break;
        }
    }

    private void CircleOrbit(Vector3 start, Vector3 end, float sp)
    {
        Vector3 center = (start + end) / 2 + new Vector3(0, -0.2f, 0);
        float radius = Vector3.Distance(center, start);
        transform.position = Vector3.Slerp(start - center, end - center, (Time.time - circleTime) * (sp / radius * 6)) + center;
        transform.LookAt(center, worldUp);
    }

    private void LinearOrbit(Vector3 start, Vector3 end, float sp)
    {
        transform.Translate((end - start) * (Time.deltaTime * sp), Space.World);
    }

    private void TrackOrbit()
    {
        transform.Translate((targetPos - transform.position) * (Time.deltaTime * speed), Space.World);
        //transform.LookAt(targetPos, worldUp);
    }
    
    
    void CheckHit()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.transform.position == targetPos && hit.distance < 1)
            {
                Destroy(gameObject);
            }
        }
    }
    
    private void ChangeForward(float speed)
    {
        //获得目标点到自身的朝向
        finalForward = (targetPos - transform.position).normalized;
        if (finalForward != transform.forward)
        {
            angleOffset = Vector3.Angle(transform.forward, finalForward);
            if (angleOffset > rotateSpeed)
            {
                angleOffset = rotateSpeed;
            }
            //将自身forward朝向慢慢转向最终朝向
            transform.forward = Vector3.Lerp(transform.forward, finalForward, speed / angleOffset);
        }
    }

    private void StateSwitch()
    {
        //print(transform.position);
        if (Vector3.Distance(transform.position, pointsList[0]) < 0.1)
        {
            state = 0;
        }
        
        if (Vector3.Distance(transform.position, pointsList[1]) < 0.5)
        {
            state = 1;
            if(circleFlag)
                circleTime = Time.time;
            circleFlag = false;
        }
        else if (Vector3.Distance(transform.position, pointsList[1]) < 1)
        {
            //speed = 1;
        }
        else
        {
            //speed = maxSpeed;
        }
        
        if (Vector3.Distance(transform.position, pointsList[2]) < 0.5)
        {
            state = 2;
        }
        if (Vector3.Distance(transform.position, pointsList[3]) < 0.5)
        {
            state = -1;
            Instantiate(bigExplodeEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 target)
    {
        targetPos = target;
        var position = transform.position;
        if (targetPos.y - position.y > verticalHeight)
        {
            Debug.LogError("The target is too high.");
        }
        pointsList = new List<Vector3>
        {
            position,
            new Vector3(position.x, position.y + verticalHeight, position.z),
            new Vector3(targetPos.x, position.y + verticalHeight, targetPos.z),
            targetPos
        };
        worldUp = Vector3.Normalize(new Vector3(pointsList[3].x - pointsList[0].x, 0, pointsList[3].z - pointsList[0].z));
    }
}
