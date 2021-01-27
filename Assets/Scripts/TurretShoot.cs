using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    private GameObject barrel;
    private GameObject gunBarrel;
    private Vector3 toward;
    private Ray ray;
    
    private LineRenderer laser;

    private bool findEnemy = true;

    private AudioClip laserAudioClip;
    private AudioSource laseAudioSource;
    
    public void ShowMsg(bool msg)
    {
        findEnemy = msg;
    }
    
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
        if (findEnemy)
        {
            toward = new Vector3(Mathf.Sin(this.transform.eulerAngles.y / 180f * Mathf.PI),
                Mathf.Tan(-barrel.transform.localEulerAngles.x / 180f * Mathf.PI),
                Mathf.Cos(this.transform.eulerAngles.y / 180f * Mathf.PI));
            ray = new Ray(gunBarrel.transform.position, Vector3.Normalize(toward));
            RaycastHit hit;
            laseAudioSource.mute = false;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.Log("碰撞对象: " + hit.collider.name);
                Debug.DrawLine(ray.origin, hit.point, Color.red);
                laser.SetPosition(1, hit.point);
            }

            //ShootAction(1, e);
        }
        else
        {
            laseAudioSource.mute = true;
            laser.SetPosition(1, barrel.transform.position);
            //ShootAction(0);
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
}
