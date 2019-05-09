using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZTools;
public class ProjectBGameMain : Singleton<ProjectBGameMain> {
    [Header ("開始頁面")]
    public Image startImage;
    [Header ("倒數計時")]
    public int time = 180;
    public TextMeshProUGUI timeText;
    [Header ("分數")]
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public Vector3 textScale;
    public Ease textEase;
    [Header ("設置模型")]
    public GameObject baby;
    public int babyNumber = 20;
    public Transform randomBabyCenter;
    public float randomPositionRange;
    public Transform bed;
    public Transform randomBedCenter;

    void Start () {

    }

    public void GameStart () {
        startImage.DOFillAmount (0.12f, 0.5f);
        StartCoroutine (CountDown ());
        StartCoroutine (CreateBaby ());
    }

    IEnumerator CreateBaby () {
        float x = Random.Range (randomBabyCenter.position.x - randomPositionRange, randomBabyCenter.position.x + randomPositionRange);
        float y = Random.Range (randomBabyCenter.position.y - randomPositionRange, randomBabyCenter.position.y + randomPositionRange);
        Instantiate (baby, new Vector3 (x, y, randomBabyCenter.position.z), Quaternion.identity);
        yield return new WaitForSeconds (0.3f);
        babyNumber--;
        if (babyNumber > 0) {
            StartCoroutine (CreateBaby ());
        }
    }

    IEnumerator CountDown () {
        timeText.text = time.ToString ();
        yield return new WaitForSeconds (1);
        time--;
        if (time == 0) {
            GameOver ();
        } else {
            StartCoroutine (CountDown ());
        }
    }
    private void GameOver () {

    }
    public void GetScore () {
        scoreText.rectTransform.localScale = textScale;
        scoreText.rectTransform.DOScale (new Vector3 (1, 1, 1), 0.5f).SetEase (textEase);
        score++;
        scoreText.text = score.ToString ();
        BedRandomPosition ();
    }
    private void BedRandomPosition () {
        float x = Random.Range (randomBedCenter.position.x - randomPositionRange, randomBedCenter.position.x + randomPositionRange);
        float y = Random.Range (randomBedCenter.position.y - randomPositionRange, randomBedCenter.position.y + randomPositionRange);
        bed.position = new Vector3 (x, y, randomBedCenter.position.z);
    }

    public void audioPlay()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
    }
}