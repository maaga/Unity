using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField, Header("移動速度")]
    private float movespeed;
    private Rigidbody2D rigidBody;
	
	
	[SerializeField, Header("攻撃力")]
    private int attack;

	
    // Start is called before the first frame update
    void Start()
    {
        rigidBody= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
	
	private void Move() 
    {
        rigidBody.velocity = new Vector2(Vector2.left.x * movespeed, rigidBody.velocity.y);
    
    }
	
	public void PlayerDamage(Player player) 
    {
        player.Damage(attack);
    }
}
