using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineService : MonoBehaviour
{
  private int _id;
  private Color _color;
  public void GetDotData(CircleDatas data)
  {
    _id = data.Id;
    _color = data.Color;
  }
  private void OnTriggerEnter2D(Collider2D collision)
  {
    switch (collision.tag)
    {
      case "Line":
        Debug.Log("Trigger Line : " + collision.name);
        break;
      case "Circle":
        Debug.Log("Trigger Circle : " + collision.name);
        break;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
      Debug.Log("Trigger Line : " + collision.collider.name);
  }
}
