using UnityEngine;

public class CheckInteractable : MonoBehaviour
{
    [SerializeField] private float maxRange = 5.0f;
    [SerializeField] private GameObject interactText;
    IInteractable currentInteractableHitObject;

    [SerializeField] private GameObject useText;
    IUsable currentUseableHitObject;

    void Update()
    {
        CheckInteract();
        CheckUse();
    }

    private void CheckInteract()
    {
        Vector3 origine = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        RaycastHit hit;



        if (Physics.Raycast(origine, direction, out hit, maxRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (currentInteractableHitObject != null && interactable != currentInteractableHitObject)
            {

                interactText.SetActive(false);
            }
            else
            {
                currentInteractableHitObject = interactable;
            }
            if (interactable != null && interactable.CanInteract())
            {

                interactText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact();
                }

            }
        }
        else
        {
            if (currentInteractableHitObject != null)
            {

                interactText.SetActive(false);
                currentInteractableHitObject = null;
            }
        }
    }
    private void CheckUse()
    {
        Vector3 origine = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;

        RaycastHit hit;



        if (Physics.Raycast(origine, direction, out hit, maxRange))
        {
            IUsable usable = hit.collider.GetComponent<IUsable>();
            if (currentUseableHitObject != null && usable != currentUseableHitObject)
            {

                useText.SetActive(false);
            }
            else
            {
                currentUseableHitObject = usable;
            }
            if (usable != null && usable.CanUse())
            {

                useText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    usable.Use();
                }

            }
        }
        else
        {
            if (currentUseableHitObject != null)
            {

                useText.SetActive(false);
                currentUseableHitObject = null;
            }
        }
    }
}
