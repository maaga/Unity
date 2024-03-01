using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameObject;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>().gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowGameOver();
    }

    private void ShowGameOver() 
    {
        if (_player != null) return;

        _gameObject.SetActive(true);
    
    }
}