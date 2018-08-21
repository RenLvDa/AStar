using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindPath : MonoBehaviour {
	public Transform player, endPoint;
	private Grid _grid;


	void Awake () {
		_grid = GetComponent<Grid> ();
	}

	void Update () {
		
	}

	private void findingPath(Vector3 startPos,Vector3 endPos){
		Node startNode = _grid.GetFromPosition (startPos);
		Node endNode = _grid.GetFromPosition (endPos);


		List<Node> openList = new List<Node> ();
		List<Node> closeList = new HashSet<Node> ();
		openList.Add (startNode);

		while (openList.Count > 0) {
			Node currentNode = openList [0];
			for (int i = 0; i < openList.Count; i++) {
				if (openList [i].fCost < currentNode.fCost ||
				   openList [i].fCost == currentNode.fCost && openList [i].hCost < currentNode.hCost) 
				{
					currentNode = openList [i];
				}
			}

			openList.Remove (currentNode);
			closeList.Add (currentNode);

			if (currentNode == endNode) {
				//生成寻路
				return;
			}

			foreach (var node in _grid.GetNeibourhood(currentNode)) {
				if (!node.walkable || closeList.Contains (node))
					continue;


			}
		}
	}
}
