using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Defines the <see cref="PlayerService" />.
/// </summary>
public class PlayerService : MonoBehaviour
{
  [SerializeField] private GameObject _linePrefab;

  [SerializeField] private LayerMask _cirlceLayer;

  [SerializeField] private LayerMask _lineLayer;

  [SerializeField] private LayerMask _borderLayer;

  [SerializeField] private float colliderRad = 0f;

  private LineRenderer lineRenderer;

  private EdgeCollider2D edgeCollider;

  private GameObject currentLine;

  private List<Vector2> fingerpositions;

  private bool isMatchFound;

  private bool createLine;

  private string _name;

  private int _id;

  /// <summary>
  /// The SetData.
  /// </summary>
  /// <param name="data">The data<see cref="CircleDatas"/>.</param>
  public void SetData(CircleDatas data)
  {
    _id = data.Id;
    _name = data.Name;
    //_linePrefab.GetComponent<LineService>().GetDotData(data);
  }

  private void Start()
  {
    fingerpositions = new List<Vector2>();
  }

  private void Update()
  {
    if (createLine)
    {
      if (Input.GetMouseButtonDown(0))
      {
        CreateLine();
      }
      if (Input.GetMouseButton(0))
      {
        Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var distanceX = Vector2.Distance(new Vector2(tempFingerPos.x, 0), new Vector2(fingerpositions.Last().x, 0));
        var distanceY = Vector2.Distance(new Vector2(0, tempFingerPos.y), new Vector2(0, fingerpositions.Last().y));

        if (Mathf.Abs(distanceX) > 0.3f)          //moveHor
          UpdateLine(new Vector2(tempFingerPos.x, fingerpositions.Last().y));
        else if (Mathf.Abs(distanceY) > 0.3f)     //moveVer
          UpdateLine(new Vector2(fingerpositions.Last().x, tempFingerPos.y));
      }
    }
    if (Input.GetMouseButtonUp(0))
    {
      if (!isMatchFound)
        Destroy(currentLine);
    }
  }

  /// <summary>
  /// The CreateLine.
  /// </summary>
  private void CreateLine()
  {
    currentLine = Instantiate(_linePrefab);
    currentLine.transform.parent = transform;
    lineRenderer = currentLine.GetComponent<LineRenderer>();
    edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
    fingerpositions.Clear();

    fingerpositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    fingerpositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));

    lineRenderer.SetPosition(0, fingerpositions[0]);
    lineRenderer.SetPosition(1, fingerpositions[1]);

    lineRenderer.startColor = GetComponent<SpriteRenderer>().color;
    lineRenderer.endColor = GetComponent<SpriteRenderer>().color;

    edgeCollider.points = fingerpositions.ToArray();
    ConnectingDotsManager.Instance.MoveCount--;
  }

  /// <summary>
  /// The UpdateLine.
  /// </summary>
  /// <param name="newFingerPos">The newFingerPos<see cref="Vector2"/>.</param>
  private void UpdateLine(Vector2 newFingerPos)
  {
    fingerpositions.Add(newFingerPos);

    if (lineRenderer != null)
    {
      lineRenderer.positionCount++;
      lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPos);
      edgeCollider.points = fingerpositions.ToArray();

      #region Collision Check
      if (Physics2D.OverlapCircle(newFingerPos, colliderRad, _cirlceLayer))
      {
        var obj = Physics2D.OverlapCircle(newFingerPos, colliderRad, _cirlceLayer);
        var id = obj.gameObject.GetComponent<PlayerService>()._id;
        
        if (id == _id)
        {
          isMatchFound = true;
          ConnectingDotsManager.Instance.FlowMatchedCount++;
        }
      }
      else if (Physics2D.OverlapCircle(newFingerPos, 0.5f, _lineLayer))
      {
        var obj = Physics2D.OverlapCircle(newFingerPos, 0.5f, _lineLayer);
        var objColor = obj.GetComponent<LineRenderer>().startColor;
        if (lineRenderer.startColor != objColor)
        {
          ConnectingDotsManager.Instance.FlowMatchedCount--;
          Destroy(obj.gameObject);
        }
        isMatchFound = false;
      }
      else if (Physics2D.OverlapCircle(newFingerPos, colliderRad, _borderLayer))
      {
        var obj = Physics2D.OverlapCircle(newFingerPos, colliderRad, _borderLayer);
        Debug.Log(obj.name);
        Destroy(currentLine);
      }
      #endregion
    }
  }

  private void OnMouseDown()
  {
    Debug.Log("Hello");
    createLine = true;
  }

  private void OnMouseUp()
  {
    Debug.Log("Bye");

    createLine = false;
    if (ConnectingDotsManager.Instance.MoveCount == 0 || ConnectingDotsManager.Instance.FlowMatchedCount == 5)
    {
      ConnectingDotsManager.Instance.SetState(GameStates.GameOverState);
    }
  }
}
