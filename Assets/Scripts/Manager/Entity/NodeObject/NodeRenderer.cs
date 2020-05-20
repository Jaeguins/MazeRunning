using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Manager.Entity.NodeObject {

    /// <summary>
    /// 칸 타입
    /// </summary>
    public enum NodeType {
        Normal,
        Start,
        End
    }

    /// <summary>
    /// 한칸에 대한 오브젝트 스크립트
    /// </summary>
    public class NodeRenderer : MonoBehaviour {
        /// <summary>
        /// 개념적 한칸
        /// </summary>
        [NonSerialized] public Node Manager;
        /// <summary>
        /// 관리자
        /// </summary>
        private MainSceneManager SceneManager;
        [Header("벽들")] public WallRenderer[] Walls;

        public NodeRenderer Initialize(MainSceneManager scene, Node parent, NodeType nodeType = NodeType.Normal) {
            SceneManager = scene;
            Manager = parent;
            transform.position = parent.NodePos;
            Walls = new WallRenderer[4];
            List<int> walls = new List<int>(4);//만들어질수 있는 벽 방향
            if (Manager.X == 0) walls.Add(3); //제일 왼쪽 열 : 테두리용 왼쪽 벽 추가
            if (Manager.Y == 0) walls.Add(0); //제일 아래쪽 행 : 테투리용 아래쪽 벽 추가
            walls.Add(1); //겹치기 방지를 위해 오른쪽과 위쪽만 관리
            walls.Add(2);

#region 이 칸에 할당된 벽 생성

            foreach (int i in walls) {
                if (Manager.Wall[i]) {//미로상으로 있어야되는 벽
                    Walls[i] = Instantiate(scene.GetWallPrefab(), transform).Initialize(this, i);
                } else {
                    if (Random.value < .03f) //미로상으론 길이지만 3% 확률로 퀴즈 배치
                    {
                        Quiz.Quiz quiz = Instantiate(scene.WallPoolManager.quizPrefabs[0], transform);
                        Walls[i] = quiz.GetComponent<WallRenderer>().Initialize(this, i);
                        quiz.Initialize(scene);
                    }
                }
            }



            switch (nodeType) {
                case NodeType.Start:
                    Instantiate(SceneManager.WallPoolManager.StartPrefab, transform);//시작점 장식물 추가
                    break;
                case NodeType.End:
                    Instantiate(SceneManager.WallPoolManager.EndPrefab, transform);//도착점 장식물 추가
                    break;
            }
#endregion
            return this;
        }
    }

}