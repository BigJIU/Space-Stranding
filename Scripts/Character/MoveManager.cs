using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    private Transform trans;
    private Rigidbody2D rigi;

    public float m_moveSpeed;
    public float m_rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        trans = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("a"))
        {
            //transform.Rotate(Vector3.up * m_rotateSpeed * Time.deltaTime, Space.Self);
            trans.Rotate(axis:Vector3.up, 0.1f);
        }
        if (Input.GetKey("d"))
        {
            trans.Rotate(axis:Vector3.down, 0.1f);
        }
        if (Input.GetKey("w"))
        {
            transform.Translate(trans.up * m_moveSpeed * Time.deltaTime);
            //rigi.AddForce(trans.forward*m_moveSpeed);

        }
        if (Input.GetKey("s"))
        {
            transform.Translate(trans.up * (-m_moveSpeed) * Time.deltaTime);
            //rigi.AddForce(trans.forward*(-m_moveSpeed));
        }
    }
    
}
