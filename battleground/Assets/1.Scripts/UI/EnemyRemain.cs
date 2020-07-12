using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyRemain : MonoBehaviour
{
    public Text text;
    public GameObject enemy;
    private int currentEnemies;

    private void Update()
    {
        currentEnemies = enemy.transform.childCount;
        text.text = "enemies remain: " + currentEnemies;
    }
}
