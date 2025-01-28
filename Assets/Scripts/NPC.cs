using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, Interactable
{
    [SerializeField, TextArea(1, 5)] private string[] phrases;
    [SerializeField] private float timeBetweenLetters;
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameManagerSO gameManager;

    private bool talking;
    private int indexPhrase = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Interact()
    {
        gameManager.ChangePlayerState(false);
        dialogBox.SetActive(true);
        if (!talking)
        {
            NextPhrase();
        }
        else
        {
            CompletePhrase();
        }
    }

    IEnumerator WriteLetter()
    {
        talking = true;
        dialogText.text = "";
        char[] characters = phrases[indexPhrase].ToCharArray();
        foreach (char character in characters)
        {
            dialogText.text += character.ToString();
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        talking = false;
    }

    private void CompletePhrase()
    {
        StopAllCoroutines();
        dialogText.text = phrases[indexPhrase];
        talking = false;
    }

    private void NextPhrase()
    {
        indexPhrase++;
        if (indexPhrase >= phrases.Length)
        {
            FinishDialog();
        }
        else
        {
            StartCoroutine(WriteLetter());
        }
    }

    private void FinishDialog()
    {
        talking = false;
        dialogText.text = "";
        indexPhrase = -1;
        dialogBox.SetActive(false);
        gameManager.ChangePlayerState(true);
    }
}
