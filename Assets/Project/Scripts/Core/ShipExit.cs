using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipExit : MonoBehaviour
{
    public Vector2 shipExteriorPosition;
    public HUDHandler hudHandler;
    public GameObject mainCamera;

    public delegate void ShipExitAction();
    public static event ShipExitAction OnExit;


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
        PlayerInputs.instance.ignoreLights = true;
        if (OnExit != null) OnExit();

        hudHandler.DoFadeToBlack();
        PlayerInputs.instance.canMove = false;

        yield return new WaitForSeconds(1f);
        gameObjectTeleported.transform.position = shipExteriorPosition;
        mainCamera.transform.position = shipExteriorPosition;
        
        yield return new WaitForSeconds(1f);
        hudHandler.RestoreFades();
        PlayerInputs.instance.canMove = true;
        PlayerInputs.instance.ignoreLights = false;
    }
}
