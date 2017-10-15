using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public int RotationMultipler = 5;

    GameObject pathGO;
    Transform targetPathNode;

    
    int pathNodeIndex = 0;

	// Use this for initialization
	private void Start ()
    {
        pathGO = GameObject.Find("Path");	
	}

    // Update is called once per frame
    private void Update()
    {
        if (targetPathNode == null)
        {
            GetNextPathNode();
            if (targetPathNode == null) 
                return;
        }

        Vector3 dir = targetPathNode.transform.position - this.transform.localPosition;

        float frameDistance = speed * Time.deltaTime;

        if (dir.magnitude <= frameDistance)
        {
            targetPathNode = null;
        }
        else
        {
            transform.Translate(dir.normalized * frameDistance, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime*RotationMultipler);
        }
      
    }

    private void GetNextPathNode()
    {
        if(pathNodeIndex < pathGO.transform.childCount)
        {
            targetPathNode = pathGO.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;
        }
        else
        {
            //pathNodeIndex = 0; -> Creates loop ie travels in circle
            ReachedGoal();
        }
    }

    private void ReachedGoal()
    {
        Destroy(gameObject);

        // TODO: Take life from player.
    }

}
