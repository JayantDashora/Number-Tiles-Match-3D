using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Voodoo.Utils;

public class WinScreenButtonsManager : MonoBehaviour
{
    public void HomeButton(){
        Vibrations.Haptic(HapticTypes.MediumImpact);
        Debug.Log("OM Home");
    }

    public void NextLevelButton(){
        Vibrations.Haptic(HapticTypes.MediumImpact);
        Debug.Log("OM Next");
    }
}
