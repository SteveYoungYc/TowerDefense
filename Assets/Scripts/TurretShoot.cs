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
        //print(this.transform.eulerAngles);
        toward = new Vector3(Mathf.Sin(this.transform.eulerAngles.y / 180 * Mathf.PI), 0, Mathf.Cos(this.transform.eulerAngles.y / 180 * Mathf.PI));
        ray = new Ray(gunBarrel.transform.position, toward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            // 如果射线与平面碰撞，打印碰撞物体信息
            Debug.Log("碰撞对象: " + hit.collider.name);
            // 在场景视图中绘制射线
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
}
