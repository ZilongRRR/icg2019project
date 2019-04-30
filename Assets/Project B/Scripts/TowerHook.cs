using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHook : MonoBehaviour
{
    // Start is called before the first frame update
    const float MOVE_SPEED = 2f;
    const float ATTACH_DISTANCE = 3f;
    GameObject m_DetectedObject;
    void Start()
    {

    }
    [SerializeField] Joint m_JointForObject;
    // Update is called once per frame
    void Update()
    {
        #region Key Control
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Translate(0, MOVE_SPEED * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            this.transform.Translate(0, -MOVE_SPEED * Time.deltaTime, 0);
        }
        #endregion
        DetectObjects();

        if (m_JointForObject.connectedBody == null)
        {
            DetectObjects();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            AttachOrDetachObject();
        }
    }
    void AttachOrDetachObject()
    {
        if (m_JointForObject.connectedBody == null)
        {
            if (m_DetectedObject != null)
            {
                m_JointForObject.connectedBody = m_DetectedObject.GetComponent<Rigidbody>();

                m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.red;
                m_DetectedObject = null;
            }
        }
        else
        {
            m_JointForObject.connectedBody.GetComponent<MeshRenderer>().material.color = Color.white;
            m_JointForObject.connectedBody = null;
        }
    }
    void DetectObjects()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ATTACH_DISTANCE))
        {
            if (m_DetectedObject == hit.collider.gameObject)
            {
                return;
            }
            RecoverDetectedObject();
            MeshRenderer renderer = hit.collider.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow;

                m_DetectedObject = hit.collider.gameObject;
            }
            else
            {
                RecoverDetectedObject();
            }

        }
    }
    void RecoverDetectedObject()
    {
        if (m_DetectedObject != null)
        {
            m_DetectedObject.GetComponent<MeshRenderer>().material.color = Color.white;
            m_DetectedObject = null;
        }
    }
    [SerializeField] LineRenderer m_Cable;

    void UpdateCable()
    {
        m_Cable.enabled = m_JointForObject.connectedBody != null;
        if (m_Cable.enabled)
        {
            m_Cable.SetPosition(0, this.transform.position);

            var connectedBodyTransform = m_JointForObject.connectedBody.transform;
            m_Cable.SetPosition(1, connectedBodyTransform.TransformPoint(m_JointForObject.connectedAnchor));
        }
    }
}
