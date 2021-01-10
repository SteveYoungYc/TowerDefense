using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour {
    private GameObject barrel;
    private GameObject gunBarrel;
    private Vector3 toward;
    private Ray ray;
    // Start is called before the first frame update
    void Start() {
        barrel = GameObject.FindGameObjectWithTag("TurretBarrel");
        gunBarrel = GameObject.Find("Cylinder");
    }

    // Update is called once per frame
    void Update() {
        toward = new Vector3(Mathf.Sin(this.transform.eulerAngles.y / 180f * Mathf.PI), 
                            Mathf.Tan(-barrel.transform.localEulerAngles.x / 180f * Mathf.PI), 
                            Mathf.Cos(this.transform.eulerAngles.y / 180f * Mathf.PI));
        ray = new Ray(gunBarrel.transform.position, Vector3.Normalize(toward));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            Debug.Log("碰撞对象: " + hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
}
