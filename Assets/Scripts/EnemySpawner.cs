using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySporner : MonoBehaviour
{
	[SerializeField,Header("敵オブジェクト")]
	private GameObject enemy;
	
	private Player player;
	private GameObject enemyObj;
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectType<Player>();
		enemyObj = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void SpawnEnemy()
	{
		if(player == null) retern;
		
		Vector3 playerPos = player.transform.position;
		Vector3 cameraMaxPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height));
		Vector3 scale = enemy.transform.lossyScale;

		float distance = Vector2.Distance(transforj.position, new Vector2(playerPos.x),new Vector2(playerPos));

	}
}
