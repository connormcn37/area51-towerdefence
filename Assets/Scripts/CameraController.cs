using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Horizontal")){
            transform.Translate(Vector3.forward * Input.GetAxis("Horizontal") * panSpeed * Time.deltaTime, Space.World);
        }
        if(Input.GetButton("Vertical")){
            transform.Translate(Vector3.left * Input.GetAxis("Vertical") * panSpeed * Time.deltaTime, Space.World);
        }
    }
}
