using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField,Header("移動速度")]
    private float moveSpeed;
	[SerializeField, Header("ジャンプ速度")]
    private float jumpSpeed;
	[SerializeField, Header("体力")]
    private int hp;
	[SerializeField,Header("無敵時間")]
	private float damageTime;
	[SerializeField, Header("点滅時間")]
	private float flashTime;
	
    //入力された方向を入れる変数
    private Vector2 inputDirection;
    //移動方向入れる変数
    private Rigidbody2D rigid;
	private Animator anim;
	private SpriteRenderer spriteRenderer;
	private bool bjump; //ジャンプ可能
	

    void Start()
    {
        //PlayerのRigidbody2Dコンポーネントを取得する
        rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		bjump = false;
    }
    void Update()
    {
        Move();
		anim.SetBool("Walk",inputDirection.x != 0.0f); //横に入力がなければFALSE
		Debug.Log(hp);
        
    }
	
    private void Move()
    {
        //プレイヤーが入力した方向に横方向限定で移動速度分の力を加える
        rigid.velocity = new Vector2(inputDirection.x * moveSpeed, rigid.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //移動方向の入力情報がInputdirectionの中に入るようになる
        inputDirection = context.ReadValue<Vector2>();
        
    }
	
	private void OnCollisionEnter2D(Collision2D collision) //コリジョン（接触時）の関数
    {
        if (collision.gameObject.tag == "Floor") 
        {
            bjump = false;
        
        }
		if(collision.gameObject.tag == "Enemy"){
			
			HitEnemy(collision.gameObject);
			gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
			
		}
    }
	
	public void Onjump(InputAction.CallbackContext context) //ジャンプ関数
    {
        if (!context.performed || bjump) 
        {
            return;
        }

        rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
		bjump = true;
    
    }
	
	public void Dead() 
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
	
	private void HitEnemy(GameObject enemy) //エネミーを討伐
	{
		float halfScaleY = transform.localScale.y/2.0f;
        float enemyHalfScaleY = transform.localScale.y / 2.0f;
        if (transform.position.y - (halfScaleY - 0.1f) >= enemy.transform.position.y + (enemyHalfScaleY - 0.1f)) 
        {
            Destroy(enemy); //エネミー消滅
        
        }
		else{
			enemy.GetComponent<Enemy>().PlayerDamage(this); //被ダメージ
			StartCoroutine(Damage());
        }
	}
	
	IEnumerator Damage()
	{
		Color color = spriteRenderer.color;
		for(int i = 0; i < damageTime; i++)
		{
			yield return new WaitForSeconds(flashTime); //flashTime時間待ってくれる
			spriteRenderer.color = new Color(color.r,color.g,color.b,0.0f); //消す
			
			yield return new WaitForSeconds(flashTime); //flashTime時間待ってくれる
			spriteRenderer.color = new Color(color.r,color.g,color.b,1.0f); //映す
		}
		spriteRenderer.color = color;
		gameObject.layer = LayerMask.NameToLayer("Default");
	}
	
	public void Damage(int damage) //被ダメージ
    {
        hp= Mathf.Max(hp - damage,0);
		Dead(); //Damage関数で実行する
    }
	
	public int GetHP() 
    {
        return hp;
    }
	
	


	
}