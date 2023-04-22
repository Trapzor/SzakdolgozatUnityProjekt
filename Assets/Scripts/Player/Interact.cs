using System.Collections.Generic;
using ObjectScripts;
using UnityEngine;

namespace Player
{
    public class Interact : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> interactables = new();
        private Interactable currentInteractionObject;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && currentInteractionObject != null)
                currentInteractionObject.Interact();
        }

        private void FixedUpdate()
        {
            if (interactables.Count >= 1)
            {
                float distance = 15;
                for (int i = 0; i < interactables.Count; i++)
                {
                    float newDistance = Vector3.Distance(interactables[i].transform.position, transform.position);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        if(currentInteractionObject != null)
                            currentInteractionObject.HidePrompt();
                        currentInteractionObject = interactables[i].GetComponent<Interactable>();
                        currentInteractionObject.ShowPrompt();
                    }
                }
            }
            else
            {
                if(currentInteractionObject != null)
                    currentInteractionObject.HidePrompt();
                currentInteractionObject = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent(typeof(Interactable)))
                interactables.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent(typeof(Interactable)))
            {
                GameObject toRemove = other.gameObject;
                if (interactables.Contains(toRemove))
                    interactables.Remove(toRemove); 
            }
        }
    }
}
