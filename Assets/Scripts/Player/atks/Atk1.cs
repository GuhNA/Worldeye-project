using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atk1 : MonoBehaviour
{
    [SerializeField] int damage;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Enemy"))
        {
            if(hit.GetComponent<BaseStats>().life > 0)
                hit.GetComponent<BaseStats>().ChangeLife(-damage);
        }
    }
}
