using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepHandler : MonoBehaviour
{
    public AudioClip step;

    public void OnFootstep()
    {
        AudioController.instance.SoundPlayer.PlayOneShot(step);
    }
    public void OnLand()
    {

    }
}

