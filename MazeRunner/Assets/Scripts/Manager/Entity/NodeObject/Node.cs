using UnityEngine;

namespace Scripts.Manager.Entity.NodeObject
{
    public class Node
    {
        public Node[] Neighbor = new Node[4];
        public bool Generated = false;
        public bool[] Wall = {true, true, true, true};
        public int X, Y;
        public static int[] Xoffset = {0, 1, 0, -1};
        public static int[] Yoffset = {-1, 0, 1, 0};
        public static float[] YwallSize = {.1f, 1, .1f, 1};
        public static float[] XwallSize = {1, .1f, 1, .1f};
        public Node LastParent = null;
        public Vector3 NodePos => new Vector3(X, 0, Y);
    }
}