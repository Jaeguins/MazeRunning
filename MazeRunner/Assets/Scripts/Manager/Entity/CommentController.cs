using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Entity
{
    public class CommentController : MonoBehaviour
    {
        public static void Comment(string msg)
        {
            if(instance!=null)
                instance.CommentMsg(msg);
            else Debug.LogWarning("No CommentController");
        }

        private void CommentMsg(string msg)
        {
            nowTime = maxTime;
            textPart.text = msg;
            group.alpha = 1;
        }

        [SerializeField] private float maxTime = 5f;
        [SerializeField] private Text textPart;
        [SerializeField] private CanvasGroup group;

        private float nowTime;

        private static CommentController instance;
        public void OnDestroy()
        {
            instance = null;
        }

        public void Start()
        {
            instance = this;
        }

        public void Update()
        {
            if (nowTime < 0)
            {
                if (group.alpha > 0)
                    group.alpha -= Time.deltaTime;
            }
            else
            {
                nowTime -= Time.deltaTime;
            }
        }
    }
}
