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

        //Uses WASD keys to move farmer
        if (Input.GetKey("w")){
            transform.position += transform.up * (speed * Time.smoothDeltaTime);
        }
            
        if (Input.GetKey("s")){
            transform.position -= transform.up * (speed * Time.smoothDeltaTime);
        }
            
        if (Input.GetKey("a")){
            transform.Translate(Vector3.left * (speed * Time.smoothDeltaTime));
        }
            
        if (Input.GetKey("d")){
            transform.Translate(Vector3.right * (speed * Time.smoothDeltaTime)); 
        }      
    }
    public void MouseRotation(){
        
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position; 
    }
}
