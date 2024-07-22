using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    Text mainText;
    [SerializeField]
    Text scoreText;
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
    public void Died()
    {
        mainText.text = "����ϼ̽��ϴ�!";
        scoreText.text = $"���� ����: {GameManager.current.score}";
    }
    public void Clear()
    {
        mainText.text = "���ϵ帳�ϴ�!";
        scoreText.text = $"���� ����: {GameManager.current.score}";
    }
}
