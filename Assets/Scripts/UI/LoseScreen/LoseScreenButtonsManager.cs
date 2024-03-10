using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voodoo.Utils;

public class LoseScreenButtonsManager : MonoBehaviour
{
    public void HomeButton(){
        Vibrations.Haptic(HapticTypes.MediumImpact);
        Debug.Log("OM Home");
    }

    public void RetryButton(){
        Vibrations.Haptic(HapticTypes.MediumImpact);
        Debug.Log("OM Retry");
    }
}
