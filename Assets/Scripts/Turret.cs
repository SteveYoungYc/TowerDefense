using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Vector3 leftRight;
    private Vector3 upDown;
    private Vector3 toward;
    
    private const float rotateSpeed = 100f;

    private GameObject barrel;
    private GameObject gunBarrel;

    private bool hasEnemy;
    
    private Ray ray;
    private LineRenderer laser;
    private AudioClip laserAudioClip;
    private AudioSource laseAudioSource;

    public delegate void ShootEvent(int type, Vector3 pos);
    public static event ShootEvent ShootAction;
    
    // Start is called before the first frame update
    void Start()
    {
        barrel = transform.Find("gun barrel").gameObject;
        gunBarrel = barrel.transform.Find("Cylinder").gameObject;

        LaserSetup();
        AudioSetup();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (EnemyManager.MostThreatening() == null)
        {
            hasEnemy = false;
            //AutoTurn(Vector3.back);
        }
        else
        {
            hasEnemy = true;
            //AutoTurn(EnemyManager.MostThreatening().GetComponent<EnemyProperty>().aimPoint);
            AutoTurn(EnemyManager.MostThreatening().transform.position);
        }

        Shoot();
    }
    
    private void ManualTurn() {
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
    
    private void AutoTurn(Vector3 pos)
    {
        Vector3 angle = LookRotation(pos - barrel.transform.position);
        transform.localEulerAngles = new Vector3(0, angle.y, 0);
        barrel.transform.localEulerAngles = new Vector3(angle.x, 0, angle.z);
        ShootAction(1, pos);
    }

    private void Shoot()
    {
        if (hasEnemy)
        {
            toward = new Vector3(Mathf.Sin(transform.eulerAngles.y / 180f * Mathf.PI),
                Mathf.Tan(-barrel.transform.localEulerAngles.x / 180f * Mathf.PI),
                Mathf.Cos(transform.eulerAngles.y / 180f * Mathf.PI));
            ray = new Ray(gunBarrel.transform.position, Vector3.Normalize(toward));
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //print(hit.collider.name);
                if (!hit.collider.CompareTag("Head"))
                {
                    //laser.forceRenderingOff = true;
                    //laseAudioSource.mute = true;
                    //return;
                }
                else
                {
                    //print(hit.collider.tag);
                }
                laser.forceRenderingOff = false;
                laseAudioSource.mute = false;
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                laser.SetPosition(1, hit.point);
            }
        }
        else
        {
            laseAudioSource.mute = true;
            laser.SetPosition(1, barrel.transform.position);
        }
    }
    
    void LaserSetup()
    {
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;
        laser.startWidth = 0.08f;
        laser.endWidth = 0.08f;
        laser.startColor = Color.magenta;
        laser.endColor = Color.cyan;
        laser.SetPosition(0, barrel.transform.position);
    }

    void AudioSetup()
    {
        laserAudioClip = Resources.Load<AudioClip>("Audio/LaserShoot1");
        laseAudioSource = gameObject.GetComponent<AudioSource>();
        laseAudioSource.clip = laserAudioClip;
        laseAudioSource.volume = 0.8f;
        laseAudioSource.loop = true;
        laseAudioSource.Play();
    }
    
    private Vector3 LookRotation(Vector3 fromDir)
    {
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