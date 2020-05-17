using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Manager
{
    public class StartSceneManager:MonoBehaviour
    {
        public string PlaySceneName;

        public void Start()
        {
            Cursor.lockState = CursorLockMode.None;
        }
        public void StartGame()
        {
            SceneManager.LoadScene(PlaySceneName);
        }
    }
}
