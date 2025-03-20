using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public enum TrapMode { RestartLevel, RegenerateLevel }
    public TrapMode trapMode = TrapMode.RestartLevel;

    private PlayerController _playerController;

    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (trapMode == TrapMode.RestartLevel)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (trapMode == TrapMode.RegenerateLevel)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trapMode == TrapMode.RestartLevel)
            {
                RestartLevel();
            }
            else if (trapMode == TrapMode.RegenerateLevel)
            {
                RegenerateLevel();
            }
        }
    }

    void RestartLevel()
    {
        _playerController.gameObject.transform.position = _playerController.spawnPosition;
    }

    void RegenerateLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
