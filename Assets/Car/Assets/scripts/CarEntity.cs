using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarEntity : MonoBehaviour {
    [SerializeField] SpriteRenderer[] m_Renderers = new SpriteRenderer[7]; 
    public GameObject wheelFrontRight;
    public GameObject wheelFrontLeft;
    public GameObject wheelBackRight;
    public GameObject wheelBackLeft;
    public SpriteRenderer leftLight;
    public SpriteRenderer rightLight;
    public TracingCameraEntity m_Camera;
    AudioSource[] audioData;

    private float flash;
    bool turningLeft = false;
    bool turningRight = false;
    public Canvas grade;
    private Image[] imagesGrade;
    int gradeNum = 0;
    public Canvas tachometer;
    private RectTransform[] transTachometer;
    RectTransform pin;
    float pinStartAngle;

    float m_WheelFrontAngle = 0;
    const float WHEEL_ANGLE_LIMIT = 40f;
    public float turnAngularVelocity = 20f;

    float m_Velocity;
    public float Velocity { get { return m_Velocity; } set { m_Velocity = value; } }
    public float WheelFrontAngle { get { return m_WheelFrontAngle; } set { m_WheelFrontAngle = value; } }
    public float ANGLE_LIMIT { get { return WHEEL_ANGLE_LIMIT; } }
    public float acceleration = 3f;
    public float deceleration = 10f;
    public float maxVelocity = 10f;

    public float m_DeltaMovement;
    float carLength = 1.6f;
    public float getCarLength { get { return carLength; } }

    public void UpdateWheels()
    {
        Vector3 localEularAngles = new Vector3(0f, 0f, m_WheelFrontAngle);
        wheelFrontLeft.transform.localEulerAngles = localEularAngles;
        wheelFrontRight.transform.localEulerAngles = localEularAngles;
    }

    void ResetColor()
    {
        ChangeColor(Color.white);
    }

    void ChangeColor(Color color)
    {
        foreach (SpriteRenderer r in m_Renderers)
            r.color = color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Stop();
        ChangeColor(Color.red);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Stop();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ResetColor();
    }

    void Stop()
    {
        m_Velocity = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint>();
        if(checkPoint!=null)
        {
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 0.1f);
        }
    }
    
    // Use this for initialization
    void Start () {
        imagesGrade = grade.GetComponentsInChildren<Image>();
        Debug.Log(imagesGrade.Length.ToString());
        imagesGrade[++gradeNum].color = Color.red;

        transTachometer = tachometer.GetComponentsInChildren<RectTransform>();
        pin = transTachometer[2];
        pinStartAngle = 150;

        audioData = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            audioData[0].Play();
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Velocity = Mathf.Min(maxVelocity, m_Velocity + Time.deltaTime * acceleration);
            var rot = pin.transform.localRotation.eulerAngles;
            rot.Set(0f, 0f, pinStartAngle - 2.2f * m_Velocity);
            pin.transform.localRotation = Quaternion.Euler(rot);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
            audioData[0].Stop();

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Velocity = Mathf.Max(0, m_Velocity - Time.deltaTime * acceleration);
            var rot = pin.transform.localRotation.eulerAngles;
            rot.Set(0f, 0f, pinStartAngle - 2.2f * m_Velocity);
            pin.transform.localRotation = Quaternion.Euler(rot);
        }
        if (Input.GetKey(KeyCode.R))
        {
            m_Velocity = Mathf.Max(-maxVelocity, m_Velocity - Time.deltaTime * acceleration);
            imagesGrade[gradeNum].color = Color.white;
            gradeNum = 0;
            imagesGrade[gradeNum].color = Color.red;

            if (m_Velocity > 0)
            {
                var rot = pin.transform.localRotation.eulerAngles;
                rot.Set(0f, 0f, pinStartAngle - 2.2f * m_Velocity);
                pin.transform.localRotation = Quaternion.Euler(rot);
            }
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            if(m_Velocity > 0)
            {
                gradeNum = (int)(m_Velocity / 10) + 1;
                imagesGrade[gradeNum].color = Color.red;

                var rot = pin.transform.localRotation.eulerAngles;
                rot.Set(0f, 0f, pinStartAngle - 2.2f * m_Velocity);
                pin.transform.localRotation = Quaternion.Euler(rot);
            }
            else
            {
                gradeNum = 1;
                imagesGrade[gradeNum].color = Color.red;
            }
            imagesGrade[0].color = Color.white;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_WheelFrontAngle = Mathf.Clamp(m_WheelFrontAngle + Time.deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            WheelFrontAngle = 0;
            UpdateWheels();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_WheelFrontAngle = Mathf.Clamp(m_WheelFrontAngle - Time.deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            WheelFrontAngle = 0;
            UpdateWheels();
        }

        //方向燈
        if (Input.GetKeyDown(KeyCode.A))
        {
            turningLeft = true;
        }

        if (turningLeft)
        {
            flash += Time.deltaTime;
            if (flash % 1 > 0.5f)
            {
                leftLight.color = Color.yellow;
            }
            else
            {
                leftLight.color = Color.white;
            }
        }

        if (WheelFrontAngle >= 20f)
        {
            leftLight.color = Color.white;
            turningLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            turningRight = true;
        }

        if (turningRight)
        {
            flash += Time.deltaTime;
            if (flash % 1 > 0.5f)
            {
                rightLight.color = Color.yellow;
            }
            else
            {
                rightLight.color = Color.white;
            }
        }

        if (WheelFrontAngle <= -20f)
        {
            rightLight.color = Color.white;
            turningRight = false;
        }

        //打檔
        if (Input.GetKeyDown(KeyCode.W))
        {
            audioData[0].Play();
            Debug.Log(maxVelocity.ToString());
            if (gradeNum < imagesGrade.Length - 1)
            {
                imagesGrade[gradeNum].color = Color.white;
                imagesGrade[++gradeNum].color = Color.red;
                maxVelocity = gradeNum * 10f;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            audioData[0].Play();
            Debug.Log(maxVelocity.ToString());
            if (gradeNum > 1)
            {
                imagesGrade[gradeNum].color = Color.white;
                imagesGrade[--gradeNum].color = Color.red;
                maxVelocity = gradeNum * 10f;
            }
        }

        m_DeltaMovement = m_Velocity * Time.deltaTime;

        this.transform.Rotate(0f, 0f, 1 / carLength * Mathf.Tan(Mathf.Deg2Rad * m_WheelFrontAngle) * m_DeltaMovement * Mathf.Rad2Deg);
        this.transform.Translate(Vector3.up * m_DeltaMovement);
    }
}
