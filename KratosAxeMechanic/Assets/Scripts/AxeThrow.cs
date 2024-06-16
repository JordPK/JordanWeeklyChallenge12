using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrow : MonoBehaviour
{
    [SerializeField] Transform hand;
    Rigidbody rb;
    spawnCubes spawner;
    

    [SerializeField] bool isHeld;
    [SerializeField] bool isReturning;

    [SerializeField] float throwingPower;
    [SerializeField] float throwRotationPower;
    [SerializeField] float returnPower;

    float repulsionForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawner = FindObjectOfType<spawnCubes>();

        //set default axe state
        Catch();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld && Input.GetMouseButtonDown(0))
        {
            Throw();
        }
        else if (!isHeld && Input.GetMouseButton(1))
        {
            isReturning = true;
        }
        else if (!isHeld && Input.GetMouseButtonUp(1))
        {
            isReturning = false;
        }
    }

    private void FixedUpdate()
    {
        if (isReturning)
        {
            Return();
        }
    }



    void Throw()
    {
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;

        rb.AddForce(-transform.right * throwingPower, ForceMode.Impulse);
        rb.AddTorque(transform.forward * throwRotationPower, ForceMode.Impulse);
        isHeld = false;
    }

    void Return()
    {
        if (Vector3.Distance(hand.position, transform.position) < 1)
        {
            Catch();
        }

        Vector3 directionToHand = hand.position - transform.position;
        rb.velocity = directionToHand.normalized * returnPower;
    }

  
    void Catch()
    {
        isReturning = false;
        isHeld = true;
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;
        transform.position = hand.position;
        transform.parent = hand;
        transform.rotation = hand.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("hit");
            spawner.ReplaceObject();
            Vector3 repulsionDirection = transform.position - collision.transform.position;
            repulsionDirection.Normalize(); // Normalize to get the direction

            rb.AddRelativeForce(repulsionDirection * repulsionForce, ForceMode.Impulse);
            Destroy(collision.gameObject);
            
        }
    }

}

