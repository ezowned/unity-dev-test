using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;

    public float distanceFromTarget=5f;
    public float rotationSpeed = 4f;


    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            var h = Input.GetAxis("Horizontal") * Time.deltaTime *1.5f * rotationSpeed;
            var v = Input.GetAxis("Vertical") * Time.deltaTime * rotationSpeed;
            var eulers = transform.eulerAngles;
            eulers.y += h;
            eulers.x -= v;
            transform.eulerAngles = eulers;
        }
        if (target == null)
        {
            return;
        }
            
        transform.position = target.position - transform.forward * distanceFromTarget;


    }

}
