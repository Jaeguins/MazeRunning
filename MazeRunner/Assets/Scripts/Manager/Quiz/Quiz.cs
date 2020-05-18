using System.Collections;
using System.Collections.Generic;
using Scripts.Manager.Entity;
using Scripts.Manager.Entity.NodeObject;
using SpriteGlow;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Manager.Quiz
{
    public class Quiz : MonoBehaviour
    {
        private MainSceneManager manager;
        private int quizIndex, correctAnswerIndex;
        [SerializeField]private List<QuizButton> buttons;
        [SerializeField]private GameObject wallModel;
        [SerializeField]private ParticleSystem effect;
        [SerializeField]private Text frontText,backText;
        [SerializeField] private float clearTime = 2f;
        [SerializeField] private Color clearColor= new Color(.5f,0,1);
        
        


        private int[] wrongIndex;

        private QuizData data => manager.WallPoolManager.quizData[quizIndex];
        public void Initialize(MainSceneManager manager)
        {
            this.manager = manager;
            quizIndex = (int) (Random.value * manager.WallPoolManager.quizData.Count);
            correctAnswerIndex= (int) (Random.value * buttons.Count);
            wrongIndex=new int[(buttons.Count<data.WrongAnswers.Count+1)?buttons.Count:data.WrongAnswers.Count+1];
            HashSet<int> set=new HashSet<int>();
            for (int i = 0; i < wrongIndex.Length; i++)
            {
                if (i == correctAnswerIndex) continue;
                int tmp = (int) (Random.value * data.WrongAnswers.Count);
                while (set.Contains(tmp)) tmp = (int) (Random.value * data.WrongAnswers.Count);
                set.Add(tmp);
                wrongIndex[i] = tmp;
            }

            for (int i = 0; i < buttons.Count; i++)
            {
                if (i < wrongIndex.Length)
                {
                    buttons[i].gameObject.SetActive(true);
                    if (i == correctAnswerIndex)
                        buttons[i].Initialize(this, data.GetCorrectAnswer, true);
                    else
                        buttons[i].Initialize(this, data.GetWrongAnswer(wrongIndex[i]), false);
                }
                else buttons[i].gameObject.SetActive(false);
            }

            frontText.text = data.ProblemText;
            backText.text = data.ProblemText;
        }

        public void Solve(bool isCorrect)
        {
            if(isCorrect)
                StartCoroutine(BreakingRoutine());
            else
            {
                StartCoroutine(BackToStart());
            }
            
        }

        private IEnumerator BackToStart()
        {
            Fader.TargetStatus = true;
            manager.Player.CanMove = false;
            yield return new WaitForSeconds(2f);
            manager.Player.transform.position=manager.MazeManager.nodes[manager.start.x, manager.start.y].NodePos + Vector3.up * .1f;
            manager.Player.CanMove = true;
            Fader.TargetStatus = false;
        }

        private IEnumerator BreakingRoutine()
        {
            float time = clearTime;
            Image[] images = GetComponentsInChildren<Image>();
            while (time > 0)
            {
                foreach (Image t in images)
                {
                    t.color = Color.Lerp(clearColor, Color.black, time*1.5f / clearTime);
                }
                time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            wallModel.SetActive(false);
            effect.Play();
        }
    }
}