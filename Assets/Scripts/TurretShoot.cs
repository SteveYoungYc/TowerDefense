using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {
    private GameObject barrel;
    private GameObject gunBarrel;
    private Vector3 toward;
    private Ray ray;
    
    private LineRenderer laser;

    private bool findEnemy = true;

    private AudioClip laserAudioClip;
    public void ShowMsg(string msg) {
        print(msg);
        if (msg == "1") {
            findEnemy = true;
        }
        else {
            findEnemy = false;
        }
    }
    
    // Start is called before the first frame update
    void Start() {
        barrel = GameObject.FindGameObjectWithTag("TurretBarrel");
        gunBarrel = GameObject.Find("Cylinder");
        
        laser = GetComponent<LineRenderer>();
        laser.positionCount = 2;
        laser.startWidth = 0.08f;
        laser.endWidth = 0.08f;
        laser.startColor = Color.magenta;
        laser.endColor = Color.cyan;
        laser.SetPosition(0, barrel.transform.position);
        
        laserAudioClip = Resources.Load<AudioClip>("Audio/LaserShoot1");
        this.gameObject.GetComponent<AudioSource>().clip = laserAudioClip;
        this.gameObject.GetComponent<AudioSource>().volume = 0.8f;
        this.gameObject.GetComponent<AudioSource>().loop = true;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update() {
        if (findEnemy) {
            toward = new Vector3(Mathf.Sin(this.transform.eulerAngles.y / 180f * Mathf.PI),
                Mathf.Tan(-barrel.transform.localEulerAngles.x / 180f * Mathf.PI),
                Mathf.Cos(this.transform.eulerAngles.y / 180f * Mathf.PI));
            ray = new Ray(gunBarrel.transform.position, Vector3.Normalize(toward));
            RaycastHit hit;
            this.gameObject.GetComponent<AudioSource>().mute = false;
            
            //yield return new WaitForSeconds(laserAudioClip.length);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                //Debug.Log("碰撞对象: " + hit.collider.name);
                //Debug.DrawLine(ray.origin, hit.point, Color.red);
                laser.SetPosition(1, hit.point);
            }
        }
        else {
            this.gameObject.GetComponent<AudioSource>().mute = true;
            laser.SetPosition(1, barrel.transform.position);
        }
    }
}
