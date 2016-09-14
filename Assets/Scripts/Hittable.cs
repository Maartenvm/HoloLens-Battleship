using UnityEngine;
using System.Collections;

public class Hittable : MonoBehaviour {
    void onHit() {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.useGravity = true;
    }

    void onDrag() {

    }
}
