using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    //IsometricCharacterRenderer isoRenderer;

	private float horizontalInput;
	private float verticalInput;

    Rigidbody2D rbody;

    /* BLINK VARIABLES */
	public int blinkCount;
	public GameObject blinkButton;
	public float blinkRange;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        //isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 currentPos = rbody.position;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;

        rbody.MovePosition(newPos);
        
        // TODO: Display range indicator with a radius value of blinkRange.
        if (Input.GetButtonDown("Blink"))
        {
		}

        // Perform a teleport.
        if (Input.GetButtonUp("Blink") && blinkCount > 0)
        {
            Blink();  
		}
    }

    // TODO: Make it so the player cannot blink into colliders and can only blink a certain distance from their current position.
    public void Blink()
    {
        Vector3 mousePosition = Input.mousePosition;
        float distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(mousePosition));
        Debug.Log("Distance: " + distance);

        // TODO: Perform blink to maximum distance. 
        if (distance > blinkRange)
        {
            Debug.Log("Out of range.");
		}
        // Perform blink to mouse position.
        else
        {
            transform.position = Camera.main.ScreenToWorldPoint(mousePosition + new Vector3(0, 0, 4f));
            blinkCount -= 1;
            Debug.Log("Remaining Blinks: " + blinkCount);
		}
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blinkRange);
	}

	public float GetHorizontal()
	{
		return horizontalInput;
	}

	public float GetVertical()
	{
		return verticalInput;
	}

    public float GetBlinkCount()
    {
        return blinkCount;
	}
}
