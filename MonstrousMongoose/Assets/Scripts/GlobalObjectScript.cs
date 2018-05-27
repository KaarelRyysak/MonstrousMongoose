﻿namespace Assets
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GlobalObjectScript : MonoBehaviour
    {
        public static GlobalObjectScript Instance;
        private float playerHealth;
        private int playerDashLimit;
        private int playerDashCount;

        public  GameObject character;
        private GameObject player;

        //Ensures there is only ever one GLobal Object active at a time
        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            playerHealth = 100.00f;
            playerDashLimit = 3;
            playerDashCount = 3;
            player = Instantiate<GameObject>(character);
        }

        //Gets and sets for player health and dashes
        public float getHealth()
        {
            return playerHealth;
        }
        public int getDashLimit()
        {
            return playerDashLimit;
        }
        public int getDashCount()
        {
            return playerDashCount;
        }

        public void setHealth(float health)
        {
            playerHealth = health;
        }
        public void setDashLimit(int dashLimit)
        {
            playerDashLimit = dashLimit;
        }
        public void setDashCount(int dashCount)
        {
            playerDashCount = dashCount;
        }
    }
}