using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public readonly string MainMenu = "MainMenu UI";
    public readonly string GameScene = "RoomMap";
    public readonly string PlayerUI = "Player UI";
    public readonly string SettingUI = "Settings UI";

    public void LoadGame()
    {
        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
        SceneManager.LoadScene(PlayerUI, LoadSceneMode.Additive);
        SceneManager.LoadScene(SettingUI, LoadSceneMode.Additive);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenu, LoadSceneMode.Single);
    }
}