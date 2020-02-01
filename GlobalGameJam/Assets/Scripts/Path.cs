using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class Path : MonoBehaviour
{
   public Color lineColor;
   public List<Transform> nodes = new List<Transform>();

   private void OnDrawGizmos()
   {
      Gizmos.color = lineColor;
      Transform[] pathTransforms = GetComponentsInChildren<Transform>();
      nodes = new List<Transform>();

      foreach (var t in pathTransforms)
      {
         if (t != transform)
         {
            nodes.Add(t);
         }
      }

      for (int i = 0; i < nodes.Count; i++)
      {
         Vector3 currentNode = nodes[i].position;
         Vector3 previousNode = Vector3.zero;
         if (i > 0)
         {
            previousNode = nodes[i - 1].position;
         }
         else if(i == 0 && nodes.Count > 1)
         {
            previousNode = nodes[nodes.Count - 1].position;
         }
         Gizmos.DrawLine(previousNode, currentNode);
         Gizmos.DrawSphere(currentNode, 0.3f);
      }
   }
}
