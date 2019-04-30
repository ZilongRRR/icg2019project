using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTools;
public class ObstacleTrigger : MonoBehaviour {
    public int score;
    AudioSource audioSource;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start () {
        audioSource = GetComponent<AudioSource> ();
    }
    void OnTriggerEnter2D (Collider2D other) {
        GradeManager.Instance.LoseScore (score);
    }
    private void OnCollisionEnter2D (Collision2D other) {
        GradeManager.Instance.LoseScore (score);
        audioSource.Play ();
    }
}