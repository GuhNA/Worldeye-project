using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    //Static Variable
    float cooldownS;
    [SerializeField] int life_;
    [SerializeField] int damage_;
    [SerializeField] int staggerCount;
    //contador do stagger
    int i = 0;

    #region encapsulation
    public int damage
    {
        get{return damage_;}
        set{damage_ = value;}
    }

    public int life
    {
        get { return life_;}
        set {life_ = value;}
    }

    #endregion

    void Update()
    {
        if(life <= 0)
        {
            deathAnim();
        }
        
    }

    public void deathAnim()
    {  
        GetComponent<Animator>().SetTrigger("die");
    }

    public void ChangeLife(int amount)
    {
        life += amount;

        if(amount < 0)
        {
            i++;
        }

        if(i == staggerCount) 
        {
            GetComponent<Animator>().SetTrigger("damage");
            i = 0;
        }
    }

    public void death()
    {
        Destroy(gameObject);
    }
}
