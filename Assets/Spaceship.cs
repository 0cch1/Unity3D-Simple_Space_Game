using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    private Rigidbody rb;
    public float accSpeed = 100f; // Acceleration speed for forward movement
    public float rotSpeed = 50f;  // Roration speed for turning
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWrap();
        // Rotate spaceship left/right using left/right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0){
            rb.AddTorque(Vector3.up * horizontalInput * rotSpeed * Time.deltaTime);
        }
        // Accelerate spaceship forward using Up arrow key
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Apply force only in the XZ plane, ensuring Y remains zero
            rb.AddForce(transform.forward * accSpeed * Time.deltaTime);
        }
       
    }

    void CheckWrap()
    {
        Camera cam = Camera.main; 

        Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);

        //Wrap horizontally
        if (viewportPosition.x > 1) viewportPosition.x = 0;
        else if (viewportPosition.x < 0) viewportPosition.x = 1;

        // Wrap vertically
        if (viewportPosition.y > 1) viewportPosition.y = 0;
        else if (viewportPosition.y < 0) viewportPosition.y = 1;

        Vector3 newWorldPosition = cam.ViewportToWorldPoint(viewportPosition);
        transform.position = new Vector3(newWorldPosition.x, transform.position.y, newWorldPosition.z);
    }
}
