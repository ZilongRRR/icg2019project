using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectBBed : MonoBehaviour {
    void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == ProjectBConfig.BABY_TAG) {
            ProjectBGameMain.Instance.GetScore ();
            Destroy (other.gameObject);
        }
    }
}