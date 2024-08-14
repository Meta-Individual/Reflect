using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    public string objectID;
    public Transform npc;
    private NPCMonologue _npcMonologue;

    void Start()
    {
        _npcMonologue = FindObjectOfType<NPCMonologue>();
    }
    public void Interact()
    {
        _npcMonologue.ShowNPCMonologue(objectID, npc);
    }
}
