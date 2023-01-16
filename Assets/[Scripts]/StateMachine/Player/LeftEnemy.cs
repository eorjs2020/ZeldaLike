using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeftEnemy : Singleton<LeftEnemy>
{
    // Start is called before the first frame update

    public TextMeshProUGUI leftEnemyText;

    private int leftEnemy;

    
    void Start()
    {
        leftEnemy = FindObjectsOfType<EnemyStateMachine>().Length;
        leftEnemyText.text = leftEnemy.ToString();
    }

    public void Update()
    {
        if(leftEnemy == 0)
        {
            SceneManager.LoadScene("End");
        }
    }

    public void KillEnemy()
    {
        leftEnemy -= 1;
        leftEnemyText.text = leftEnemy.ToString();
    }
}
