using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
	public enum StateMachine
	{
		Idle, Walk, Run, Attack
	}

	public Transform player;
	private Animator anim;

	public float aggroDistance = 10f;
	public float aggroAngle = 30f; // Determines the field of vision cone size.

	private StateMachine movementState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
	
		Vector2 direction = player.position - this.transform.position;

        float angle = Vector2.Angle(direction, this.transform.forward);	// Foward position used in calculating the field of vision cone.

		// Check distance between player and AI entity positions.
		if (Vector2.Distance(player.position, this.transform.position) < aggroDistance && angle < aggroAngle)
		{
			/*
			FACING DIRECTIONS
			N = 0* | NE = 45* | E = 90* | SE = 135* | S = 180* | SW = 225* | W = 270* | NW = 315*
			*/
			
			// N
			if (direction.x == 0 && direction.y > 0)
			{
				transform.rotation = Quaternion.Euler(-30, 0, 0);
			}
			
			// NE
			if (direction.x > 0 && direction.y > 0)
			{
				transform.rotation = Quaternion.Euler(-15, 45, -15);
			}

			// E
			if (direction.x > 0 && direction.y == 0)
			{
				transform.rotation = Quaternion.Euler(0, 90, -30);
			}

			// SE
			if (direction.x > 0 && direction.y < 0)
			{
				transform.rotation = Quaternion.Euler(15, 135, -15);
			}

			// S
			if (direction.x == 0 && direction.y < 0)
			{
				transform.rotation = Quaternion.Euler(30, 180, 0);
			}

			// SW
			if (direction.x < 0 && direction.y < 0)
			{
				transform.rotation = Quaternion.Euler(15, 225, 15);
			}

			// W
			if (direction.x < 0 && direction.y == 0)
			{
				transform.rotation = Quaternion.Euler(0, 270, 30);
			}

			// NW
			if (direction.x < 0 && direction.y > 0)
			{
				transform.rotation = Quaternion.Euler(-15, 315, 15);
			}

			// Note: Forward facing direction should always be in the z-axis.
			if (direction.magnitude > 1)
			{
				SetMovementState(StateMachine.Walk);
				this.transform.Translate(Mathf.Clamp(direction.x, -0.02f, 0.02f), Mathf.Clamp(direction.y, -0.02f, 0.02f), 0, Space.World);
			}
			else
			{
				// Kill range.
				SetMovementState(StateMachine.Attack);
			}
		}
		// Player is outside of AI entity aggro range.
		else
		{
			SetMovementState(StateMachine.Idle);
		}

		// Set the animation state to whatever the current state machine input is.
		Animate();
    }

	private void SetMovementState(StateMachine newState)
	{
		// Base case.
		if (movementState == newState)
		{
			return;
		}

		// Set the current state to the new desired state.
		movementState = newState;
	}
	
	// Getter used if an outside script wants to modify the state.
	public StateMachine GetMovementState()
	{
		return movementState;
	}

	private void Animate()
	{
		anim.SetBool("IsRunning", movementState == StateMachine.Walk);
		anim.SetBool("IsAttacking", movementState == StateMachine.Attack);
	}

	// Displays the aggro range of AI entity for debugging purposes.
	// TODO: the current aggro range is determined as a cone and not a sphere.
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(this.transform.position, aggroDistance);
	}
}
