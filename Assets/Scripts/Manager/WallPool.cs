using System.Collections.Generic;
using Scripts.Manager.Entity.NodeObject;
using Scripts.Manager.Quiz;
using UnityEngine;

namespace Scripts.Manager {

    [CreateAssetMenu(fileName = "WallPool", menuName = "custom/WallPool")]
    ///벽 종류 관리자
    public class WallPool : ScriptableObject {
        [Header("벽 종류")] public List<WallRenderer> wallPrefabs;
        [Header("퀴즈벽 종류")] public List<Quiz.Quiz> quizPrefabs;
        [Header("퀴즈 종류")] public List<QuizData> quizData;
        [Header("게임씬 매니저")] private MainSceneManager Manager;
        [Header("시작점 장식")] public GameObject EndPrefab;
        [Header("도착점 장식")] public GameObject StartPrefab;
        /// <summary>
        /// 벽 얻기
        /// </summary>
        /// <param name="type">인덱스</param>
        /// <returns></returns>
        public WallRenderer GetWallPrefab(int type = -1) => type == -1 ? wallPrefabs[(int) (Random.value * wallPrefabs.Count)] : wallPrefabs[type];
        /// <summary>
        /// 초기화
        /// </summary>
        /// <param name="manager">관리자</param>
        public void Initialize(MainSceneManager manager) {
            Manager = manager;
        }
    }

}