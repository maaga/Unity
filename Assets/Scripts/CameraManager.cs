using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[SerializeField, Header("振動する時間")]
	private float shakeTime;
	[SerializeField, Header("振動の大きさ")]
	private float shakeMagnitude;
	
	
	private Player player;
	private Vector3 initPos;
	private float shakeCount; //振動している時間のカウント
	private int currentPlayerHP;
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
		currentPlayerHP = player.GetHP();  //初期HPの値を得る
		initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ShakeCheck();
		FollowPlayer();
    }
	
	private void ShakeCheck()
	{
		if(currentPlayerHP > player.GetHP()) //ダメージを喰らった判定
		{
			currentPlayerHP = player.GetHP();
			shakeCount = 0.0f;
			StartCoroutine(Shake());  //振動開始
		}
	}
	
	IEnumerator Shake()			//振動処理
	{
		Vector3 initPos = transform.position; //カメラの初期座標を取得
		
		while(shakeCount < shakeTime){
			float x = initPos.x + Random.Range(-shakeMagnitude,shakeMagnitude);
			float y = initPos.y + Random.Range(-shakeMagnitude,shakeMagnitude);
			transform.position = new Vector3(x,y,initPos.z);
			
			shakeCount += Time.deltaTime;  //フレームが変わるときの時間を加算
			
			yield return null; //処理を中断して次のフレームで処理を始める
		}
		
		transform.position = initPos; //カメラの座標を初期化
	}
	
	private void FollowPlayer()
	{
		float x = player.transform.position.x;
		x = Mathf.Clamp(x,initPos.x,Mathf.Infinity); //xの値を、カメラの初期座標initPos.x（最小値）からMathf.Infinity（無限大）の間に収める
		transform.position = new Vector3(x,transform.position.y,transform.position.z);
	}
}
