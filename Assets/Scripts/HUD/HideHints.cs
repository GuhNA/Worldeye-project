using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HideHints : MonoBehaviour
{
    [SerializeField] GameObject texts;
    void OnTriggerEnter2D(Collider2D hit)
    {
        if(hit.CompareTag("Player"))
        {
            texts.SetActive(false);
            Destroy(gameObject);
        }
    }
}
