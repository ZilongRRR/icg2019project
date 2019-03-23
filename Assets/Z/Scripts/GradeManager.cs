using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
namespace ZTools {
    public class GradeManager : Singleton<GradeManager> {
        [Space]
        public TextMeshProUGUI scoreText;
    
        public int originScore = 100;
        public Vector3 textScale;
        public Ease textEase;
        [Header("人設定")]
        public int nowPeopleNumber;
        public GameObject people;
        public Sprite[] peopleSprite;

        public void LoseScore(int score){
            scoreText.rectTransform.localScale=textScale;
            scoreText.rectTransform.DOScale(new Vector3(1,1,1),0.5f).SetEase(textEase);
            originScore -= score;
            if(originScore<70)
                scoreText.color=Color.red;
            scoreText.text = "" + originScore;
        }
        public void LosePeople(){
            nowPeopleNumber--;
            if(nowPeopleNumber < 5){
                AddPeople(5);
            }
        }

        public void AddPeople(int number){
            for (int i = 0; i < number; i++)
            {
                Vector3 position=new Vector3(Random.Range(-13, 13), Random.Range(-20, 20), 0);
                IdleMan idleMan = Instantiate(people,position,Quaternion.identity).GetComponent<IdleMan>();
                int randomi=Random.Range(0, 10);
                idleMan.SetSprite(0, peopleSprite[randomi * 2]);
                idleMan.SetSprite(1, peopleSprite[randomi * 2 + 1]);
                nowPeopleNumber++;
            }
        }

        void Start()
        {
            AddPeople(10);     
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.X)){
                LoseScore(10);
            }
        }
    }
}