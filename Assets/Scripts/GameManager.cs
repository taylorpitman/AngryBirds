using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleton pattern
    public static GameManager gameManager;
    public const int MAX_SHOTS = 3;
    private int currentNumShots = 0;
    [SerializeField] private IconHandler iconHandler;
    private void Awake() 
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    
    }
    public void UseShot()
    {
        currentNumShots++;
        iconHandler.UseShot(currentNumShots);
    }

    public bool HasEnoughShots()
    {
        bool enoughShots = true;

        if(currentNumShots >= MAX_SHOTS)
        {
            enoughShots = false;
        }

        return enoughShots;
    }
}
