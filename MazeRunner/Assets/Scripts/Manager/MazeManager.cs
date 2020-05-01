using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Manager.Entity.NodeObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Manager
{
    //URDL 순서

    public class MazeManager : MonoBehaviour
    {
        public static int MaximumTurn = 10000000;
        public Vector2Int LastStart => start;
        public Vector2Int LastEnd => end;
        private Node[,] nodes;
        private Vector2Int start, end, size;
        private HashSet<Vector2Int> empties;
        private MainSceneManager Manager;
        [SerializeField] private bool showGizmos = false;
        [SerializeField] private NodeRenderer rendererPrefab;

        private List<NodeRenderer> Renderers;
        public void GenerateMap(Vector2Int size, Vector2Int startNode, Vector2Int endNode)
        {
            foreach (NodeRenderer t in Renderers)
            {
                Destroy(t.gameObject);
            }
            Renderers.Clear();
            this.size = size;
            nodes = new Node[size.x, size.y];
            for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                nodes[i, j] = new Node() {X = i, Y = j};
                if (i > 0)
                {
                    nodes[i, j].Neighbor[3] = nodes[i - 1, j];
                    nodes[i - 1, j].Neighbor[1] = nodes[i, j];
                }

                if (j > 0)
                {
                    nodes[i, j].Neighbor[0] = nodes[i, j - 1];
                    nodes[i, j - 1].Neighbor[2] = nodes[i, j];
                }
            }

            Node ends = nodes[endNode.x, endNode.y];
            ends.Generated = true;

            empties = new HashSet<Vector2Int>();

            for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                if (endNode.x != i || endNode.y != j)
                    empties.Add(new Vector2Int(i, j));
            int counterA = 0, counterB = 0, counterC = 0;
            while (empties.Count > 0 && counterA++ < MaximumTurn)
            {
                Vector2Int initialCoord = empties.ElementAt((int) (Random.value * empties.Count));
                Node nowNode = nodes[initialCoord.x, initialCoord.y];
                counterB = 0;
                while (!nowNode.Generated && counterB++ < MaximumTurn)
                {
                    int nextDir = (int) (Random.value * 4);
                    counterC = 0;
                    while (nowNode.Neighbor[nextDir] == null && counterC++ < MaximumTurn)
                    {
                        nextDir = (nextDir + 1) % 4;
                    }

                    nowNode.Neighbor[nextDir].LastParent = nowNode;
                    nowNode = nowNode.Neighbor[nextDir];
                }

                counterB = 0;
                do
                {
                    nowNode.Generated = true;
                    empties.Remove(new Vector2Int(nowNode.X, nowNode.Y));
                    if (nowNode.LastParent == null) break;
                    for (int i = 0; i < nowNode.Neighbor.Length; i++)
                    {
                        if (nowNode.Neighbor[i] == nowNode.LastParent)
                        {
                            nowNode.Wall[i] = false;
                            nowNode.LastParent.Wall[(i + 2) % 4] = false;
                        }
                    }

                    nowNode = nowNode.LastParent;
                } while (!nowNode.Generated && counterB++ < MaximumTurn);
            }

            foreach (Node t in nodes)
            {
                Renderers.Add(Instantiate(rendererPrefab, transform).Initialize(Manager, t));
            }
        }

        public void OnDrawGizmos()
        {
            if (showGizmos)
            {
                Gizmos.color = Color.gray;
                if (nodes != null)
                    foreach (Node t in nodes)
                    {
                        
                        for (int i = 0; i < 4; i++)
                        {
                            if (t.Wall[i])
                            {
                                Gizmos.DrawCube(t.NodePos + new Vector3(Node.Xoffset[i], 1, Node.Yoffset[i]) * .45f,
                                        new Vector3(Node.XwallSize[i], 2, Node.YwallSize[i]));
                            }
                        }
                    }

                Gizmos.color = Color.red;
                if (empties != null)
                {
                    foreach (Vector2Int t in empties)
                    {
                        Gizmos.DrawWireCube(new Vector2(t.x, t.y), Vector3.one * .75f);
                    }
                }
            }
        }

        public void Initialize(MainSceneManager mainSceneManager)
        {
            Manager = mainSceneManager;
            Renderers=new List<NodeRenderer>();
        }
    }
}