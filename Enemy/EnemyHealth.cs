using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 5;
    //[SerializeField] int difficultyRamp = 1;
    [SerializeField] float hitPointUpRatio = 20/100f;
    float currentHitPoints = 0;

    Enemy enemy;
    [SerializeField] TMP_Text healthText;

    private void OnEnable()
    {
        currentHitPoints = maxHitPoints;
        UpdateHealthText();
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void UpdateHealthText()
    {
        if (currentHitPoints >= 0)
            healthText.text = ((int)currentHitPoints).ToString();
        else
            healthText.text = "0";
    }
    private void ProcessHit()
    {
        currentHitPoints--;
        UpdateHealthText();
        if (currentHitPoints < 1)
        {
            HandleEnemyDeath();
        }
    }

    void HandleEnemyDeath()
    {
        maxHitPoints = maxHitPoints + (maxHitPoints * hitPointUpRatio);
        enemy.RewardGold();
        gameObject.SetActive(false);
    }
}
