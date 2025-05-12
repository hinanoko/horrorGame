using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Trans : MonoBehaviour
{
    //public string nextSceneName; // ��һ������������
    public string playerTag = "Player"; // ��ұ�ǩ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Debug.Log("����뿪�Ǳ���������һ��������");
            //SceneManager.LoadScene(nextSceneName);
            SceneManager.LoadScene("Level6");
        }
    }
}
