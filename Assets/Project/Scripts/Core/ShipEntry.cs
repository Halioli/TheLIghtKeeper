using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipEntry : MonoBehaviour
{
    public Vector2 shipInteriorPosition;
    public HUDHandler hudHandler;
    public Animator animator;
    public GameObject mainCamera;

    public delegate void ShipEntryAction();
    public static event ShipEntryAction OnEntry;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportToShipInterior(collision.gameObject));
        }
    }

    IEnumerator TeleportToShipInterior(GameObject gameObjectTeleported)
    {
        PlayerInputs.instance.ignoreLights = true;


        if (OnEntry != null) 
            OnEntry();

        hudHandler.DoFadeToBlack();
        PlayerInputs.instance.canMove = false;

        yield return new WaitForSeconds(1f);
        gameObjectTeleported.transform.position = shipInteriorPosition;
        mainCamera.transform.position = shipInteriorPosition;

        yield return new WaitForSeconds(1f);
        hudHandler.RestoreFades();
        PlayerInputs.instance.canMove = true;

        animator.SetBool("isHealed", false);
    }
}
