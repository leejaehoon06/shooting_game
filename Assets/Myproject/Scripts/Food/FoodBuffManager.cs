using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Damage,
    Speed,
    AttackSpeed,
    HpRegen,
    HpHeal
}
public class FoodBuffManager : MonoBehaviour
{
    public static FoodBuffManager current;

    Player player;
    void Awake()
    {
        current = this;
    }
    private void Start()
    {
        player = Player.current;
    }
    public IEnumerator Buff(BuffType buffType, float buffNum, float buffTime)
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        float timer = 0;
        if (buffType == BuffType.Damage)
        {
            player.attackPower += buffNum;
            while (timer <= buffTime)
            {
                timer += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            player.attackPower -= buffNum;
        }
        else if (buffType == BuffType.Speed)
        {
            player.speed *= buffNum;
            while (timer <= buffTime)
            {
                timer += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            player.speed /= buffNum;
        }
        else if (buffType == BuffType.AttackSpeed)
        {
            player.attackSpeed *= buffNum;
            while (timer <= buffTime)
            {
                timer += Time.deltaTime;
                yield return waitForEndOfFrame;
            }
            player.attackSpeed /= buffNum;
        }
        else if (buffType == BuffType.HpRegen)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(1);
            while (timer < buffTime)
            {
                timer += 1;
                player.Heal(buffNum);
                yield return waitForSeconds;
            }
        }
        else if (buffType == BuffType.HpHeal)
        {
            player.Heal(buffNum);
        }
    }
}
