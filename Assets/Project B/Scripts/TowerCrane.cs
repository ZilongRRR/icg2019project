using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCrane : MonoBehaviour
{
    [SerializeField] GameObject m_Jib;
    [SerializeField] GameObject m_Trolley;
    [SerializeField] GameObject m_Cable;

    const float MOVE_SPEED = 8f;
    float LONG_LLIMIT = 0f;
    const float LONG_HLIMIT = -17f;

    LineRenderer lineRenderer;
    GameObject connectObject;

    List<GameObject> gameObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        m_Cable.SetActive(true);
        connectObject = m_Trolley.GetComponent<ConfigurableJoint>().connectedBody.GetComponent<ConfigurableJoint>().connectedBody.gameObject;

        LONG_LLIMIT = m_Trolley.transform.localPosition.y;
        lineRenderer = m_Cable.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, m_Trolley.transform.position);
        lineRenderer.SetPosition(1, connectObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, m_Trolley.transform.position);
        lineRenderer.SetPosition(1, connectObject.transform.position);
        
        #region Key Control
        if (Input.GetKey(KeyCode.A))
        {
            var nowRotate = m_Jib.transform.localRotation.eulerAngles;
            nowRotate.z -= MOVE_SPEED * Time.deltaTime;
            m_Jib.transform.localRotation = Quaternion.Euler(nowRotate);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            var nowRotate = m_Jib.transform.localRotation.eulerAngles;
            nowRotate.z += MOVE_SPEED * Time.deltaTime;
            m_Jib.transform.localRotation = Quaternion.Euler(nowRotate);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            var nowPos = m_Trolley.transform.localPosition;
            m_Trolley.transform.localPosition = new Vector3(nowPos.x, Mathf.Max(nowPos.y -= MOVE_SPEED / 2 * Time.deltaTime, LONG_HLIMIT), nowPos.z);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var nowPos = m_Trolley.transform.localPosition;
            m_Trolley.transform.localPosition = new Vector3(nowPos.x, Mathf.Min(nowPos.y += MOVE_SPEED / 2 * Time.deltaTime, LONG_LLIMIT), nowPos.z);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            var limit = m_Trolley.GetComponent<ConfigurableJoint>().connectedBody.gameObject.GetComponent<ConfigurableJoint>().linearLimit;
            if (limit.limit > 3)
            {
                limit.limit -= MOVE_SPEED * Time.deltaTime;
                m_Trolley.GetComponent<ConfigurableJoint>().connectedBody.gameObject.GetComponent<ConfigurableJoint>().linearLimit = limit;
            }
        }
        else if (Input.GetKey(KeyCode.E))
        {
            var limit = m_Trolley.GetComponent<ConfigurableJoint>().connectedBody.gameObject.GetComponent<ConfigurableJoint>().linearLimit;
            if (limit.limit < 100)
            {
                limit.limit += MOVE_SPEED * Time.deltaTime;
                m_Trolley.GetComponent<ConfigurableJoint>().connectedBody.gameObject.GetComponent<ConfigurableJoint>().linearLimit = limit;
            }
        }
        #endregion
    }
}
