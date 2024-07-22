using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.current.StartCoroutine(GodMod());
            gameObject.SetActive(false);
        }
    }
    IEnumerator GodMod()
    {
        Player.current.godMod = true;
        yield return new WaitForSeconds(7);
        Player.current.godMod = false;
    }
}
