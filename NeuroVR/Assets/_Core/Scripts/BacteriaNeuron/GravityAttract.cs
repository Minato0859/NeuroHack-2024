using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public GameObject SourceMass;
    public GameObject TestMass;
    public float speed;
    public bool collided = false;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided)
        {
            print("moving");
            TestMass.transform.position = Vector3.MoveTowards(TestMass.transform.position, SourceMass.transform.position, speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "neuron")
        {
            print("collided");
            collided = true;
            rb.isKinematic = true;
        }
    }
}