using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGame : MonoBehaviour {
    public CarEntity car1;
    public CarEntity car2;
    public CarEntity car3;
    public List<CarEntity> cars;
    int index = 0;
    CarEntity nowSelect;
    CarEntity lastSelect;
    private float flash;
    bool turningLeft = false;
    bool turningRight = false;
    public Canvas canvas;
    private Image[] images;
    int gradeNum = 0;

    // Use this for initialization
    void Start () {
        cars.Add(car1);
        cars.Add(car2);
        cars.Add(car3);

        nowSelect = cars[index];
        images = canvas.GetComponentsInChildren<Image>();
        Debug.Log(images.Length.ToString());
        images[++gradeNum].color = Color.white;
    }
	
	// Update is called once per frame
    /*
	void FixedUpdate () {    
        if (Input.GetKeyDown(KeyCode.Q))
        {
            lastSelect = cars[index];
            index = index + 1 > cars.Count - 1 ? 0 : index + 1;
            nowSelect = cars[index];
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastSelect = cars[index];
            index = index - 1 < 0 ? cars.Count - 1 : index - 1;
            nowSelect = cars[index];
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            nowSelect.Velocity = Mathf.Min(nowSelect.maxVelocity, nowSelect.Velocity + Time.fixedDeltaTime * nowSelect.acceleration);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            nowSelect.Velocity = Mathf.Max(0, nowSelect.Velocity - Time.fixedDeltaTime * nowSelect.acceleration);
        }
        if (Input.GetKey(KeyCode.R))
        {
            nowSelect.Velocity = Mathf.Max(-nowSelect.maxVelocity, nowSelect.Velocity - Time.fixedDeltaTime * nowSelect.acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nowSelect.WheelFrontAngle = Mathf.Clamp(nowSelect.WheelFrontAngle + Time.fixedDeltaTime * nowSelect.turnAngularVelocity, -nowSelect.ANGLE_LIMIT, nowSelect.ANGLE_LIMIT);
            nowSelect.UpdateWheels();
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            nowSelect.WheelFrontAngle = 0;
            nowSelect.UpdateWheels();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nowSelect.WheelFrontAngle = Mathf.Clamp(nowSelect.WheelFrontAngle - Time.fixedDeltaTime * nowSelect.turnAngularVelocity, -nowSelect.ANGLE_LIMIT, nowSelect.ANGLE_LIMIT);
            nowSelect.UpdateWheels();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            nowSelect.WheelFrontAngle = 0;
            nowSelect.UpdateWheels();
        }
        
        //方向燈
        if (Input.GetKeyDown(KeyCode.Z))
        {
            turningLeft = true;
        }
        
        if (turningLeft)
        {
            flash += Time.deltaTime;
            if (flash % 1 > 0.5f)
            {
                nowSelect.leftLight.color = Color.red;
            }
            else
            {
                nowSelect.leftLight.color = Color.white;
            }
        }

        if(nowSelect.WheelFrontAngle >= 20f)
        {
            nowSelect.leftLight.color = Color.white;
            turningLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            turningRight = true;
        }

        if (turningRight)
        {
            flash += Time.deltaTime;
            if (flash % 1 > 0.5f)
            {
                nowSelect.rightLight.color = Color.red;
            }
            else
            {
                nowSelect.rightLight.color = Color.white;
            }
        }

        if (nowSelect.WheelFrontAngle <= -20f)
        {
            nowSelect.rightLight.color = Color.white;
            turningRight = false;
        }

        //打檔
        if(Input.GetKeyDown(KeyCode.D))
        {
            nowSelect.maxVelocity += 10f;
            if (nowSelect.maxVelocity >= 60f)
                nowSelect.maxVelocity = 60f;
            Debug.Log(nowSelect.maxVelocity.ToString());
            if (gradeNum < images.Length - 1)
            {
                images[gradeNum].color = Color.white;
                images[++gradeNum].color = Color.red;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            nowSelect.maxVelocity -= 10f;
            if (nowSelect.maxVelocity <= 0f)
                nowSelect.maxVelocity = 0f;
            Debug.Log(nowSelect.maxVelocity.ToString());
            if (gradeNum > 0)
            {
                images[gradeNum].color = Color.white;
                images[--gradeNum].color = Color.red;
            }
        }

        nowSelect.m_DeltaMovement = nowSelect.Velocity * Time.deltaTime;

        nowSelect.transform.Rotate(0f, 0f, 1 / nowSelect.getCarLength * Mathf.Tan(Mathf.Deg2Rad * nowSelect.WheelFrontAngle) * nowSelect.m_DeltaMovement * Mathf.Rad2Deg);
        nowSelect.transform.Translate(Vector3.up * nowSelect.m_DeltaMovement);

        nowSelect.m_Camera.targetObject = nowSelect;
    }*/
}
