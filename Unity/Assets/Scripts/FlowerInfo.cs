using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInfo : MonoBehaviour
{
    // Basic Text Object (3d).
    public TextMesh Text;

    public void SetText(string text)
    {
        Text.text = text;
    }
}
