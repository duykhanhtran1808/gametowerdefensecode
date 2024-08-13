using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] int cost = 25;
      
        public bool CreateTower(Tower tower, Vector3 position)
        {
            Bank bank = FindObjectOfType<Bank>();
            if (bank == null) { return false; }
            if (bank.CurrentMoney >= cost)
            { 
            GameObject newTower = Instantiate(tower.gameObject, position, Quaternion.identity);
                
            bank.WithdrawMoney(cost);
            return true;
            }
            else
            {
                return false;
            }
        }

        


    }
}