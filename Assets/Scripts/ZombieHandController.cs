using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ZombieHandController : MonoBehaviour
{
    [SerializeField] Transform zombieHand;
    Animator zombieHandAnimator;

    [SerializeField] XRBaseInteractor hand;
    [SerializeField] InputActionProperty triggerAction;
    [SerializeField] InputActionProperty primaryAction;

    void Start()
    {
        zombieHandAnimator = zombieHand.GetComponent<Animator>();

        hand.selectEntered.AddListener(OnGrabEnter);
        hand.selectExited.AddListener(OnGrabExit);
    }

    void OnGrabEnter(SelectEnterEventArgs args)
    {
        if(args.interactorObject != (IXRSelectInteractor)hand)
        {
            return;
        }

        zombieHandAnimator.CrossFadeInFixedTime("grab_pose", 0.25f);
    }

    void OnGrabExit(SelectExitEventArgs args)
    {
        if (args.interactorObject != (IXRSelectInteractor)hand)
        {
            return;
        }

        zombieHandAnimator.CrossFadeInFixedTime("default_pose", 0.25f);
    }

    void Update()
    {
        AnimatorStateInfo current = zombieHandAnimator.GetCurrentAnimatorStateInfo(0);

        if(triggerAction.action.WasPressedThisFrame())
        {
            zombieHandAnimator.CrossFadeInFixedTime("peace_peace", 0.25f);
        }
        else if (triggerAction.action.WasReleasedThisFrame())
        {
            zombieHandAnimator.CrossFadeInFixedTime("default_pose", 0.25f);
        }

        if (primaryAction.action.WasPressedThisFrame())
        {
            zombieHandAnimator.CrossFadeInFixedTime("point_pose", 0.25f);
        }
        else if (primaryAction.action.WasReleasedThisFrame())
        {
            zombieHandAnimator.CrossFadeInFixedTime("default_pose", 0.25f);
        }
    }

    void ResetAllTriggers()
    {
        foreach(var param in zombieHandAnimator.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Trigger)
            {
                zombieHandAnimator.ResetTrigger(param.name);
            }
        }
    }
}
