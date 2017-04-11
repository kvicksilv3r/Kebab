using UnityEngine;
using System.Collections;

public class control_script : MonoBehaviour
{

	Animator anim;
	bool boolper, boolper2, boolper3;
	PlayerScript player;


	void Awake()
	{
		anim = GetComponentInChildren<Animator>();
		player = transform.root.GetComponent<PlayerScript>();
	}

	public void Walk()
	{

		boolper = anim.GetBool("isWalk");

		if (!boolper && player.JumpStates()[0])
		{
			anim.SetBool("isWalk", !boolper);
			anim.SetBool("isRun", false);
			anim.SetBool("isAnother", false);
			anim.SetBool("Attack", false);
			anim.SetBool("LowKick", false);
			anim.SetBool("isDeath", false);
			anim.SetBool("isDeath2", false);
			anim.SetBool("HitStrike", false);
		}
	}

	public void Run()
	{

		boolper2 = anim.GetBool("isRun");

		if (!boolper2 && player.JumpStates()[0])
		{
			anim.SetBool("isRun", !boolper2);
			anim.SetBool("isWalk", false);
			anim.SetBool("isAnother", false);
			anim.SetBool("Attack", false);
			anim.SetBool("LowKick", false);
			anim.SetBool("isDeath", false);
			anim.SetBool("isDeath2", false);
			anim.SetBool("HitStrike", false);
		}
	}

	public void OtherIdle()
	{
		boolper3 = anim.GetBool("isAnother");
		if (!boolper3)
		{
			anim.SetBool("isAnother", !boolper3);
			anim.SetBool("isWalk", false);
			anim.SetBool("isRun", false);
			anim.SetBool("Attack", false);
			anim.SetBool("LowKick", false);
			anim.SetBool("isDeath", false);
			anim.SetBool("isDeath2", false);
			anim.SetBool("HitStrike", false);
		}
	}
	public void Attack()
	{
		anim.SetBool("Attack", true);
	}

	public void LowKick()
	{
		anim.SetBool("LowKick", true);
	}

	public void Death()
	{
		anim.SetBool("isDeath", true);
	}
	public void Death2()
	{
		anim.SetBool("isDeath2", true);
	}
	public void Strike()
	{
		anim.SetBool("HitStrike", true);
	}

	public void Damage()
	{
		anim.SetTrigger("Jump");
	}

	public void MovementAnimation(float vectorMagnitude)
	{
		if (vectorMagnitude > 0.5)
		{
			Run();
		}
		else
		{
			Walk();
		}
	}

	void Update()
	{

	}
}
