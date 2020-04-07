using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string hover, freshText, usedText;
    public bool fresh, cooldown;

    public int currentCount, freshTextCount, usedTextCount;
    // Start is called before the first frame update
    void Start()
    {
        currentCount = 120;
        fresh = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown)
        {
            currentCount--;
            if (currentCount <= 0)
            {
                cooldown = false;
                currentCount = 120;
            }
        }
    }

    public void ShowActionName()
    {
        
    }

    public void InteractWithMe()
    {
        
    }
}
