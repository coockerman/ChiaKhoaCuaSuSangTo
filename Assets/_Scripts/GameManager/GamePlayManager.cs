using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void ChuyenMan()
    {
        int sceneNow = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        // Kiểm tra nếu scene hiện tại là scene cuối cùng
        if (sceneNow <= totalScenes - 1)
        {
            SceneManager.LoadScene(sceneNow + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}
