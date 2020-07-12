using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearTime : MonoBehaviour
{
    public Text text;
    public GameObject enemy;
    private int currentEnemies;
    private float time;
    private int min;
    private int sec;


    private void Start()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        currentEnemies = enemy.transform.childCount;
        if (currentEnemies > 0)
        {
            time += Time.fixedDeltaTime;
            min = (int)time / 60;
            sec = (int)time % 60;
        }
        else
        {
            
            text.enabled = true;
            text.text = "축하합니다. 모든 적을 제거하였습니다." + "\n" + "      총 플레이 시간 : " + min + " 분 " + sec + " 초";
        }
    }

}