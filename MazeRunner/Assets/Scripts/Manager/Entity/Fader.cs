using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Entity
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private CanvasGroup targetGraphic;
        public static Fader Instance;

        public static bool TargetStatus
        {
            get => Instance != null && Instance.targetStatus;
            set
            {
                if (Instance != null)
                    Instance.targetStatus = value;
            }
        }

        private bool targetStatus;
        public const float FadeSpeed = .01f;

        public void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Update()
        {
            targetGraphic.alpha = Mathf.Clamp01(targetGraphic.alpha + (targetStatus ? FadeSpeed : -FadeSpeed));
        }
    }
}