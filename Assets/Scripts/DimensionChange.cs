using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionChange : MonoBehaviour
{  


    [Header("Dimensions")]
    [SerializeField] GameObject dimension;
    [SerializeField] GameObject dimension2;
    [SerializeField] GameObject gridDimension;
    [SerializeField] GameObject gridDimension2;

    Player player;
    Animator anim;
    float timer;

    bool actTimer;


    /* Trabalhar com os filhos da dimensao usando 
        Dimension.transform.GetChild(0).gameObject.SetActive(true); neste caso: Enemy
    */

    bool fstDim;
    bool actDim;

    void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        
    }
    void Start()
    {
        timer = 0.3f;
        fstDim = true;
        actDim = fstDim;
    }

    void Update()
    {
        if(player.canChange)
            if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Z))
            {
                player.cantJump = true;
                anim.SetBool("active", true);
                actTimer = true;
                player.speed = 0f;
            }
            
            if(actTimer)
            {
                timer -= Time.deltaTime;
                if(timer < 0)
                {
                    if(actDim == fstDim) GetComponent<PlayerAnim>().ChangingDimension("dim1");
                    else GetComponent<PlayerAnim>().ChangingDimension("dim2");
                    actTimer = false;
                }
            }

        if(fstDim == actDim)
        {
            dimension.SetActive(true);
            gridDimension.SetActive(true);
            dimension2.SetActive(false);
            gridDimension2.SetActive(false);

        }
        else
        {
            dimension.SetActive(false);
            gridDimension.SetActive(false);
            dimension2.SetActive(true);
            gridDimension2.SetActive(true);
        }
    }

    public void dimensionChange()
    {
        GetComponent<PlayerAnim>().anim.SetBool("active", false);
        actDim = !actDim;
    }

    public void getSpeedBack()
    {
        player.cantJump = false;
        player.speed = player.speedS;
    }
}
