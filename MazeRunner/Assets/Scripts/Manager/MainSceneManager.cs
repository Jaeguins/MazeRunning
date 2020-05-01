using Assets.Scripts.Manager.Entity;
using Assets.Scripts.Manager.Entity.NodeObject;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class MainSceneManager : MonoBehaviour
    {
        public MazeManager MazeManager;
        public WallPool WallPoolManager;
        public WallRenderer GetWallPrefab(int type = -1) => WallPoolManager.GetWallPrefab(type);

        public Vector2Int size, start, end;

        public Player Player;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            MazeManager.Initialize(this);
            WallPoolManager.Initialize(this);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnClick()
        {
            MazeManager.GenerateMap(size,start,end);
        }
    }
}
