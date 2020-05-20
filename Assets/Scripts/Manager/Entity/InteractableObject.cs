using UnityEngine;

namespace Scripts.Manager.Entity {

    /// <summary>
    /// 상호작용 가능한 오브젝트
    /// </summary>
    public class InteractableObject : MonoBehaviour {
        [Header("보고있는지 여부")]
        [SerializeField] private bool looking = false;
        [Header("나올 자막")]
        [SerializeField] private string commentMessage;
        [Header("보여줄 툴팁")]
        [SerializeField] private CanvasGroup canvas;
        [Header("상호작용 가능 거리")]
        [SerializeField] internal float maxInteractDistance;
        /// <summary>
        /// 투명도 조절 속도
        /// </summary>
        public const float AlphaChangeSpeed = .1f;

        /// <summary>
        /// 조준 시작
        /// </summary>
        public void OnMouseEnter() {
            looking = true;
        }
        /// <summary>
        /// 조준 끝
        /// </summary>
        public void OnMouseExit() {
            looking = false;
        }
        /// <summary>
        /// 툴팁 투명도 처리
        /// </summary>
        public void Update() {
            if (canvas != null) canvas.alpha = Mathf.Clamp01(canvas.alpha + (looking && Vector3.Distance(Camera.main.transform.position, transform.position) < maxInteractDistance ? AlphaChangeSpeed : -AlphaChangeSpeed));
        }
        /// <summary>
        /// 자막 출력
        /// </summary>
        public virtual void OnMouseOver() {
            if (Vector3.Distance(Camera.main.transform.position, transform.position) < maxInteractDistance) CommentController.Comment(commentMessage);
        }
    }

}