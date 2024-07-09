using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;

    [SerializeField]
    int[] _scoreNumbers;
    public int[] scoreNumbers {  get { return _scoreNumbers; } }
    public int difficulty { get; set; }
    [SerializeField]
    UnityEngine.UI.Text ScoreText;
    public int score { get; set; }

    [SerializeField] 
    GameObject GameoverText;
    public void Died()
    {
        //GameoverText.SetActive(true);
    }

    
    //int Bestscore = 0;
    private void Awake()
    {
        if(current == null)
        {
            current = this;
        }
        else
        {
            Debug.LogError("Not Single");
        }
        
    }


    public void AddScore(int point)
    {
        score += point;
        ScoreText.text = "SCORE:" + score.ToString();
        for (int i = _scoreNumbers.Length - 1; i >= 0; i--)
        {
            if (_scoreNumbers[i] <= score)
            {
                difficulty = i + 1;
                break;
            }
        }
        
    }

}
