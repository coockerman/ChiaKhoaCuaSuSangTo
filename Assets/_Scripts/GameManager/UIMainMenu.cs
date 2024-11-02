using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject objBackGround;
    [SerializeField] private float moveSpeed = 1f; // Tốc độ di chuyển
    [SerializeField] private float moveDistance = 2f; // Khoảng cách di chuyển từ điểm ban đầu

    private Vector3 startPos;

    private void Start()
    {
        startPos = objBackGround.transform.position; // Lưu lại vị trí ban đầu của nền
        StartCoroutine(MoveAutoRightLeft());
    }

    IEnumerator MoveAutoRightLeft()
    {
        while (true)
        {
            // Di chuyển sang phải
            yield return MoveBackground(startPos + Vector3.right * moveDistance);
            // Di chuyển sang trái
            yield return MoveBackground(startPos - Vector3.right * moveDistance);
        }
    }

    IEnumerator MoveBackground(Vector3 targetPos)
    {
        while (Vector3.Distance(objBackGround.transform.position, targetPos) > 0.01f)
        {
            objBackGround.transform.position = Vector3.MoveTowards(objBackGround.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // Đợi cho đến frame tiếp theo
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
