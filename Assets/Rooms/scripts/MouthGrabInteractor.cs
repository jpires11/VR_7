using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MouthGrabInteractor : UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor
{
    public Transform mouthAttachTransform;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (mouthAttachTransform != null)
        {
            attachTransform = mouthAttachTransform;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);

        // Reset attachTransform back to default
        attachTransform = null;
    }
}
