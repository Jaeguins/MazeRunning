using System.Collections.Generic;
using Scripts.Manager.Entity.NodeObject;
using Scripts.Manager.Quiz;
using UnityEngine;

namespace Scripts.Manager
{
    [CreateAssetMenu(fileName = "WallPool",menuName = "custom/WallPool")]
    public class WallPool:ScriptableObject
    {
        public List<WallRenderer> wallPrefabs;
        public List<Quiz.Quiz> quizPrefabs;
        public List<QuizData> quizData;
        private MainSceneManager Manager;
        public GameObject EndPrefab;
        public GameObject StartPrefab;

        public WallRenderer GetWallPrefab(int type = -1) => type == -1 ? wallPrefabs[(int) (Random.value * wallPrefabs.Count)] : wallPrefabs[type];

        public void Initialize(MainSceneManager manager)
        {
            Manager = manager;
        }
    }
}