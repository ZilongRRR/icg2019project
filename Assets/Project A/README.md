# ICG2019 ProjectA

## Team members

r07521606 劉鎧禎
r06521605 許舜翔
r06521609 趙君傑

### Car

Additional works: 

- the light and the sound of direction indicators:    
  KeyCode: A, D    
  Description:     
  When the wheel angle reaches 40f, the light will be automatically turned off.

```
if (m_WheelFrontAngle >= 40f)
{
    leftLight.color = Color.white;
    turningLeft = false;
    audioData[2].Stop();
    audioData[3].Play();
}
```

- manual transmission and engine sound     
  KeyCode: W, S     
  Description:     
  Keycode W means to upshift, and Keycode S means to downshift.     
  There are six grades for this car. Every grade represents different maximum volume of engine sound and different maximum velocity which increases/decreases 4f as you upshift/downshift.

```
if (Input.GetKeyDown (KeyCode.W))
{
    Debug.Log (maxVelocity.ToString ());
    if (gradeNum < imagesGrade.Length - 1) {
        imagesGrade[gradeNum].color = Color.white;
        imagesGrade[++gradeNum].color = Color.red;
        maxVelocity = gradeNum * 4f;
    }
    audioData[0].volume = maxVelocity / 24f * 1;
    audioData[0].Play ();
}
```

![](https://i.imgur.com/qrmaMiT.png)

- tachometer     
  The rect transform of the pin will change depending the velocity.

```
RectTransform pin;

if (Input.GetKey (KeyCode.UpArrow))
{
    m_Velocity = Mathf.Min (maxVelocity, m_Velocity + Time.deltaTime * acceleration);
    var rot = pin.transform.localRotation.eulerAngles;
    rot.Set (0f, 0f, pinStartAngle - 6.6f * m_Velocity);
    pin.transform.localRotation = Quaternion.Euler (rot);
}
```

![](https://i.imgur.com/767IX5i.png)

- wheel angle holding key     
  KeyCode: space     
  Description:     
  When the keycode space is hold, the wheel angle will be unchangeable.

### Task

![](https://i.imgur.com/mvaAdvm.jpg)

According to the driving test in Taiwan, we design four tasks in the scene which are back up, street parking, s-shape and traffic light. Each of them consists of two trigger components and separately detect if the car is on line and check if he/she completed the task. We believe that the whole parts of car must cross the trigger line to finish the task, so we just simply count the total numbers passing the line and check if he/she completed depending on the maximum count. Once the car is on line, it will trigger the alarm and deduct points. Besides, it will also make game fail if any task is not achieved.

```
// our car consists of 7 gameobjects
if (count == 0 && triggerRecord.Count == 7 && !isCheck) {
            // set true to avoid duplicate work
            isCheck = true;
            // change the color of the check sign
            checkMark.color = Color.green;
        }
```

- #### Back Up
  ![](https://i.imgur.com/woxMgd4.png)

![](https://i.imgur.com/g0Uqj9t.png)

- #### Street Parking
  ![](https://i.imgur.com/PqpDYSx.png)

![](https://i.imgur.com/BEghoZ2.png)

- #### S-shape
  ![](https://i.imgur.com/N1hLqFq.png)

![](https://i.imgur.com/gy1XO3l.png)

Due to the complex structure of it, we setup multiple check points during the task.

- #### Traffic light
  ![](https://i.imgur.com/uSOmCW7.gif)

We use text mesh pro to display count down and cotrol the text with
`InvokeRepeating("lightCounting", 1, 1);`

```
void lightCounting()
{
    switch (m_status)
    {
        case 0:
            // Debug.Log("green " + m_greenTime);
            if (m_greenTime == greenTime) { updateColor(); }
            greenNum.text = "" + m_greenTime;
            m_greenTime -= 1;
            if (m_greenTime == -1)
            {
                m_status = (m_status + 1) % 3;
                m_greenTime = greenTime;
                greenNum.text = "";
                m_yellowTime = yellowTime;
            }
            break;
        case 1:
            // Debug.Log("yellow " + m_yellowTime);
            if (m_yellowTime == yellowTime) { updateColor(); }
            m_yellowTime -= 1;
            if (m_yellowTime == -1)
            {
                m_status = (m_status + 1) % 3;
                m_yellowTime = yellowTime;
                m_redTime = redTime;
            }
            break;
        case 2:
            // Debug.Log("red " + m_redTime);
            if (m_redTime == redTime) { updateColor(); }
            redNum.text = "" + m_redTime;
            m_redTime -= 1;
            if (m_redTime == -1)
            {
                m_status = (m_status + 1) % 3;
                m_redTime = redTime;
                redNum.text = "";
                m_greenTime = greenTime;
            }
            break;
    }
}
```

### Map

![](https://i.imgur.com/EzZbz9U.png)

When the car arrives the end area. You can choose to replay this game.

```
public class EndGameA : MonoBehaviour {

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
```

### Additional UI

![](https://i.imgur.com/vqaDZNP.jpg)

A. Score : If the car collide the edge, the score would decrease. We use a singleton script named "GradeManager" to calculate and show the score. Once the score is lower than 70 points, the score text's color would change to red, it means that this time you fail the challenge.

```
public class GradeManager : Singleton<GradeManager> {
    public void LoseScore (int score) {
            scoreText.rectTransform.localScale = textScale;
            scoreText.rectTransform.DOScale (new Vector3 (1, 1, 1), 0.5f).SetEase (textEase);
            originScore -= score;
            if (originScore < 70)
                scoreText.color = Color.red;
            scoreText.text = "" + originScore;
        }
}
```

B. Mini map : help the player to know the entire level

### Work Division

The behavior of the car entity: 劉鎧禎
The Task and the collision check: 許舜翔
The Map and the game UI: 趙君傑
