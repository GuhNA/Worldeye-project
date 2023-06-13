using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class HiddenArea : MonoBehaviour
{
    float fade = 1;
    bool faded;
    [SerializeField] Tilemap tilemap;
    void Update()
    {
        if(!faded)
        {
            if(fade <= 1)
            fade = Mathf.Clamp((fade += Time.deltaTime *0.5f),0,1);
            tilemap.color = new Color(tilemap.color.r,
                                        tilemap.color.g,
                                        tilemap.color.b,
                                        fade);
        }
        else
        {
            if(fade >= 0.4f)
            fade = Mathf.Clamp((fade -= Time.deltaTime *0.5f),0,1);
            tilemap.color = new Color(tilemap.color.r,
                                        tilemap.color.g,
                                        tilemap.color.b,
                                        fade);
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Player"))
        {
            faded = true;
            
        }
    }
    
    void OnTriggerExit2D(Collider2D hit)
    {
        if(hit.CompareTag("Player"))
        {
           faded = false;
            
        }
    }
}
