using UnityEngine;

public class enemyAtkRange : MonoBehaviour
{
    [SerializeField] BaseStats enemy;

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Player"))
        {
            hit.GetComponent<Player>().fallDmg = false;
            hit.GetComponent<Player>().ChangeLife(-enemy.damage);
        }
    }
}
