using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    
    public float speed = 10f;

    void Update(){

        Movement();

    }
    public void Movement(){
        if (Input.GetKey("w")){
            transform.position += transform.up * (speed * Time.smoothDeltaTime);
        }
            
        if (Input.GetKey("s")){
            transform.position -= transform.up * (speed * Time.smoothDeltaTime);

        }
            
        if (Input.GetKey("a")){
            transform.Translate(Vector3.left * (speed * Time.smoothDeltaTime));
            // Quaternion farmerRotation = Quaternion.LookRotation(Vector3.left);

        }
            
        if (Input.GetKey("d")){
            transform.Translate(Vector3.right * (speed * Time.smoothDeltaTime)); 
            // Quaternion farmerRotation = Quaternion.LookRotation(Vector3.right);

        }      
    }
}
