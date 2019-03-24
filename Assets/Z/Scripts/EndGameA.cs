using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGameA : MonoBehaviour {
    public CarEntity carEntity;
    public GameObject endCanvas;
    public TextMeshProUGUI text;
    public checkTask[] checkTasks;

    private void OnTriggerEnter2D (Collider2D other) {
        carEntity.Stop ();
        carEntity.enabled = false;
        endCanvas.SetActive (true);
        int score = ZTools.GradeManager.Instance.originScore;
        if (score < 70) {
            text.text = "FAILURE";
            text.color = Color.red;
        } else {
            text.text = "SUCCESS";
        }
        for (int i = 0; i < checkTasks.Length; i++) {
            if (!checkTasks[i].isCheck) {
                text.text = "FAILURE";
                text.color = Color.red;
            }
        }
        text.DOFade (1, 0.3f);
        text.rectTransform.DOAnchorPosY (0, 0.3f);
    }

    public void ReLoad () {
        SceneManager.LoadScene ("projectA", LoadSceneMode.Single);
    }
}