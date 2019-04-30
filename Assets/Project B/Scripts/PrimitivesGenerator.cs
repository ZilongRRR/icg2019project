using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimitivesGenerator : MonoBehaviour
{
    [SerializeField] GameObject m_Baby;
    [SerializeField] GameObject m_Tower;
    // Start is called before the first frame update

    Vector2 m_Dimension = new Vector2(50, 50);

    void GeneratePrimitives(GameObject primitive,int count)
    {
        for (int i = 0; i < count; i++)
        {
            var primitiveIns = GameObject.Instantiate(primitive);
            primitiveIns.transform.position = new Vector3(
                m_Tower.transform.position.x + Random.Range(-m_Dimension.x, m_Dimension.x),
                10f,
                m_Tower.transform.position.z + Random.Range(-m_Dimension.y, m_Dimension.y)
                );
        }
    }

    void Start()
    {
        GeneratePrimitives(m_Baby, Random.Range(5, 10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}