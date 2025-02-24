using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

enum SceneEnum
{
    MainMenu,
    Village,
    Dungeon,
    GameOver,
    Finish
}

public class ChangeScene : MonoBehaviour
{
    [SerializeField] SceneEnum scene;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject virtualCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (player != null) {
                DontDestroyOnLoad(player);
            }
            if (UI != null) {
                DontDestroyOnLoad(UI);
            }
            if (virtualCam != null) {
                DontDestroyOnLoad(virtualCam);
            }
            player.transform.position = Vector3.zero;
            player.ResetDestinationPoint();
            LoadNextScene();
        }
    }

    private string GetSceneName()
    {
        if (scene == SceneEnum.MainMenu)
        {
            return "MainMenu";
        }
        else if (scene == SceneEnum.Village)
        {
            return "Village";
        }
        else if (scene == SceneEnum.Dungeon)
        {
            return "Dungeon";
        }
        else if (scene == SceneEnum.GameOver)
        {
            return "GameOver";
        }
        else if (scene == SceneEnum.Finish)
        {
            return "Finish";
        }
        else
        {
            return "";
        }
    }

    public void LoadNextScene()
    {
        string sceneName = GetSceneName();
        if (sceneName == "Finish") { 
            Destroy(player);
            Destroy(UI);
            Destroy(virtualCam);
        }
        SceneManager.LoadScene(GetSceneName());
    }

    public void LoadNextScene(string sceneName)
    {
        if (sceneName == "Finish") { 
            Destroy(player);
            Destroy(UI);
            Destroy(virtualCam);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
