using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipExit : MonoBehaviour
{
    public Vector2 shipExteriorPosition;
    public HUDHandler hudHandler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportToShipExterior(collision.gameObject));

            // Unload ship scene
            //SceneManager.UnloadSceneAsync(2);
        }
    }

    IEnumerator TeleportToShipExterior(GameObject gameObjectTeleported)
    {
        hudHandler.DoFadeToBlack();
        PlayerInputs.instance.canMove = false;

        yield return new WaitForSeconds(1f);
        gameObjectTeleported.transform.position = shipExteriorPosition;
        hudHandler.RestoreFades();
        PlayerInputs.instance.canMove = true;
    }
}
