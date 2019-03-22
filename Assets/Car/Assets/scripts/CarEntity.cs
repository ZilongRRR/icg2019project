using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntity : MonoBehaviour {
    [SerializeField] SpriteRenderer[] m_Renderers = new SpriteRenderer[7]; 
    public GameObject wheelFrontRight;
    public GameObject wheelFrontLeft;
    public GameObject wheelBackRight;
    public GameObject wheelBackLeft;
    public SpriteRenderer leftLight;
    public SpriteRenderer rightLight;
    public TracingCameraEntity m_Camera;

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

    /*
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Velocity = Mathf.Min(maxVelocity, m_Velocity + Time.deltaTime * acceleration);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Velocity = Mathf.Max(0, m_Velocity - Time.deltaTime * acceleration);
        }
        if (Input.GetKey(KeyCode.R))
        {
            m_Velocity = Mathf.Max(-maxVelocity, m_Velocity - Time.deltaTime * acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_WheelFrontAngle = Mathf.Clamp(m_WheelFrontAngle + Time.deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_WheelFrontAngle = Mathf.Clamp(m_WheelFrontAngle - Time.deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
            UpdateWheels();
        }

        m_DeltaMovement = m_Velocity * Time.deltaTime;

        this.transform.Rotate(0f, 0f, 1 / carLength * Mathf.Tan(Mathf.Deg2Rad * m_WheelFrontAngle) * m_DeltaMovement * Mathf.Rad2Deg);
        this.transform.Translate(Vector3.up * m_DeltaMovement);
    }*/
}
