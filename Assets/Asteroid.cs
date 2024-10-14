using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject asteroidFragment;
    public int numFragment = 3;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        float randomSpeed = Random.Range(5f, 8f);
        rb.velocity = randomDirection* randomSpeed;

        // Invoke method to check for screen wrapping 5 times per second
        InvokeRepeating("CheckScreenWrap", 0f, 0.2f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckScreenWrap()
    {
        // Get the camera reference
        Camera cam = Camera.main;

        // Convert the object's world position to viewport position
        Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);

        // Store the original depth (distance from the camera) given that the games uses the XZ plane for movement
        float originalZ = viewportPosition.z;

        // wrap horizontally (x-axis)
        if(viewportPosition.x > 1)
        {
            viewportPosition.x = 0;
         
        } else if (viewportPosition.x < 0)
        {
            viewportPosition.x = 1;
           
        }

        // wrap vertically ( Z axis in world space corresponds to Y axis in viewport space)
        if (viewportPosition.y > 1)
        {
            viewportPosition.y = 0;
            
        }
        else if (viewportPosition.y < 0)
        {
            viewportPosition.y = 1;
            
        }
        // Restore the original depth
        viewportPosition.z = originalZ;

        // Convert back to world space
        Vector3 newWoldPosition = cam.ViewportToWorldPoint(viewportPosition);

        // Since we're moving in the XZ plane, we keep the Y position unchanged
        transform.position = new Vector3(newWoldPosition.x, transform.position.y, newWoldPosition.z);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Spawn fragments at the point of impact
        for(int i = 0; i < numFragment; i++)
        {
            // Get the collision point
            Vector3 spawnPosition = collision.contacts[0].point;

            // Instantiate a fragment at the point of impact with random rotation
            GameObject fragment = Instantiate(asteroidFragment, spawnPosition, Random.rotation);

            // Add a random small force to each fragment
            Rigidbody fragmentRb = fragment.GetComponent<Rigidbody>();
            if(fragmentRb != null)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                fragmentRb.AddForce(randomDirection*Random.Range(2f, 5f),ForceMode.Impulse);
            }
            // Destroy fragment after 2 seconds
            Destroy(fragment,2f);
        }
        // Destroy asteroid its self after collision
        Destroy(gameObject);
    }
}
