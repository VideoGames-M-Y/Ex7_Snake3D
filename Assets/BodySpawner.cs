// using System.Collections.Generic;
// using UnityEngine;

// public class BodySpawner : MonoBehaviour
// {
//     [SerializeField] private GameObject bodyPrefab; // The prefab for the body part
//     [SerializeField] private float followDistance = 0.5f; // Distance between each body part

//     private List<Transform> bodyParts = new List<Transform>(); // List to store body parts

//     void Update()
//     {
//         // Update positions of body parts to follow like a train without overlap
//         Vector3 previousPosition = transform.position;
//         for (int i = 0; i < bodyParts.Count; i++)
//         {
//             Vector3 currentBodyPosition = bodyParts[i].position;
//             float distance = Vector3.Distance(currentBodyPosition, previousPosition);

//             if (distance > followDistance)
//             {
//                 bodyParts[i].position = Vector3.Lerp(currentBodyPosition, previousPosition, Time.deltaTime * 10);
//             }
//             previousPosition = bodyParts[i].position;
//         }
//     }

//     public void AddBodyPart()
//     {
//         // Instantiate a new body part
//         Vector3 spawnPosition = transform.position;
//         if (bodyParts.Count > 0)
//         {
//             spawnPosition = bodyParts[bodyParts.Count - 1].position;
//         }

//         GameObject newBodyPart = Instantiate(bodyPrefab, spawnPosition, Quaternion.identity);
//         bodyParts.Add(newBodyPart.transform);
//     }

//     public void RemoveBodyPart()
//     {
//         if (bodyParts.Count > 0)
//         {
//             Transform bodyPartToRemove = bodyParts[bodyParts.Count - 1];
//             bodyParts.RemoveAt(bodyParts.Count - 1);
//             Destroy(bodyPartToRemove.gameObject);
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         // Check if the player head collides with an apple
//         if (other.gameObject.CompareTag("Apple"))
//         {
//             Destroy(other.gameObject); // Destroy the apple
//             AddBodyPart(); // Add a body part
//         }
//         else if (other.gameObject.CompareTag("Enemy"))
//         {
//             RemoveBodyPart(); // Remove a body part
//         }
//     }
// }

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BodySpawner : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab; // The prefab for the body part
    [SerializeField] private float followDistance = 0.5f; // Distance between each body part

    private List<Transform> bodyParts = new List<Transform>(); // List to store body parts

    void Update()
    {
        // Update positions and rotations of body parts to follow like a train without overlap
        Vector3 previousPosition = transform.position;
        Quaternion previousRotation = transform.rotation;

        for (int i = 0; i < bodyParts.Count; i++)
        {
            Vector3 currentBodyPosition = bodyParts[i].position;
            Quaternion currentBodyRotation = bodyParts[i].rotation;
            float distance = Vector3.Distance(currentBodyPosition, previousPosition);

            if (distance > followDistance)
            {
                bodyParts[i].position = Vector3.Lerp(currentBodyPosition, previousPosition, Time.deltaTime * 10);
                bodyParts[i].rotation = Quaternion.Lerp(currentBodyRotation, previousRotation, Time.deltaTime * 10);
            }

            previousPosition = bodyParts[i].position;
            previousRotation = bodyParts[i].rotation;
        }
    }

    public void AddBodyPart()
    {
        // Instantiate a new body part
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;
        if (bodyParts.Count > 0)
        {
            spawnPosition = bodyParts[bodyParts.Count - 1].position;
            spawnRotation = bodyParts[bodyParts.Count - 1].rotation;
        }

        GameObject newBodyPart = Instantiate(bodyPrefab, spawnPosition, spawnRotation);
        bodyParts.Add(newBodyPart.transform);
    }

    public void RemoveBodyPart()
    {
        HPManager.Instance.LoseHP(); // Lose 1 HP
        if (bodyParts.Count > 0)
        {
            Transform bodyPartToRemove = bodyParts[bodyParts.Count - 1];
            bodyParts.RemoveAt(bodyParts.Count - 1);
            Destroy(bodyPartToRemove.gameObject);
        }
        else
        {
            // Game over logic
            Debug.Log("Game Over!");
            //SceneManager.LoadScene("GameOver");

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player head collides with an apple
        if (other.gameObject.CompareTag("Apple"))
        {
            Destroy(other.gameObject); // Destroy the apple
            ScoreManager.Instance.AddScore(1); // Add 1 to the score
            HPManager.Instance.AddHP(1); // Add 1 to the HP
            AddBodyPart(); // Add a body part
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            RemoveBodyPart(); // Remove a body part
        }
    }
}

