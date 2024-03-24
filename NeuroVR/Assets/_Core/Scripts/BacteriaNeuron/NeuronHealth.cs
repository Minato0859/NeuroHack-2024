using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronHealth : MonoBehaviour
{
    public int Health = 100;
    public int BacteriaDamage = 1;
    public int CooldownTime = 10;
    public float shakeDuration = 0.5f; // Adjust this value to control the duration of the shake

    private int counter = 0;
    private int numberOfBacterias = 0;

    private Color flashColor = new Color(1f, 0.5f, 0.5f, 0.5f); // Red color with transparency
    private Color originalColor;

    private Transform neuron;
    private Transform cellTransform;
    private Renderer cellRenderer;


    // Start is called before the first frame update
    void Start()
    {
        neuron = transform.Find("neuron1");
        cellTransform = neuron.Find("cell");
        cellRenderer = cellTransform.GetComponent<Renderer>();
        originalColor = cellRenderer.material.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        //60fps therefore, this is one second * attack speed
        if (counter == (60 * CooldownTime))
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
                // Trigger flash effect
                StartCoroutine(FlashRed());
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

    private IEnumerator FlashRed()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            cellRenderer.material.color = flashColor;

            // Increment elapsedTime by the time since the last frame
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= shakeDuration)
            {
                // Reset the object's position to its original position after the shake effect is finished
                cellRenderer.material.color = originalColor;
            }
            // Yield execution of the coroutine until the next frame
            yield return null;
        }
    }
}