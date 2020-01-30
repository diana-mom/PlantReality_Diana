using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    public TextMesh text;
    public int id;
    public float startPrice;
    public float minimumPrice;
    public float currentPrice;

    public bool MinimumReached { get; set; } = false;

    // Decrease the number, but not below the set Minimum.
    public void GoDown()
    {
        if (currentPrice > minimumPrice)
        {
            currentPrice--;
            text.text = currentPrice.ToString();
        }
        else
        {
            MinimumReached = true;
        }
    }

    // Change to new price.
    public void SetPrices(float min, float start)
    {
        minimumPrice = min;
        startPrice = start;
        currentPrice = start;

        MinimumReached = false;
    }

    // Reset the currentPrice to the startPrice.
    public void ResetCurrentPrice()
    {
        currentPrice = startPrice;
        MinimumReached = false;

        text.text = currentPrice.ToString();
    }




}
