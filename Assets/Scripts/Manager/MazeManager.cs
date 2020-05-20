using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Manager.Entity.NodeObject;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Manager {

    //URDL 순서
    //미로 관리자
    public class MazeManager : MonoBehaviour {
        /// <summary>
        /// 무한반복 방지값
        /// </summary>
        public static int MaximumTurn = 10000000;
        /// <summary>
        /// 마지막 생성의 시작점
        /// </summary>
        public Vector2Int LastStart => start;
        /// <summary>
        /// 마지막 생성의 도착점
        /// </summary>
        public Vector2Int LastEnd => end;

        /// <summary>
        /// 한칸 한칸
        /// </summary>
        [NonSerialized] public Node[,] nodes;
        /// <summary>
        /// 마지막 연산결과로써의 시작,끝, 크기정보
        /// </summary>
        private Vector2Int start,
                           end,
                           size;
        /// <summary>
        /// 탐색 방문여부
        /// </summary>
        private HashSet<Vector2Int> empties;
        /// <summary>
        /// 게임씬 관리자
        /// </summary>
        private MainSceneManager Manager;
        [Header("미로 생성 완료 여부")] public bool Generated = false;
        [Header("한칸의 프리팹")] [SerializeField] private NodeRenderer rendererPrefab;
        [Header("한 프레임 당 최대 노드 생성 수")] [SerializeField] private int InstantiateSpeed;
        [Header("씬에 그려진 노드들")] private List<NodeRenderer> Renderers;
        /// <summary>
        /// 맵 생성
        /// </summary>
        /// <param name="size">크기</param>
        /// <param name="startNode">시작점</param>
        /// <param name="endNode">도착점</param>
        /// <returns></returns>
        public IEnumerator GenerateMap(Vector2Int size, Vector2Int startNode, Vector2Int endNode) {
#region 이전 생성 기록 해제 및 새로 시작 조건 설정

            foreach (NodeRenderer t in Renderers) Destroy(t.gameObject);
            Renderers.Clear();
            this.size = size;
            start = startNode;
            end = endNode;
            nodes = new Node[size.x, size.y];
            for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++) {
                nodes[i, j] = new Node() {
                    X = i,
                    Y = j
                };
                if (i > 0) {
                    nodes[i, j].Neighbor[3] = nodes[i - 1, j];
                    nodes[i - 1, j].Neighbor[1] = nodes[i, j];
                }
                if (j > 0) {
                    nodes[i, j].Neighbor[0] = nodes[i, j - 1];
                    nodes[i, j - 1].Neighbor[2] = nodes[i, j];
                }
            }

#endregion

#region 미로 생성

            Node ends = nodes[endNode.x, endNode.y];
            ends.Generated = true;
            empties = new HashSet<Vector2Int>(); //미로가 아직 생성 안된 노드들의 좌표
            for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
                if (endNode.x != i || endNode.y != j)
                    empties.Add(new Vector2Int(i, j)); //도착점 제외 모두 생성안됨에 추가
            int counterA = 0;
            while (empties.Count > 0 && counterA++ < MaximumTurn) { //무한반복 가능성 1
                Vector2Int initialCoord = empties.ElementAt((int) (Random.value * empties.Count)); //미로 안된 랜덤한 지점 골라서
                Node nowNode = nodes[initialCoord.x, initialCoord.y];
                int counterB = 0;
                while (!nowNode.Generated && counterB++ < MaximumTurn) { //무한반복 가능성 2
                    int nextDir = (int) (Random.value * 4);
                    int counterC = 0;
                    while (nowNode.Neighbor[nextDir] == null && counterC++ < MaximumTurn) { //무한반복 가능성 3
                        nextDir = (nextDir + 1) % 4;
                    } //랜덤한 방향 골라서
                    nowNode.Neighbor[nextDir].LastParent = nowNode; //랜덤하게 이동
                    nowNode = nowNode.Neighbor[nextDir];
                }
                counterB = 0;
                do { //무한반복 가능성 4
                    nowNode.Generated = true;
                    empties.Remove(new Vector2Int(nowNode.X, nowNode.Y));
                    if (nowNode.LastParent == null) break;
                    for (int i = 0; i < nowNode.Neighbor.Length; i++) {
                        if (nowNode.Neighbor[i] == nowNode.LastParent) {
                            nowNode.Wall[i] = false;
                            nowNode.LastParent.Wall[(i + 2) % 4] = false;
                        }
                    }
                    nowNode = nowNode.LastParent;
                } while (!nowNode.Generated && counterB++ < MaximumTurn); //생성된 부분에 도착 시 되짚어가며 미로경로 생성
            }

#endregion

#region 생성된 미로 씬에 등장

            int timer = InstantiateSpeed;
            if (rendererPrefab != null)
                for (int i = 0; i < size.x; i++) {
                    for (int j = 0; j < size.y; j++) {
                        Vector2Int coord = new Vector2Int(i, j);
                        NodeType type = NodeType.Normal; //시작/도착 여부
                        if (coord == start) type = NodeType.Start;
                        if (coord == end) type = NodeType.End;
                        Node t = nodes[i, j];
                        timer--;
                        Renderers.Add(Instantiate(rendererPrefab, transform).Initialize(Manager, t, type)); //칸 생성
                        if (timer < 0) { //일정 갯수 이상 생성했으면 렉방지를 위해 쉬어가기
                            timer = InstantiateSpeed;
                            yield return null;
                        }
                    }
                }

#endregion

            Generated = true;
        }
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="mainSceneManager">관리자</param>
        public void Initialize(MainSceneManager mainSceneManager) {
            Manager = mainSceneManager;
            Renderers = new List<NodeRenderer>();
        }
    }

}