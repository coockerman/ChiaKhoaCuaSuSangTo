using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepHandler : MonoBehaviour
{
    public void OnFootstep()
    {
        AudioController.instance.OnAudioStepPlayer();
    }
    public void OnLand()
    {

    }
}

