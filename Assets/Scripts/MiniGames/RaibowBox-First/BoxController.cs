using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoxController : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_NumText;
    [SerializeField] private Animator m_boxRevealAnim;

    public int CurrVal;
    public bool isLocked;

    public void SetData(int val, int[] mines)
    {
        int value = Array.FindIndex(mines, x => x.Equals(val));

        if (value != -1)
            m_NumText.text = "M";
        else
            m_NumText.text = val.ToString();

        CurrVal = val;
    }

    private void OnMouseDown()
    {
        if (isLocked)
            return;

        BoxManager.BMinstance.GetClickedObjects(this.gameObject);
    }

    public void RevealNumber()
    {
        if (isLocked)
            return;

        m_boxRevealAnim.SetTrigger("Reveal");
        isLocked = true;
        var nColor = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
        this.GetComponent<Renderer>().material.color = nColor;
    }

    public bool CheckIfBoxIsMine(int[] mines)
    {
        int value = Array.FindIndex(mines, x => x.Equals(CurrVal));

        if (value != -1)
        {
            return true;
        }

        return false;
    }
}
