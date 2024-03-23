using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronHealth : MonoBehaviour
{
    public int Health;
    public int BacteriaDamage;
    public int CooldownTime;

    private int counter;
    private int numberOfBacterias = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //60fps therefore, this is one second * attack speed
        if(counter == (60*CooldownTime))
        {
            counter = 0;
            Health -= (BacteriaDamage * numberOfBacterias);
            //print(Health);
        }
        counter += 1;

        if (Health <= 0)
        {
            // Destroy the GameObject if health is zero or less
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bacteria")
        {
            //print("Collided with neuron");
            numberOfBacterias++; // Increment the collision counter
        }
    }
}
