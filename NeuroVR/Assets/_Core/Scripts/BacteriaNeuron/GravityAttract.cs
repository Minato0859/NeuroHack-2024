using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject SourceMass;
    public GameObject TestMass;
    public float speed;

    private bool collided = false;
    private Rigidbody rb;
    private Vector3 initialOffset;
    private bool SourceMassAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SourceMassAlive)
        {
            if (!collided)
            {
                TestMass.transform.position = Vector3.MoveTowards(TestMass.transform.position, SourceMass.transform.position, speed);
            }
            else
            {
                if (SourceMass != null)
                {
                    TestMass.transform.position = SourceMass.transform.position + initialOffset;
                }
                else
                {
                    // If the source mass is destroyed, apply gravity to the test mass
                    rb.useGravity = true;
                    rb.isKinematic = false;
                    SourceMassAlive = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "neuron")
        {
            collided = true;
            rb.isKinematic = true;
            initialOffset = TestMass.transform.position - SourceMass.transform.position;
        }
    }
}
