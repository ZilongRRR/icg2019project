using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour {
    SpriteRenderer m_TargetRenderer;
	// Use this for initialization
	void Start () {
        m_TargetRenderer = this.GetComponent <SpriteRenderer> ();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_TargetRenderer.color = Color.red;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        m_TargetRenderer.color = Color.white;
    }
}
