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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
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
        SceneManager.LoadScene(GetSceneName());
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
