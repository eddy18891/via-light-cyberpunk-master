using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;      // The actual text box.
    public GameObject dialogueText;
    public TextMeshProUGUI textDisplay; // The actual text.
    public string[] sentences;          // An array holding the many sentences we might scroll through.
    private int index;                  // Our current index in the scrollable array of sentences.
    public float typingSpeed;           // Processing speed of text appearing on the screen.

    public bool playerInRange;          // Determinant of whether or not we are within range of the interactable object.
    private bool canNext;

    public GameObject continueButton;   // Button that lets us move to the next sentence.

    IEnumerator Type()
    {
        canNext = false;
        foreach (char letter in sentences[index].ToCharArray())
        {
            if (!playerInRange)
            {
                yield break;     
			}

            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
		}
	}

    public void NextSentence()
    {
        continueButton.SetActive(false);

        // Check we are not at the end of our dialogue.
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            // End of dialogue.
            textDisplay.text = "";  
            dialogueBox.SetActive(false);
            index = 0;
		}
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                // If text is already displaying and somebody is impatiently proceeding.
                // TODO: Does not really work, but the code breaks if removed altogether.
                if (dialogueBox.activeInHierarchy && continueButton.activeSelf == false)
                {
                    // StopCoroutine(Type());  // Stop typing anything.
                    // textDisplay.text = "";  // Clear the current display.
                    // textDisplay.text = sentences[index];    // Update the display.
			    } 
                else if (dialogueBox.activeInHierarchy && continueButton.activeSelf == true)
                {
                    NextSentence();
			    }
                // Startup dialogue.
                else
                {
                    dialogueBox.SetActive(true);
                    dialogueText.SetActive(true);
                    StartCoroutine(Type());
			    }
            }

            if (textDisplay.text == sentences[index])
            {
                canNext = true;
                continueButton.SetActive(true);  
		    }
        }
	}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset all the dialogue.
        if (collision.CompareTag("Player"))
        {
            textDisplay.text = "";
            playerInRange = false;
            dialogueBox.SetActive(false);
            dialogueText.SetActive(false);
            index = 0;
            continueButton.SetActive(false);
        }
	}
}
