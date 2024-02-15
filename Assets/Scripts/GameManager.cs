using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public GameObject _player;
    
    [SerializeField] public int _numberOfMonstersToKill;
    
    public bool _isQuestActive = false;
    public bool _isQuestFinished = false;
    private int _monsterKilled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EnnemyKilled()
    {
        if(_isQuestActive)
        {
            _monsterKilled++;
            if(_monsterKilled >= _numberOfMonstersToKill) 
            {
                _isQuestFinished=true;
                _isQuestActive=false;
                Debug.Log("GG!!");
            }
        }
    }
}
