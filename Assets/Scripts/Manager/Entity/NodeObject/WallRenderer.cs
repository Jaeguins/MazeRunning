using UnityEngine;

namespace Scripts.Manager.Entity.NodeObject {

    /// <summary>
    /// 벽 오브젝트 스크립트
    /// </summary>
    public class WallRenderer : MonoBehaviour {
        /// <summary>
        /// 부모가 되는 칸
        /// </summary>
        private NodeRenderer node;
        [Header("칸 중심과의 거리")] public float dist;
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="node">칸</param>
        /// <param name="dir">방향 - 시계방향</param>
        /// <returns></returns>
        public WallRenderer Initialize(NodeRenderer node, int dir) {
            this.node = node;
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.Euler(0, 90 * (dir + 1), 0);
            if (Random.value > .5f) transform.Rotate(0, 180, 0); //50%확률로 앞뒤 뒤집어서 등장
            transform.localPosition = new Vector3(Node.Xoffset[dir], 0, Node.Yoffset[dir]) * dist;
            return this;
        }
    }

}