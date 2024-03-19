using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //singleton pattern
    public static GameManager gameManager;
    public const int MAX_SHOTS = 3;
    private int currentNumShots = 0;

    [SerializeField] private IconHandler iconHandler;
    [SerializeField] private float LevelEndDelay = 3f;

    private List<Piggie> piggies= new List<Piggie>();

    [SerializeField] private GameObject restartScreenObj;
    [SerializeField] private SlingShotHandler slingShotHandler;


    private void Awake() 
    {
        if (gameManager == null)
        {
            gameManager = this;
        }

        FindPiggies();
    
    }

    public void FindPiggies()
    {
        Piggie[] piggieList = FindObjectsOfType<Piggie>();

        for (int i = 0; i < piggieList.Length; i++)
        {
            piggies.Add(piggieList[i]);
        }
    }
    public void UseShot()
    {
        currentNumShots++;
        iconHandler.UseShot(currentNumShots);

        CheckForLastShot();
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

    public void CheckForLastShot()
    {
        if(currentNumShots == MAX_SHOTS)
        {
            StartCoroutine(NoMoreShotsDelay());
        }
    }

    public void CheckForLastDeadPiggie()
    {
        if(piggies.Count == 0)
        {
            WinGame();
        }
    }

    private IEnumerator NoMoreShotsDelay()
    {
        yield return new WaitForSeconds(LevelEndDelay);

        if(piggies.Count == 0)
        {
            WinGame();
        }
        else
        {
            RestartGame();
        }
    }

    public void RemovePiggie(Piggie piggie)
    {
        piggies.Remove(piggie);
        CheckForLastDeadPiggie();
    }

    #region Win/Lose

    private void WinGame()
    {
        //makes restart screen visible
        restartScreenObj.SetActive(true);
        slingShotHandler.enabled = false;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
    

    
}
