using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{

    [SerializeField] Image lifebar;
    Player player;
    void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Update()
    {
        lifebar.fillAmount = (float)player.life / 100;
    }
}
