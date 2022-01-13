using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipEntry : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add ship scene to current scene as to not lose anything
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
