using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaaaaaa : MonoBehaviour
{
    public float range;
    public LayerMask playerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        find();
    }


    void find()
    {
        Vector2 position = new Vector2(transform.position.x , transform.position.y);
       if(Physics2D.OverlapCircle(position, range, playerMask))
       {
            print("achei");
       }
    }

        void OnDrawGizmosSelected()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        Gizmos.DrawWireSphere(position, range);
    }
}
