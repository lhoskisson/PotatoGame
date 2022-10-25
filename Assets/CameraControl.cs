using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject farmer;
    Vector3 farmerPosition;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        mainCamera();
    }
    public void mainCamera(){
        farmerPosition.Set(farmer.transform.position.x, farmer.transform.position.y, -10);
        transform.position = farmerPosition;
    }
}
