using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronHealth : MonoBehaviour
{
    public int Health = 100;
    public int BacteriaDamage = 1;
    public int CooldownTime = 10;

    public float shakeMagnitude = 0.01f; // Adjust this value to control the intensity of the shake
    public float shakeDuration = 0.1f; // Adjust this value to control the duration of the shake

    private int counter = 0;
    private int numberOfBacterias = 0;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
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
            if (Health <= 0)
            {
                // Destroy the GameObject if health is zero or less
                Destroy(gameObject);
            }
            else
            {
                // Trigger shake effect
                StartCoroutine(Shake());
            }
        }
        counter += 1;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "bacteria")
        {
            //print("Collided with neuron");
            numberOfBacterias++; // Increment the collision counter
        }
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            // Calculate a random position within a range defined by shakeMagnitude
            Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            // Set the object's position to the shakePosition
            transform.position = shakePosition;

            // Increment elapsedTime by the time since the last frame
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= shakeDuration)
            {
                // Reset the object's position to its original position after the shake effect is finished
                transform.position = originalPosition;
            }
            // Yield execution of the coroutine until the next frame
            yield return null;
        }
    }

}
