using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallaxEffect;
    float startPos;
    float length;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float rePos = Camera.main.transform.position.x * (1 - parallaxEffect);
        float distance = Camera.main.transform.position.x *parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(rePos > startPos + length)
        {
            startPos += length;
        }
        else if(rePos < startPos - length)
        {
            startPos -= length;
        }
    }
}