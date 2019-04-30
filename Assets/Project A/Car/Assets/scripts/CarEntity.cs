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
    bool hold = false;
    public Canvas grade;
    private Image[] imagesGrade;
    int gradeNum = 0;
    public Canvas tachometer;
    private RectTransform[] transTachometer;
    RectTransform pin;
    float pinStartAngle;

    float m_WheelFrontAngle = 0;
    float WHEEL_ANGLE_LIMIT = 60f;
    public float turnAngularVelocity = 30f;

    float m_Velocity;
    public float Velocity { get { return m_Velocity; } }
    public float acceleration = 2f;
    public float maxVelocity = 4f;
    public float mfs = 2f;

    private float m_DeltaMovement;
    float carLength = 1.6f;
    public float getCarLength { get { return carLength; } }

    public void UpdateWheels () {
        Vector3 localEularAngles = new Vector3 (0f, 0f, m_WheelFrontAngle);
        wheelFrontLeft.transform.localEulerAngles = localEularAngles;
        wheelFrontRight.transform.localEulerAngles = localEularAngles;
    }

    void ResetColor () {
        ChangeColor (Color.white);
    }

    void ChangeColor (Color color) {
        foreach (SpriteRenderer r in m_Renderers)
            r.color = color;
    }

    private void OnCollisionEnter2D (Collision2D collision) {
        Stop ();
        ChangeColor (Color.red);
    }

    private void OnCollisionStay2D (Collision2D collision) {
        Stop ();
    }

    private void OnCollisionExit2D (Collision2D collision) {
        ResetColor ();
    }

    public void Stop () {
        m_Velocity = 0;
        for (int i = 0; i < audioData.Length; i++) {
            audioData[i].Pause ();
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        CheckPoint checkPoint = other.gameObject.GetComponent<CheckPoint> ();
        if (checkPoint != null) {
            ChangeColor (Color.green);
            this.Invoke ("ResetColor", 0.1f);
        }
    }

    // Use this for initialization
    void Start () {
        imagesGrade = grade.GetComponentsInChildren<Image> ();
        Debug.Log (imagesGrade.Length.ToString ());
        imagesGrade[++gradeNum].color = Color.red;

        transTachometer = tachometer.GetComponentsInChildren<RectTransform> ();
        pin = transTachometer[2];
        pinStartAngle = 150;

        audioData = GetComponents<AudioSource> ();
    }

    // Update is called once per frame
    void Update () {
        if (m_Velocity > 0) { m_Velocity = Mathf.Max (0, m_Velocity - mfs); } else if (m_Velocity < 0) { m_Velocity = Mathf.Min (0, m_Velocity + mfs); }
        if (Input.GetKeyDown (KeyCode.UpArrow)) {
            audioData[0].volume = maxVelocity / 24f * 1;
            audioData[0].Play ();
        }
        if (Input.GetKey (KeyCode.UpArrow)) {
            m_Velocity = Mathf.Min (maxVelocity, m_Velocity + Time.deltaTime * acceleration);
            var rot = pin.transform.localRotation.eulerAngles;
            rot.Set (0f, 0f, pinStartAngle - 6.6f * m_Velocity);
            pin.transform.localRotation = Quaternion.Euler (rot);
        }
        if (Input.GetKeyUp (KeyCode.UpArrow))
            audioData[0].Stop ();

        if (Input.GetKey (KeyCode.DownArrow)) {
            audioData[0].Stop ();
            if (m_Velocity > 0) {
                m_Velocity = Mathf.Max (0, m_Velocity - Time.deltaTime * acceleration);
                var rot = pin.transform.localRotation.eulerAngles;
                rot.Set (0f, 0f, pinStartAngle - 6.6f * m_Velocity);
                pin.transform.localRotation = Quaternion.Euler (rot);
            } else
                m_Velocity = Mathf.Min (0, m_Velocity + Time.deltaTime * acceleration);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            audioData[0].Stop();
            audioData[1].Play();
        }

        if (Input.GetKey (KeyCode.R)) {
            m_Velocity = Mathf.Max (-maxVelocity, m_Velocity - Time.deltaTime * acceleration / 1.5f);
            imagesGrade[gradeNum].color = Color.white;
            gradeNum = 0;
            imagesGrade[gradeNum].color = Color.red;

            if (m_Velocity > 0) {
                var rot = pin.transform.localRotation.eulerAngles;
                rot.Set (0f, 0f, pinStartAngle - 6.6f * m_Velocity);
                pin.transform.localRotation = Quaternion.Euler (rot);
            }
        }
        if (Input.GetKeyUp (KeyCode.R)) {
            if (m_Velocity > 0) {
                gradeNum = (int) (m_Velocity / 10) + 1;
                imagesGrade[gradeNum].color = Color.red;

                var rot = pin.transform.localRotation.eulerAngles;
                rot.Set (0f, 0f, pinStartAngle - 6.6f * m_Velocity);
                pin.transform.localRotation = Quaternion.Euler (rot);
            } else {
                gradeNum = 1;
                imagesGrade[gradeNum].color = Color.red;
            }
            imagesGrade[0].color = Color.white;

            audioData[1].Stop();
        }

        if (!hold)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_WheelFrontAngle = Mathf.Clamp(m_WheelFrontAngle + Time.deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
                UpdateWheels();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                m_WheelFrontAngle = 0;
                UpdateWheels();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_WheelFrontAngle = Mathf.Clamp(m_WheelFrontAngle - Time.deltaTime * turnAngularVelocity, -WHEEL_ANGLE_LIMIT, WHEEL_ANGLE_LIMIT);
                UpdateWheels();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                m_WheelFrontAngle = 0;
                UpdateWheels();
            }
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            hold = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_WheelFrontAngle = 0;
            UpdateWheels();
            hold = false;
        }

        //方向燈
        if (Input.GetKeyDown (KeyCode.A)) {
            if (turningLeft == false)
            {
                turningLeft = true;
                audioData[2].Play();
            }
            else
            {
                leftLight.color = Color.white;
                turningLeft = false;
                audioData[2].Stop();
                audioData[3].Play();
            }
        }

        if (turningLeft) {
            flash += Time.deltaTime;
            if (flash % 1 > 0.5f) {
                leftLight.color = Color.yellow;
            } else {
                leftLight.color = Color.white;
            }

            if (m_WheelFrontAngle >= 40f)
            {
                leftLight.color = Color.white;
                turningLeft = false;
                audioData[2].Stop();
                audioData[3].Play();
            }
        }

        if (Input.GetKeyDown (KeyCode.D)) {
            if (turningRight == false)
            {
                turningRight = true;
                audioData[2].Play();
            }
            else
            {
                rightLight.color = Color.white;
                turningRight = false;
                audioData[2].Stop();
                audioData[3].Play();
            }
        }

        if (turningRight) {
            flash += Time.deltaTime;
            if (flash % 1 > 0.5f) {
                rightLight.color = Color.yellow;
            } else {
                rightLight.color = Color.white;
            }

            if (m_WheelFrontAngle <= -40f)
            {
                rightLight.color = Color.white;
                turningRight = false;
                audioData[2].Stop();
                audioData[3].Play();
            }
        }

        //打檔
        if (Input.GetKeyDown (KeyCode.W)) {
            Debug.Log (maxVelocity.ToString ());
            if (gradeNum < imagesGrade.Length - 1) {
                imagesGrade[gradeNum].color = Color.white;
                imagesGrade[++gradeNum].color = Color.red;
                maxVelocity = gradeNum * 4f;
            }
            audioData[0].volume = maxVelocity / 24f * 1;
            audioData[0].Play ();
        }
        if (Input.GetKeyDown (KeyCode.S)) {
            Debug.Log (maxVelocity.ToString ());
            if (gradeNum > 1) {
                imagesGrade[gradeNum].color = Color.white;
                imagesGrade[--gradeNum].color = Color.red;
                maxVelocity = gradeNum * 4f;
            }
            audioData[0].volume = maxVelocity / 24f * 1;
            audioData[0].Play ();
        }

        m_DeltaMovement = m_Velocity * Time.deltaTime;

        this.transform.Rotate (0f, 0f, 1 / carLength * Mathf.Tan (Mathf.Deg2Rad * m_WheelFrontAngle) * m_DeltaMovement * Mathf.Rad2Deg);
        this.transform.Translate (Vector3.up * m_DeltaMovement);
    }

}