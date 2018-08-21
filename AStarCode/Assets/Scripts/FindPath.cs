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
		findingPath (player.position, endPoint.position);
	}

	private void findingPath(Vector3 startPos,Vector3 endPos){
		Node startNode = _grid.GetFromPosition (startPos);
		Node endNode = _grid.GetFromPosition (endPos);

		List<Node> openList = new List<Node> ();
		HashSet<Node> closeList = new HashSet<Node> ();
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
				generatePath(startNode,endNode);
				return;
			}

			foreach (var node in _grid.GetNeibourhood(currentNode)) {
				if (!node.walkable || closeList.Contains (node))
					continue;

				int newCont = currentNode.gCost + getDistanceNodes (currentNode, node);
				if (newCont < node.gCost || !openList.Contains (node)) {
					node.gCost = newCont;
					node.hCost = getDistanceNodes (node, endNode);
					node.parent = currentNode;

					if (!openList.Contains (node)) {
						openList.Add (node);
					}
				}

			}
		}
	}

	private int getDistanceNodes(Node a,Node b){
		int cntX = Mathf.Abs (a.gridX - b.gridX);
		int cntY = Mathf.Abs (a.gridY - b.gridY);

		if (cntX >= cntY)
			return 14 * cntY + 10 * (cntX - cntY);
		else
			return 14 * cntX + 10 * (cntY - cntX);
	}

	private void generatePath(Node startNode,Node endNode){
		List<Node> path = new List<Node> ();
		Node temp = endNode;

		while (temp != startNode) {
			path.Add (temp);
			temp = temp.parent;
		}

		path.Reverse ();
		_grid.path = path;
	}
}
