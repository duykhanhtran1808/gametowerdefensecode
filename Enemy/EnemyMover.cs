using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    List<Node> path = new List<Node>();
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
       
    }

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoord;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        //GameObject waypointsParent = GameObject.FindGameObjectWithTag("Path");
        //foreach (Transform waypoint in waypointsParent.transform)
        //{
        //    if(waypoint != null)
        //        path.Add(waypoint.GetComponent<Tile>());
        //}
        StartCoroutine(FollowPath());
    }
    void ReturnToStart()
    {
        //transform.position = path[0].transform.position;
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoord);

    }
    IEnumerator FollowPath()
    {
        for(int i = 0; i< path.Count; i++) 
        {
            Vector3 startPos = this.transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            transform.LookAt(endPos);
            this.transform.GetChild(1).transform.rotation = Quaternion.Euler(90f, 0f, 0f); //Health Text
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return null;

            }
            //this.transform.position = w.transform.position;
            //yield return new WaitForSeconds(waitTime);
        }
        FinishPath();

    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
