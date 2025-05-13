using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickReturnToMenu : MonoBehaviour
{
    public string mainMenuSceneName = "GameOver"; // 替换成你的主菜单场景名字

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ReturnToMainMenu();
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
