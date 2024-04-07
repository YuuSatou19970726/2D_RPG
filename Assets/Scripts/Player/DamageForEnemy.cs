using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageForEnemy : MonoBehaviour
{
    public float damageToGive;
    public GameObject damageBurst;
    public Transform hitPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.ENEMY_TAG))
        {
            other.gameObject.GetComponent<EnemyHpManager>().DmgEnemy(damageToGive);
            Instantiate(damageBurst, hitPoint.position, hitPoint.rotation);
        }
    }
}
