using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessFaint : MonoBehaviour
{
    [SerializeField] float totalFaintTime = 30f;
    [SerializeField] float timeBeforeBeatStart = 15f;
    [SerializeField] float respawnTime = 5f;
    [SerializeField] float middleRespawnTime = 4.0f;

    bool hasFainted = false;

    [SerializeField] Vector2 teleportPosition;


    public delegate void DarknessFaintAction();
    public static event DarknessFaintAction OnHeartBeatsStart;
    public static event DarknessFaintAction OnFaintEnd;
    public static event DarknessFaintAction OnFaintStop;
    public static event DarknessFaintAction OnFaintEndRespawn;

    public delegate void DarknessFaintTeleportAction(Vector3 position);
    public static event DarknessFaintTeleportAction OnFaintTeleport;


    private void OnEnable()
    {
        PlayerLightChecker.OnPlayerEntersLight += StopFaintTimer;
        Lamp.OnLanternTurnsOnInDarkness += StopFaintTimer;

        PlayerLightChecker.OnPlayerInDarknessNoLantern += StartFaintTimer;
    }

    private void OnDisable()
    {
        PlayerLightChecker.OnPlayerEntersLight -= StopFaintTimer;
        Lamp.OnLanternTurnsOnInDarkness -= StopFaintTimer;

        PlayerLightChecker.OnPlayerInDarknessNoLantern -= StartFaintTimer;
    }



    private void StartFaintTimer()
    {
        StartCoroutine("FaintTimer");
    }

    private void StopFaintTimer()
    {
        if (hasFainted) return;

        StopCoroutine("FaintTimer");

        if (OnFaintStop != null) OnFaintStop();
    }


    IEnumerator FaintTimer()
    {
        yield return new WaitForSeconds(timeBeforeBeatStart);

        if (OnHeartBeatsStart != null) OnHeartBeatsStart();
        yield return new WaitForSeconds(totalFaintTime - timeBeforeBeatStart);


        if (OnFaintEnd != null) OnFaintEnd();
        hasFainted = true;
        PlayerInputs.instance.canMove = false;
        yield return new WaitForSeconds(middleRespawnTime);


        if (OnFaintTeleport != null) OnFaintTeleport(teleportPosition);
        yield return new WaitForSeconds(respawnTime - middleRespawnTime);


        hasFainted = false;
        PlayerInputs.instance.canMove = true;
        if (OnFaintEndRespawn != null) OnFaintEndRespawn();
    }

}
