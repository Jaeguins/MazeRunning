using UnityEngine;

namespace Scripts.Manager.Entity {

    /// <summary>
    /// 페이드 인/아웃 관리자
    /// </summary>
    public class Fader : MonoBehaviour {
        [Header("페이드 그래픽")] [SerializeField] private CanvasGroup targetGraphic;
        /// <summary>
        /// 싱글톤 레퍼런스(어디서든 접근가능
        /// </summary>
        public static Fader Instance;
        /// <summary>
        /// 상태 수정
        /// </summary>
        public static bool TargetStatus {
            get => Instance != null && Instance.targetStatus;
            set {
                if (Instance != null) Instance.targetStatus = value;
            }
        }
        /// <summary>
        /// 현재 목표하는 상태(true일 시 페이드 아웃)
        /// </summary>
        private bool targetStatus;
        /// <summary>
        /// 속도
        /// </summary>
        public const float FadeSpeed = .01f;
        /// <summary>
        /// 초기화
        /// </summary>
        public void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject); //씬 로드 중 사라지지 않게 설정
        }
        /// <summary>
        /// 투명도 갱신
        /// </summary>
        public void Update() {
            targetGraphic.alpha = Mathf.Clamp01(targetGraphic.alpha + (targetStatus ? FadeSpeed : -FadeSpeed));
        }
    }

}