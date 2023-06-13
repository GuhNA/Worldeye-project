using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    Player player;
    Animator anim_;

    public Animator anim
    {
        get{return anim_;}
        set{anim_ = value;}
    }
    void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        OnRun();
        OnJump();
    }



    void OnRun()
    {
        if(Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) > 0.1f)
            anim.SetInteger("select",1);
        else
        {
            anim.SetInteger("select",0);
        }
        
    }

    public void ChangingDimension(string dimension)
    {
        anim.SetTrigger(dimension);
    }

    void OnJump()
    {
        if(player.GetComponent<Rigidbody2D>().velocity.y > 0.2f && !player.onGrounded())
        {
            anim.SetTrigger("jump");
        }
        else if(player.GetComponent<Rigidbody2D>().velocity.y < 0.2f && !player.onGrounded())
        {
            anim.SetTrigger("fall");
        }
    }
}
