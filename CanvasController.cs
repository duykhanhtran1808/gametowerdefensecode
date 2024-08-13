using Assets.Scripts.Tower;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] TMP_Text goldText;
    [SerializeField] TMP_Text currentStrengthText;
    [SerializeField] Button menuButton;
    [SerializeField] Button upgradeButton;
    [SerializeField] GameObject shopPanel;
    Bank bank;
    int upgradePrice = 100;
    float upgradeRatio = 1f;
    float currentAttack = 1f;
    //Tower towerControl = new Tower();
    Tower[] towerList;
    void Start()
    {
        bank = FindObjectOfType<Bank>();

        menuButton.onClick.AddListener(delegate
        {
            shopPanel.SetActive(!shopPanel.activeInHierarchy);
        });
        upgradeButton.onClick.AddListener(delegate
        {
            upgradeWeapon();
        });
    }

    // Update is called once per frame
    void Update()
    {
        DisplayMoney();

        
    }

    private void DisplayMoney()
    {
        goldText.text = "Money: " + bank.CurrentMoney;
    }
    void upgradeWeapon()
    {
        
        if (bank.CurrentMoney >= upgradePrice)
        {
            bank.WithdrawMoney(upgradePrice);
            towerList = FindObjectsOfType<Tower>();
            AddTowerStrength(towerList);
            currentStrengthText.text = "Attack Speed: " + ((int)currentAttack * 100) + "%";
            upgradePrice += 100;
            upgradeButton.transform.GetChild(0).GetComponent<TMP_Text>().text = "Upgrade - " + upgradePrice + " gold.";
        }
    }

    void AddTowerStrength(Tower[] allTowers)
    {
        currentAttack += upgradeRatio;
        foreach(Tower tower in allTowers)
        {
            var emission = tower.transform.GetChild(1).GetChild(0).GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = currentAttack;
        }
    }
}
