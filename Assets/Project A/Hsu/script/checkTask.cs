using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkTask : MonoBehaviour {
    public bool isCheck = false;
    SpriteRenderer checkMark;
    List<int> triggerRecord = new List<int> ();
    int count = 0;
    void OnTriggerEnter2D (Collider2D other) {
        count++;
        if (!triggerRecord.Contains (other.GetInstanceID ())) { triggerRecord.Add (other.GetInstanceID ()); }
    }

    void OnTriggerStay2D (Collider2D other) {

    }
    void OnTriggerExit2D (Collider2D other) {
        count--;
        // Debug.Log(triggerRecord.Count);
        // Debug.Log(count);
    }
    // Start is called before the first frame update
    void Start () {
        checkMark = this.GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update () {
        if (count == 0 && triggerRecord.Count == 7 && !isCheck) {
            isCheck = true;
            checkMark.color = Color.green;
        }
    }
}