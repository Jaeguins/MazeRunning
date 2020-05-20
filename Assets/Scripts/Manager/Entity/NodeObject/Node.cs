using UnityEngine;

namespace Scripts.Manager.Entity.NodeObject
{
    /// <summary>
    /// 개념적 한칸
    /// </summary>
    public class Node
    {
        /// <summary>
        /// 이웃 칸
        /// </summary>
        public Node[] Neighbor = new Node[4];
        /// <summary>
        /// 생성되었는지 여부
        /// </summary>
        public bool Generated = false;
        /// <summary>
        /// 벽 존재여부
        /// </summary>
        public bool[] Wall = {true, true, true, true};
        /// <summary>
        /// 위치
        /// </summary>
        public int X, Y;
        /// <summary>
        /// 방향에 따른 X 차이
        /// </summary>
        public static int[] Xoffset = {0, 1, 0, -1};
        /// <summary>
        /// 방향에 따른 Y 차이
        /// </summary>
        public static int[] Yoffset = {-1, 0, 1, 0};
        /// <summary>
        /// 미로 생성 중 이 칸에 도달한 칸
        /// </summary>
        public Node LastParent = null;
        /// <summary>
        /// 칸의 월드상 좌표
        /// </summary>
        public Vector3 NodePos => new Vector3(X, 0, Y);
    }
}