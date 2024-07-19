using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    Text Level;
    [SerializeField]
    Slider HpBar;
    [SerializeField]
    Slider ExpBar;
    [SerializeField]
    Slider UltimateBar;

    Player player;
    private void Start()
    {
        player = Player.current;
    }

    void Update()
    {
        Level.text = $"Lv.{player.level}";
        HpBar.value = Mathf.Lerp(HpBar.value, player.curHp / player.maxHp, 0.2f);
        ExpBar.value = Mathf.Lerp(ExpBar.value, player.curExp / player.maxExp, 0.2f);
        UltimateBar.value = Mathf.Lerp(UltimateBar.value, player.curUltimatePoint / player.maxUltimatePoint, 0.2f);
    }
}
