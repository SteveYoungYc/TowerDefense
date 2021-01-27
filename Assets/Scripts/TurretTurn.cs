using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTurn : MonoBehaviour {
    private Transform tf;
    
    private Vector3 leftRight;
    private Vector3 upDown;
    private const float rotateSpeed = 100f;

    private GameObject barrel;
    private GameObject turretBase;

    private bool hasEnemy;
    private int enemyHitIndex;

    public delegate void Shoot(int type, Vector3 pos);
    public static event Shoot ShootAction;
    // Start is called before the first frame update
    void Start() {
        barrel = transform.Find("gun barrel").gameObject;
    }
    
    // Update is called once per frame
    void Update() {
        //ManualTurn();
        if (EnemyManager.MostThreatening() == null)
        {
            hasEnemy = false;
            //AutoTurn(Vector3.back);
        }
        else
        {
            hasEnemy = true;
            AutoTurn(EnemyManager.MostThreatening().transform.position);
        }
    }
    
    void ManualTurn() {
        if (Input.GetKey(KeyCode.W)) {
            upDown = new Vector3(-rotateSpeed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S)) {
            upDown = new Vector3(rotateSpeed * Time.deltaTime, 0, 0);
        }
        else {
            upDown = new Vector3(0, 0, 0);
        }
    
        if (Input.GetKey(KeyCode.A)) {
            leftRight = new Vector3(0, -rotateSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D)) {
            leftRight = new Vector3(0, rotateSpeed * Time.deltaTime, 0);
        }
        else {
            leftRight = new Vector3(0, 0, 0);
        }
        this.transform.Rotate(leftRight,Space.Self);
        barrel.transform.Rotate(upDown,Space.Self);
    }
    
    void AutoTurn(Vector3 pos) {
        Vector3 angle = LookRotation(pos - barrel.transform.position);
        //print(name + barrel.transform.position);
        //Quaternion yawQuaternion = Quaternion.Euler(new Vector3(0, angle.y, 0));
        //Quaternion pitchQuaternion = Quaternion.Euler(new Vector3(angle.x, 0, angle.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation, yawQuaternion, rotateSpeed * Time.deltaTime);
        //barrel.transform.rotation = Quaternion.Slerp(barrel.transform.rotation, pitchQuaternion, rotateSpeed * Time.deltaTime);
        if (Vector3.Distance(pos, transform.position) < 10) {
            transform.localEulerAngles = new Vector3(0, angle.y, 0);
            barrel.transform.localEulerAngles = new Vector3(angle.x, 0, angle.z);
            ShootAction(1, pos);
        }
        SendMessage("ShowMsg", hasEnemy);
    }
    
    private Vector3 LookRotation(Vector3 fromDir) {
        Vector3 eulerAngles = new Vector3();
 
        //AngleX = arc cos(sqrt((x^2 + z^2)/(x^2+y^2+z^2)))
        eulerAngles.x = Mathf.Acos(Mathf.Sqrt((fromDir.x * fromDir.x + fromDir.z * fromDir.z) / (fromDir.x * fromDir.x + fromDir.y * fromDir.y + fromDir.z * fromDir.z))) * Mathf.Rad2Deg;
        if (fromDir.y > 0) eulerAngles.x = 360 - eulerAngles.x;
 
        //AngleY = arc tan(x/z)
        eulerAngles.y = Mathf.Atan2(fromDir.x, fromDir.z) * Mathf.Rad2Deg;
        if (eulerAngles.y < 0) eulerAngles.y += 180;
        if (fromDir.x < 0) eulerAngles.y += 180;
        //AngleZ = 0
        eulerAngles.z = 0; 
        return eulerAngles;
    }
    
}
