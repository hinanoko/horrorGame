using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    //public string nextSceneName; // 下一个场景的名称
    public string playerTag = "Player"; // 玩家标签

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("玩家离开城堡，加载下一个场景：");
            //SceneManager.LoadScene(nextSceneName);
            SceneManager.LoadScene("GameOver");
        }
    }
}
