using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    private readonly float moveSpeed = 1.0f;
    private Vector3 dir = Vector3.forward;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.W)) {
            dir = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S)) {
            dir = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A)) {
            dir = Vector3.left;
        }        
        else if (Input.GetKey(KeyCode.D)) {
            dir = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.Space)) {
            dir = Vector3.up;
        }        
        else if (Input.GetKey(KeyCode.LeftShift)) {
            dir = Vector3.down;
        }
        else {
            dir = new Vector3(0, 0, 0);
        }
        this.transform.Translate(dir * Time.deltaTime * moveSpeed, Space.Self);
    }
}
