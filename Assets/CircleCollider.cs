using System.Collections.Generic;
using UnityEngine;

public class CircleCollector : MonoBehaviour
{
    // List to hold the audio clips.
    public List<AudioClip> audioClips = new List<AudioClip>();

    // Current index to track which audio clip to play next.
    private int currentAudioClipIndex = 0;

    // Reference to the audio source component.
    private AudioSource audioSource;

    // Proximity distance within which a circle is considered "collected".
    public float collectionDistance = 5f;

    void Start()
    {
        // Get the audio source component from the dragon game object.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Find all game objects tagged as "Circle".
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");

        foreach (var circle in circles)
        {
            // Check the distance between the dragon and each circle.
            if (Vector3.Distance(transform.position, circle.transform.position) <= collectionDistance)
            {
                CollectCircle(circle);
            }
        }
    }

    void CollectCircle(GameObject circle)
    {
        // Make the circle disappear.
        Destroy(circle);

        // Play the next audio clip in the list if there is one.
        if (audioClips.Count > 0 && currentAudioClipIndex < audioClips.Count)
        {
            audioSource.clip = audioClips[currentAudioClipIndex];
            audioSource.Play();

            // Move to the next clip in the list for the next collection.
            currentAudioClipIndex++;

            // Optional: Reset to the first clip after the last one plays.
            if (currentAudioClipIndex >= audioClips.Count)
            {
                currentAudioClipIndex = 0; // Loop back to the start or handle as needed.
            }
        }
    }
}
