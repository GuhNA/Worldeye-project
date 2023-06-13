using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    //Statics variables
    float iniSpeed;
    float idleTimerS;


    [Header("Movement")]
    [SerializeField] float fspeed;
    [SerializeField] float idleTimer;

    [Space(10)]
    [SerializeField] List<Transform> paths;
    int i;

    Vector2 pathPos;

    FindPlayer fp;
    Animator anim;
    public Player player;

    BaseStats bs;

    void Awake()
    {
        fp = GetComponent<FindPlayer>();
        anim = GetComponent<Animator>();
        bs = GetComponent<BaseStats>();
    }
    void Start()
    {
        iniSpeed = fspeed;
        idleTimerS = idleTimer;
        i = 0;
    }

    void Update()
    {
        //Timers
        if(fp.limitArea()) fp.cooldowmArea -= Time.deltaTime;

        //Animation
        if(fspeed == 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("NBdmg"))
        {
            idleTimer -= Time.deltaTime;
            anim.SetInteger("select",0);
        }
        if(fspeed > 0) anim.SetInteger("select",1);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(bs.life > 0)
        { 
        //Seguir player
            if(fp.limitArea() && fp.cooldowmArea < 0f && fp.find() && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("NBdmg"))
            {
                fp.Attack();

                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("NBattack"))
                {
                    rotation(player.transform.position.x);
                    fspeed = iniSpeed;
                    float distance =  (Mathf.Abs(player.transform.position.x - transform.position.x) - fp.stopToAtk);
                    if(Mathf.Abs(player.transform.position.x - transform.position.x) >= fp.stopToAtk)
                    {
                        
                        transform.position = Vector2.MoveTowards(transform.position, 
                                new Vector2(player.transform.position.x,transform.position.y),
                                                (fspeed+0.65f) * Time.deltaTime);
                    }

                }
            }
            else if(anim.GetCurrentAnimatorStateInfo(0).IsName("NBdmg"))
            {
                fspeed = 0;
            }
            //Seguir caminhos
            else
            {
                #region selectingPath
                if(!anim.GetCurrentAnimatorStateInfo(0).IsName("NBattack"))
                {

                    pathPos = new Vector2(paths[i].position.x, transform.position.y);
                    rotation(pathPos.x);
                    if(Vector2.Distance(transform.position, pathPos) < 0.15f)
                    {
                        fspeed = 0;
                    }
                    else if(idleTimer > 0)
                    {
                        fspeed = iniSpeed;
                        transform.position = Vector2.MoveTowards(transform.position,
                                                                            pathPos,
                                                                fspeed * Time.deltaTime);
                    }

                    if(idleTimer < 0)
                    {
                        idleTimer = idleTimerS;
                        i++;
                    }

                    if(i == (paths.Count))
                    {
                        i = 0;
                    }
                }
                #endregion

            }
        }
        else
        {
            fspeed = 0;
            bs.deathAnim();
            
        }

    }

    void rotation(float target)
    {
        if((transform.position.x - target) > 0) transform.eulerAngles = new Vector2 (0,180);
        else if((transform.position.x - target) < 0) transform.eulerAngles = new Vector2 (0,0);
    } 

}


