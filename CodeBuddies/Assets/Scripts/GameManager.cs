using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public PlayerController1 player1;
    public PlayerController2 player2;
    
    [SerializeField, Tooltip("P1 winning coords")]
    private Vector2 p1Coords;
    [SerializeField, Tooltip("P2 winning coords")]
    private Vector2 p2Coords;
    [SerializeField, Tooltip("Next Level")]
    private string nextSceneName;
    
    [SerializeField, Tooltip("Winning tolerance margin")]
    private float winTolerance = 0.3f;
    
    //SFX Stuff
    public AudioSource SFX;
    [SerializeField] public AudioClip win, lose;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CheckForLoss()
    {
        bool isP1Winning = Vector2.Distance((Vector2)player1.transform.position, p1Coords) <= winTolerance;
        bool isP2Winning = Vector2.Distance((Vector2)player2.transform.position, p2Coords) <= winTolerance;
        if (player1.IsFinished && player2.IsFinished)
        {

            if (!isP1Winning || !isP2Winning)
            {
                SFX.clip = lose;
                SFX.Play();
                ResetPlayers();
            }
        }
    }
    
    public void CheckForWin()
    {
        if (player1.IsFinished && player2.IsFinished)
        {
            bool isP1Winning = Vector2.Distance((Vector2)player1.transform.position, p1Coords) <= winTolerance;
            bool isP2Winning = Vector2.Distance((Vector2)player2.transform.position, p2Coords) <= winTolerance;

            if (isP1Winning && isP2Winning)
            {
                StartCoroutine(WaitForSoundAndLoadNextScene());
            }
        }
    }

    private IEnumerator WaitForSoundAndLoadNextScene()
    {
        SFX.clip = win;
        SFX.Play();
        yield return new WaitForSeconds(win.length);
        SceneManager.LoadScene(nextSceneName);
    }

    private void ResetPlayers()
    {
        Debug.Log("Both players lost! Resetting positions...");
        
        player1.transform.position = player1.startingPosition;
        player2.transform.position = player2.startingPosition;

        player1.IsFinished = false;
        player2.IsFinished = false;

        player1.ClearCommands();
        player2.ClearCommands();
    }
}