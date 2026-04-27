using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName = "Bob";

    public void Interact()
    {
        Debug.Log(npcName + ": Get that bucket");
    }
}