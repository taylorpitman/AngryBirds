using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image[] birdIcons;
    [SerializeField] private Color usedColor;

    public void UseShot(int shotNumber)
    {
        bool foundIndex = false;
        int index = 0;

        while(index < birdIcons.Length && !foundIndex)
        {
            if(shotNumber == index + 1)
            {
                birdIcons[index].color = usedColor;
            }
            index++;
        }
    }
}
