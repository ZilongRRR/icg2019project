using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectBBed : MonoBehaviour {
    Rigidbody rg;
    private void Start()
    {
        rg = this.GetComponent<Rigidbody>();
    }
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == ProjectBConfig.BABY_TAG) {
            rg.velocity = Vector3.zero;
            ProjectBGameMain.Instance.GetScore ();
            Destroy (other.gameObject);
        }
    }
}