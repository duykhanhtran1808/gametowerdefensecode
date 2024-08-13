using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabaler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.blue;
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    //Waipoint waypoint;
    //GridManager System
    GridManager gridManager;



    int currentEditorSnap = 10;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = this.GetComponent<TextMeshPro>();
        label.enabled = false;
        //label.enabled = true;

        //waypoint = GetComponentInParent<Waipoint>();
        DisplayCoordinates();

    }
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        ColorCoordinates();
        ToggleLabels();
    }

    void ColorCoordinates()
    {
        //if(waypoint.IsPlaceable)
        //{
        //    label.color = defaultColor;
        //}
        //else
        //{
        //    label.color = blockColor;
        //}
        if (gridManager == null) return;
        Node currentNode = gridManager.GetNode(coordinates);
        if (currentNode == null) return;

        if (!currentNode.isWalkable)
        {
            label.color = blockColor;
        }
        else if(currentNode.isPath)
        {
            label.color = pathColor;
        }
        else if (currentNode.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }

    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / currentEditorSnap);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / currentEditorSnap);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
