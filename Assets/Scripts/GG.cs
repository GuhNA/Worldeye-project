using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GG : MonoBehaviour
{
    bool timergg;
    float timer = 1.4f;
    void Update()
    {  
        if(timergg) timer -= Time.deltaTime;
        if(timer < 0) SceneManager.LoadScene(0);
        if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene(0);
    }
    void OnTriggerStay2D(Collider2D hit)
    {
        if(hit.CompareTag("Player") && (
            hit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("changeDim") ||
            hit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("changeDim2")))
        {
            timergg = true;
        }
    }
}
