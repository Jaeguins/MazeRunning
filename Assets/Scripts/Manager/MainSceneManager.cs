using System.Collections;
using Scripts.Manager.Entity;
using Scripts.Manager.Entity.NodeObject;
using UnityEngine;

namespace Scripts.Manager {

    public class MainSceneManager : MonoBehaviour {
        [Header("미로 관리자")] public MazeManager MazeManager;
        [Header("벽 프리팹 관리자")] public WallPool WallPoolManager;
        /// <summary>
        /// 벽 얻기 - 거쳐가는 메소드
        /// </summary>
        /// <param name="type">인덱스</param>
        /// <returns></returns>
        public WallRenderer GetWallPrefab(int type = -1) => WallPoolManager.GetWallPrefab(type);
        [Header("미로 간략정보")] public Vector2Int size,
                                              start,
                                              end;
        [Header("플레이어")] public Player Player;
        [Header("초기화 완료여부")] public bool Initialized = false;
        [Header("입자효과 여부")] public bool Particle = true;

        /// <summary>
        /// 시작
        /// </summary>
        void Start() {
            Initialize();
        }
        /// <summary>
        /// 초기화
        /// </summary>
        private void Initialize() {
#region 하위모듈 초기화

            MazeManager.Initialize(this);
            WallPoolManager.Initialize(this);

#endregion

#region 시작/도착점 랜덤지정

            start = new Vector2Int(Random.Range(0, size.x / 2), Random.Range(0, size.y / 2));
            do {
                end = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
            } while (Vector2Int.Distance(start, end) < size.magnitude / 2);

#endregion

            StartCoroutine(InitializeRoutine());
            Player.Initialize(this);
            Initialized = true;
        }
        /// <summary>
        /// 시작중 순서가 정해진 부분 - 미로가 생성 다된 뒤에 플레이어가 시작점에 위치해야함
        /// </summary>
        /// <returns></returns>
        private IEnumerator InitializeRoutine() {
            yield return MazeManager.GenerateMap(size, start, end);
            yield return Player.InitializeCoroutine();
            Fader.TargetStatus = false;
        }
    }

}