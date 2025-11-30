using UnityEngine;
using Yarn.Unity;

public class InteractionArea_Conversation : MonoBehaviour
{
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private string _conversationName;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter" + other.gameObject.name);
        if (other.tag == "Player")
        {
            _dialogueRunner.StartDialogue(_conversationName);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit" + other.gameObject.name);
        if (other.tag == "Player")
        {
            _dialogueRunner.Stop();
        }
    }
}