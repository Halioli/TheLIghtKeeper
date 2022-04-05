using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlayer : MonoBehaviour
{
    private HealthSystem playerHealthSystem;
    public ParticleSystem healingParticles;


    // Start is called before the first frame update
    void Start()
    {
        playerHealthSystem = GetComponent<HealthSystem>();
        healingParticles.Stop();
    }

    private void OnEnable()
    {
        HealingStation.OnHealedByHealingStation += PlayerHealedByStation;
        Hotkeys.OnHealed += PlayerHealed;
    }

    private void OnDisable()
    {
        HealingStation.OnHealedByHealingStation -= PlayerHealedByStation;
        Hotkeys.OnHealed -= PlayerHealed;
    }

    private void PlayerHealedByStation()
    {
        playerHealthSystem.RestoreHealthToMaxHealth();
        StartCoroutine(PlayHealingParticles());
    }

    private void PlayerHealed(int healthToAdd)
    {
        playerHealthSystem.ReceiveHealth(healthToAdd);
        StartCoroutine(PlayHealingParticles());
    }
    IEnumerator PlayHealingParticles()
    {
        healingParticles.Play();
        yield return new WaitForSeconds(1f);
        healingParticles.Stop();
    }
}
