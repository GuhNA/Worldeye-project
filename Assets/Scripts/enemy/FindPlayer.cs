using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{

    //Static Parameters
    float cooldowmAreaS;

    [Header("Attack Parameters")]
    [SerializeField] float followRange;
    [SerializeField] float Atkradius;

     [SerializeField, Range(0,1)] float stopToAtk_;
    public float stopToAtk
    {
        get{return stopToAtk_;}
        set{stopToAtk_ = value;}
    }

    [Space(20)]

    [Header("Limite Area")]
    [SerializeField] List<Transform> limiters;
    [SerializeField] float cooldowmArea_;

    [Space(5)]
    [SerializeField] LayerMask playerMask;
    public float cooldowmArea
    {
        get{return cooldowmArea_;}
        set{cooldowmArea_ = value;}
    }
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        cooldowmAreaS = cooldowmArea;
    }
    
    public bool find()
    {
        Vector2 position = new Vector2(transform.position.x + 0.1100961f, transform.position.y + 0.19763465f);
       if(Physics2D.OverlapCircle(position, followRange, playerMask))
       {
            return true;
       }
       return false;
    }

    //limiters[0] = esquerda
    //limiters[1] = direita
    public bool limitArea()
    {
        int x,y;
        //limiters[0] = direita, limiters[1] = esquerda
        if(limiters[0].position.x < limiters[1].position.x)
        {
            x = 0;
            y = 1;
        }
        else
        {
           x = 1;
           y = 0;
        }

        if((limiters[x].position.x > transform.position.x) ||
            limiters[y].position.x < transform.position.x )
        {
            cooldowmArea = cooldowmAreaS;
            return false;
        } 

        return true;
    }

    public void Attack()
    {
        Collider2D hit= Physics2D.OverlapCircle(transform.GetChild(0).position, Atkradius, playerMask);
        if(hit)
        { 
            anim.SetTrigger("atk");
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector2 position = new Vector2(transform.position.x + 0.1100961f, transform.position.y + 0.19763465f);
        Gizmos.DrawWireSphere(position, followRange);

        Gizmos.DrawWireSphere(transform.GetChild(0).position, Atkradius);
    }

}

