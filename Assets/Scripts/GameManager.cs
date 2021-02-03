using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static void GameOver()
    {
        GameState.State = GameState.States.Inactive;
        UIController.SetPanelActive(true);
        UIController.Panel.GetComponent<AudioSource>().Play(); //play game over sound effect
        UIController.SetFinalScore(Player.Points);
        SceneHelper.GetSpaceship().GetComponent<SpaceshipController>().GameOver();
        GameObject.Find("AsteroidBuilderHolder").GetComponent<AsteroidBuilder>().GameOver();
    }
    
    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Awake()
    {
        ReloadStatics();
    }

    private void ReloadStatics()
    {
        SceneHelper.Reload();
        ResourcesLoader.Reload();
        GameState.State = GameState.States.Active;
        Player.Reload();
        UIController.Reload();
    }
}
