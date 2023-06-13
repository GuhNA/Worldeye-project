using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    float cooldown = 5f;
    bool cdReset = true;
    float iniCooldown;
    BoxCollider2D box; 
    [SerializeField] Vector2 resetPos;
    public Vector2 nextPos;
    [SerializeField] Transform[] list;
    int i = 0;
    Transform player;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        box = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        iniCooldown = cooldown;
        resetPos = list[0].position;
        nextPos  = list[0].position;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        if(player.position.x > nextPos.x)
        {
            if(i != list.Length-1)
            {
                resetPos = nextPos;
                once();
                nextPos = new Vector2(list[i].position.x, list[i].position.y);
            }
            else resetPos = nextPos;

        }
    }
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Player"))
        {
            Player player = hit.GetComponent<Player>();
            player.ChangeLife(-10);
            player.fallDmg = true;
            player.transform.position = new Vector3(resetPos.x,
                                                    resetPos.y,
                                                            0);
        }
    }
    void once()
    {
        if(cdReset)
        {   
            i++;
            cdReset = false;
        }
        if(cooldown < 0)
        {
            cdReset = true;
            cooldown = iniCooldown;
        }
    }
}
