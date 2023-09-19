using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[DisallowMultipleComponent]
public class GameObjectPickup : MonoBehaviour
{
    [SerializeField]
    private XRGrabInteractable XRGrab;
    [SerializeField]
    private Collider[] Colliders;

    private void Start()
    {
        XRGrab = GetComponent<XRGrabInteractable>();
        Colliders = GetComponentsInChildren<Collider>();

        XRGrab.selectEntered.AddListener(Grab);
        XRGrab.selectExited.AddListener(Drop);
    }

    public void Grab(SelectEnterEventArgs args)
    {
        foreach (Collider collider in Colliders)
        {
            Debug.Log("Collider set");
            collider.isTrigger = true;
        }
    }

    public void Drop(SelectExitEventArgs args)
    {
        foreach (Collider collider in Colliders)
        {
            Debug.Log("Collider set");
            collider.isTrigger = false;
        }

    }
}
