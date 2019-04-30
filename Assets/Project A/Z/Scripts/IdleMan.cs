using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class IdleMan : MonoBehaviour {
    public GameObject blood;
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float walkSpeed = 0.1f;
    public float RANDOM_X_MAX = 13;
    public float RANDOM_Y_MAX = 20;
    public void SetSprite (int i) {
        spriteRenderer.sprite = sprites[i];
    }

    // Start is called before the first frame update
    void Start () {
        RandomWalk ();
    }

    void RandomWalk () {
        float rnadomX = Random.Range (-RANDOM_X_MAX, RANDOM_X_MAX);
        float rnadomY = Random.Range (-RANDOM_Y_MAX, RANDOM_Y_MAX);
        Vector2 newPosition = new Vector2 (rnadomX, rnadomY);

        float distance = Vector3.Distance (this.transform.position, newPosition);
        float walkTime = distance / walkSpeed;
        this.transform.LookAt (newPosition);
        this.transform.Rotate (new Vector3 (0, 90, 0));
        this.transform.DOMove (newPosition, walkTime).OnComplete (() => RandomWalk ());
    }
    void OnTriggerEnter2D (Collider2D other) {
        Instantiate (blood, this.transform.position, Quaternion.identity);
        ZTools.GradeManager.Instance.LoseScore (3);
        ZTools.GradeManager.Instance.PeopleDie ();
        Destroy (this.gameObject);
    }

    public void SetSprite (int i, Sprite sp) {
        sprites[i] = sp;
    }
}