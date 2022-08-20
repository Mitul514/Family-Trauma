using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    [SerializeField] private TextMeshPro m_NumText;
    [SerializeField] private Animator m_boxRevealAnim;

    public int CurrVal;
    public bool isLocked;

    public void SetData(int val)
    {
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
}
