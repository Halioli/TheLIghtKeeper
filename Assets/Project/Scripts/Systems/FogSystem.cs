using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSystem : MonoBehaviour
{
    public PlayerLightChecker playerLightChecker;
    private GameObject player;

    private bool playerInFog = false;
    public float timer;
    private bool hasFaded = false;

    public Vector3 respawnPosition;

    public GameObject skullEnemy;
    [SerializeField] private HUDHandler hudHandler;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        skullEnemy.SetActive(false);
        respawnPosition = new Vector3(110,-16,0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerInFog)
        {
            skullEnemy.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            player.GetComponentInChildren<Lamp>().lampTime = 0;
            if (!playerLightChecker.IsPlayerInLight())
            {

                if(timer > 0f)
                {
                    timer -= Time.deltaTime;
                    Debug.Log(timer);
                    skullEnemy.SetActive(true);
                    PlayerInputs.instance.canMove = false;
                }
                else
                {
                    if (!hasFaded)
                    {
                        StartCoroutine(RespawnFade());
                    }
                }
            }
            else
            {
                PlayerInputs.instance.canMove = true;
                ResetTimer();
                skullEnemy.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInFog = true;
            Debug.Log("PlayerIn");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            playerInFog = false;
            Debug.Log("PlayerOut");
            ResetTimer();
            skullEnemy.SetActive(false);
        }
    }
    private void ResetTimer()
    {
        timer = 1f;
    }

    IEnumerator RespawnFade()
    {
        hasFaded = true;
        hudHandler.DoFadeToBlack();

        //PlayerInputs.instance.canMove = false;
        skullEnemy.SetActive(false);

        yield return new WaitForSeconds(3f);

        player.transform.position = respawnPosition;

        hudHandler.RestoreFades();

        PlayerInputs.instance.canMove = true;
        hasFaded = false;
        ResetTimer();
    }
}
