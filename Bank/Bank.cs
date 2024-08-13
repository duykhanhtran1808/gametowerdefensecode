using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingMoney = 150;
    [SerializeField] int currentMoney;
    public int CurrentMoney {  get { return currentMoney; } }

    private void Awake()
    {
        currentMoney = startingMoney;
    }

    public void AddMoney(int amount)
    {
        currentMoney += Mathf.Abs(amount);
    }

    public void WithdrawMoney(int amount)
    {
        currentMoney -= Mathf.Abs(amount);
        if(currentMoney < 0)
        {
            //Lose the game
            ReloadScene();
        }
    }
    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
