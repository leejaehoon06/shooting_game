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
        mainText.text = "사망하셨습니다!";
        scoreText.text = $"최종 점수: {GameManager.current.score}";
    }
    public void Clear()
    {
        mainText.text = "축하드립니다!";
        scoreText.text = $"최종 점수: {GameManager.current.score}";
    }
}
