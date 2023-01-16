using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{

    public ChangeGameManager _gameManager;
    public int StageIndex = 0;
    public Transform exit;
    public GameObject player;
    public GameObject d;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            int pHealth = 0;
            pHealth = other.GetComponentInChildren<Health>().health;
            Destroy(other.GetComponentInParent<Player>().gameObject);
            var P = Instantiate(player, exit.position, Quaternion.identity);
            P.GetComponentInChildren<Health>().health = pHealth;
            P.transform.parent = d.transform;
            //player.GetComponentInChildren<Health>().health = 100;
            //GameObject.FindObjectsOfType<EnemyStateMachine>();
            foreach (EnemyStateMachine go in GameObject.FindObjectsOfType(typeof(EnemyStateMachine)))
            {
                go.FindPlayer();
            }
            _gameManager.ChangeGame(StageIndex);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _gameManager.ChangeGame3D(StageIndex);            
            Destroy(this.gameObject);
        }
    }
   
}
