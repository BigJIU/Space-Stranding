using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    private Transform transAs;
    private Rigidbody2D rigi;

    public float m_moveMaxSpeed;
    public float m_moveAccSpeed;
    public float m_moveDelSpeed;
    public float m_rotateSpeed;
    public float m_rotateDelaySpeed;
    public float m_rotateMaxDegree;

    private float currentSpeed = 0;
    

    
    
    void Start()
    {
        rigi = this.GetComponent<Rigidbody2D>();
        transAs = transform.GetChild(0).GetComponent<Transform>();

    }

    void cameraController()
    {
        GameManager.Camera.transform.position = new Vector3(0,9,0)+transform.position;
        GameManager.Camera.transform.rotation = Quaternion.Euler(90 - 15*(1-(m_moveMaxSpeed-currentSpeed)/m_moveMaxSpeed),0,90);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(spaceForward()* Time.deltaTime * currentSpeed, Space.Self);
        //cameraController();
        if (Input.GetKey("r")) transform.position = new Vector3(0,0,-10);

        if (Input.GetKey("s"))
        {
            recoverSpeed(m_moveAccSpeed);
        }
        else
        {
            if (Input.GetKey("w")) yAxisMove(0);
            if (Input.GetKey("a")) yAxisMove(1);
            if (Input.GetKey("d")) yAxisMove(-1);
            
            if(!Input.GetKey("w")&!Input.GetKey("d")&!Input.GetKey("a"))
            {
                recoverRotate();
                recoverSpeed(m_moveDelSpeed);
            }  
        }
        
        // if (Input.GetKey("a"))
        // {
        //     //transform.Rotate(Vector3.up * m_rotateSpeed * Time.deltaTime, Space.Self);
        //     trans.Rotate(axis:Vector3.up, 0.1f);
        // }
        // if (Input.GetKey("d"))
        // {
        //     trans.Rotate(axis:Vector3.down, 0.1f);
        // }
        // if (Input.GetKey("w"))
        // {
        //     transform.Translate(trans.up * m_moveSpeed * Time.deltaTime);
        //     //rigi.AddForce(trans.forward*m_moveSpeed);
        //
        // }
        // if (Input.GetKey("s"))
        // {
        //     transform.Translate(trans.up * (-m_moveSpeed) * Time.deltaTime);
        //     //rigi.AddForce(trans.forward*(-m_moveSpeed));
        // }
    }
    private void yAxisMove(int dir){

        if (dir == 0)
        {
            if (currentSpeed < m_moveMaxSpeed)
            {
                currentSpeed += Time.deltaTime * m_moveAccSpeed;
            }
        }
        //forward at now direction
        if (dir == 1)//turn left 
        {
            if (yEulerAngel() > -m_rotateMaxDegree)
            {
                transAs.Rotate(Vector3.up, -m_rotateSpeed * Time.deltaTime);	
            }
        }
        else if(dir == -1)//turn right
        {
            if (yEulerAngel() < m_rotateMaxDegree)
            {
                transAs.Rotate(Vector3.up, m_rotateSpeed * Time.deltaTime);	
            }
        }

    }

    private void recoverRotate()
    {
        
        //rotation recover
        if (Math.Abs(yEulerAngel()) > m_rotateDelaySpeed * Time.deltaTime)
        {
            transAs.Rotate(Vector3.up, (yEulerAngel()>0?-1:1)* m_rotateDelaySpeed * Time.deltaTime);
        }
        else if(Math.Abs(yEulerAngel()) > 1)
        {
            transAs.localRotation = new Quaternion(0,0,0,0);
        }

    }

    private void recoverSpeed(float delay)
    {
        if (currentSpeed > 0)
        {
            currentSpeed -= delay * Time.deltaTime;
        }
        else
        {
            currentSpeed = 0;
        }
    }

    private Vector3 spaceForward()
    {
        // Transform(transAs.forward)
        // Vector3 tmp = .
        // Vector3 offset = transAs.localPosition * transform.localScale;//???????????????????????????  
        // Vector3 result = parent.position + parent.rotation * offset;//???????????????????????????????????????????????????????????????????????????????????????????????????  
        return transAs.forward;
    }

    private float yEulerAngel()
    {
        return transAs.localRotation.eulerAngles.y > 180
            ? transAs.localRotation.eulerAngles.y - 360
            : transAs.localRotation.eulerAngles.y;
    }
    
    
}
