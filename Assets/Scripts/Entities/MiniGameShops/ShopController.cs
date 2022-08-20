using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private bool isInitialized;

    public void InitShopping()
    {
        if (!isInitialized)
        {
            isInitialized = true;
            PlayShopDialogue();
        }

        ShowMiniGame();
    }

    private void PlayShopDialogue()
    {
        Debug.Log($"1. Play Dialogue");
    }

    private void ShowMiniGame()
    {
        Debug.Log($"2. Show minigame");
    }
}
