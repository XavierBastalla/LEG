using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //in-game text when looking at interactable
    public string promptMessage;
    
    //called from player
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //override function for subclass
    }
}
