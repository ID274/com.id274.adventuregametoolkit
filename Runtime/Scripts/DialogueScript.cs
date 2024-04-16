using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject dialogueMenu;
    [SerializeField] private Image speakerSprite; // The image for the speaker, in the toolkit it will be the image for the NPC
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private TextMeshProUGUI speakerDialogueText;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button choice1Button;
    [SerializeField] private Button choice2Button;

    [Header("References")]
    [SerializeField] private QuestNPC questNPC;

    [Header("Attributes")]
    public int dialogueID;
    public string dialogueText;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;

    [Header("Show Text Attributes")]
    [SerializeField] private string currentText = "";
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float delayStart; // This bit is used to delay the next text so that the text effect does not overlap


    [Header("Dialogue Contents")]
    [SerializeField] private string[] dialogueContent; // Dialogue content refers to the actual text the NPC communicates to the player
    [SerializeField] private string[] dialogueChoice1; // Dialoguechoice1 and 2 refer to the options available to the player in the dialogue menu
    [SerializeField] private string[] dialogueChoice2;



    private void OnEnable()
    {
        delayStart = delay;
        dialogueText = dialogueContent[dialogueID];
        StartCoroutine(ShowText());
    }

    private void Update()
    {
        if (dialogueID == 0)
        {
            speakerNameText.text = "[QUEST NPC]";
        }
    }

    IEnumerator ShowText()
    {
        // This code creates an effect where the text appears letter by letter and uses the "delay" variable for the delay between letters
        choice1Text.text = dialogueChoice1[dialogueID];
        choice2Text.text = dialogueChoice2[dialogueID];
        for (int i = 0; i < dialogueText.Length + 1; i++)
        {
            currentText = dialogueText.Substring(0, i);
            speakerDialogueText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }

    public void OnChoiceOne()
    {
        if (dialogueID == 0 && QuestManager.Instance.currentQuestID == 0)
        {
            questNPC.talked = true; // Sets the quest as accepted to let the questmanager know to set the corresponding quest as active
            delay = delayStart;
            dialogueMenu.SetActive(false);
        }
        else if (dialogueID == 1)
        {
            delay = delayStart;
            dialogueMenu.SetActive(false);
            QuestManager.Instance.EndQuest();
        }
        else if (dialogueID == 2)
        {
            delay = delayStart;
            dialogueMenu.SetActive(false);
        }
    }
    public void OnChoiceTwo()
    {
        if (dialogueID == 0 && QuestManager.Instance.currentQuestID == 0)
        {
            delay = delayStart; // The questNPC.talked is not set to true here as this option will let the player back out of the dialogue without accepting the next quest
            dialogueMenu.SetActive(false);
        }
        else if (dialogueID == 1)
        {
            delay = delayStart;
            dialogueMenu.SetActive(false);
            QuestManager.Instance.EndQuest();
        }
    }

    public void OnNextButton()
    {
        delay = 0.01f; // This lets the text "animation" speed up when the Next button is pressed. In essence: speeds up the dialogue.
    }

    public void OnCloseButton()
    {
        delay = delayStart;
        dialogueMenu.SetActive(false);
    }

    public void OnQuestComplete()
    {
        // The code below progresses the dialogue for the next quest
        dialogueID++;
        choice1Text.text = dialogueChoice1[dialogueID];
        choice2Text.text = dialogueChoice2[dialogueID];
        dialogueText = dialogueContent[dialogueID];
    }

}
