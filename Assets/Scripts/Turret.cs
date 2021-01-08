using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    private Transform tf;

    private Vector3 toward;
    private Vector3 pos;
    private const float rotateSpeed = 0.1f;
    // Start is called before the first frame update
    void Start() {
        tf = this.transform;
        toward = tf.eulerAngles;
        pos = tf.position;
        pos = new Vector3(0,0,2);
        tf.position = pos;
    }

    // Update is called once per frame
    void Update() {
        /*
        tf = this.transform;
        toward = tf.eulerAngles;
        pos = tf.position;
        */
        if (Input.GetKey(KeyCode.W)) {
            toward = new Vector3(-rotateSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S)) {
            toward = new Vector3(rotateSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A)) {
            toward = new Vector3(0, -rotateSpeed, 0);
        }        
        else if (Input.GetKey(KeyCode.D)) {
            toward = new Vector3(0, rotateSpeed, 0);
        }
        else {
            toward = new Vector3(0, 0, 0);
        }
        tf.Rotate(toward,Space.Self);
    }
}
