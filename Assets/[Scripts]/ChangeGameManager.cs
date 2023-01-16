using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeGameManager : Singleton<ChangeGameManager>
{

    public GameObject _3D;
    public GameObject[] _2D;

    public GameObject player;
    bool change;
    public bool is3D = true;
    public bool isWeapon = false;
    public bool isShield = false;
    public int swordDurability = 100;
    public int shieldDurability = 100;
    public int health = 100;
    public Slider healthBar;

    public bool isGamePaused = false;
    public GameObject PauseUI;
    // Start is called before the first frame update
    void Start()
    {
        _3D.gameObject.SetActive(true);
        for (int i = 0; i < _2D.Length; i++)
        { _2D[i].gameObject.SetActive(false); }
        healthBar.value = health;
        change = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            SceneManager.LoadScene("End");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void Pause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void ChangeGame(int index)
    {

        is3D = false;
        _2D[index].gameObject.SetActive(true);
        _3D.gameObject.SetActive(false);
    }

    public void ChangeGame3D(int index)
    {
        is3D = true;
        _2D[index].gameObject.SetActive(false);
        _3D.gameObject.SetActive(true);
    }

    public void GetNewSword()
    {
        swordDurability = 100;
        isWeapon = true;
    }

    public void GetNewSheild()
    {
        shieldDurability = 100;
        isShield = false;
    }

    public void ResetHealth()
    {
        healthBar.value = 100;
        health = (int)healthBar.value;
        GameObject.FindObjectOfType<PlayerStateMachine>().GetComponent<Health>().health = 100;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Damaged");
        healthBar.value -= damage;
        health = (int)healthBar.value;
        if (healthBar.value < 0)
        {
            healthBar.value = 0;
        }
        

    }

    public void HealDamage(int damage)
    {
        healthBar.value += damage;

        health = (int)healthBar.value;
        if (healthBar.value > 100)
        {
            healthBar.value = 100;
        }
        

    }
}
