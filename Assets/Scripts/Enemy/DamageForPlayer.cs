using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForPlayer : MonoBehaviour
{
    public float damageToGive;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == GameObjectNames.PLAYER_NAME)
        {
            other.gameObject.GetComponent<PlayerHPmanager>().DmgPlayer(damageToGive);
        }
    }
}
