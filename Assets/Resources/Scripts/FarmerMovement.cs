using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    
    public float speed = 10f;

    void Update(){
        Movement();
		    MouseRotation();

    }
    public void Movement(){

        //Movement using WASD keys
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveFarmer = new Vector3(x, y, 0f);
        transform.position += moveFarmer * (speed * Time.smoothDeltaTime);   
    }
    public void MouseRotation(){
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }
}
