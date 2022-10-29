using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    
    public float speed = 10f;

    void Update(){

        Movement();
<<<<<<< Updated upstream:Assets/FarmerMovement.cs
=======
		MouseRotation();
>>>>>>> Stashed changes:Assets/Resources/Scripts/FarmerMovement.cs

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
    public void MouseRotation(){
        
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }
}
