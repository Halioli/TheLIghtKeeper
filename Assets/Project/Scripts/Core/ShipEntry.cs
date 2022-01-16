using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipEntry : MonoBehaviour
{
    public Vector2 shipInteriorPosition;
    public HUDHandler hudHandler;
    public Animator animator;


    public delegate void ShipEntryAction();
    public static event ShipEntryAction OnEntry;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(TeleportToShipInterior(collision.gameObject));

            // Add ship scene to current scene as to not lose anything
            //SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }

    IEnumerator TeleportToShipInterior(GameObject gameObjectTeleported)
    {
        if (OnEntry != null) OnEntry();

        hudHandler.DoFadeToBlack();
        PlayerInputs.instance.canMove = false;

        yield return new WaitForSeconds(1f);
        gameObjectTeleported.transform.position = shipInteriorPosition;
        hudHandler.RestoreFades();
        PlayerInputs.instance.canMove = true;
        animator.SetBool("isHealed", false);
    }
}
