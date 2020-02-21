using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
   public Color lineColor;
   public List<Transform> nodesTransform = new List<Transform>();
   [HideInInspector] public List<Node> nodes;

   private void Start()
   {
      InitNodes();
   }

   private void InitNodes()
   {
      nodes = new List<Node>();
      Node[] _nodes = GetComponentsInChildren<Node>();

      foreach (var n in _nodes)
      {
         nodes.Add(n);
      }
   }

   public int GetLastAchievedNode()
   {
      var lastAchievedNode = 0;
      
      foreach (var n in nodes)
      {
         if (n.carAchievedNode && n.nodeIndex >= lastAchievedNode) lastAchievedNode = n.nodeIndex;
      }

      return lastAchievedNode;
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = lineColor;
      Transform[] pathTransforms = GetComponentsInChildren<Transform>();
      nodesTransform = new List<Transform>();

      foreach (var t in pathTransforms)
      {
         if (t != transform)
         {
            nodesTransform.Add(t);
         }
      }

      for (int i = 0; i < nodesTransform.Count; i++)
      {
         Vector3 currentNode = nodesTransform[i].position;
         Vector3 previousNode = Vector3.zero;
         if (i > 0)
         {
            previousNode = nodesTransform[i - 1].position;
         }
         else if(i == 0 && nodesTransform.Count > 1)
         {
            previousNode = nodesTransform[nodesTransform.Count - 1].position;
         }
         Gizmos.DrawLine(previousNode, currentNode);
         Gizmos.DrawSphere(currentNode, 1f);
      }
   }
}
