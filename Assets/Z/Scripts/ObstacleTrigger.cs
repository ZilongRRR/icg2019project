using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZTools;
public class ObstacleTrigger : MonoBehaviour
{
    public int score;
    void OnTriggerEnter2D(Collider2D other)
    {
        GradeManager.Instance.LoseScore(score);
    }
}
