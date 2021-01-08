using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTurn : MonoBehaviour {
    private Transform tf;
    
    private Vector3 leftRight;
    private Vector3 upDown;
    private Vector3 pos;
    private const float rotateSpeed = 0.5f;

    private GameObject barrel;
    private GameObject turretBase;
    // Start is called before the first frame update
    void Start() {
        this.transform.position = new Vector3(0,0,2);

        barrel = GameObject.FindGameObjectWithTag("TurretBarrel");
    }

    // Update is called once per frame
    void Update() {
        /*
        tf = this.transform;
        toward = tf.eulerAngles;
        pos = tf.position;
        */
        if (Input.GetKey(KeyCode.W)) {
            upDown = new Vector3(-rotateSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S)) {
            upDown = new Vector3(rotateSpeed, 0, 0);
        }
        else {
            upDown = new Vector3(0, 0, 0);
        }
        
        if (Input.GetKey(KeyCode.A)) {
            leftRight = new Vector3(0, -rotateSpeed, 0);
        }        
        else if (Input.GetKey(KeyCode.D)) {
            leftRight = new Vector3(0, rotateSpeed, 0);
        }
        else {
            leftRight = new Vector3(0, 0, 0);
        }
        
        this.transform.Rotate(leftRight,Space.Self);
        barrel.transform.Rotate(upDown,Space.Self);
    }
}
