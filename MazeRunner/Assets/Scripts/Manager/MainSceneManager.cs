﻿using Assets.Scripts.Manager.Entity;
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

        public bool Initialized = false;
        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            MazeManager.Initialize(this);
            WallPoolManager.Initialize(this);
            start = new Vector2Int(Random.Range(0, size.x/2), Random.Range(0, size.y/2));
            do
            {
                end = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
            } while (Vector2Int.Distance(start,end)<size.magnitude/2);

            StartCoroutine(MazeManager.GenerateMap(size,start,end));
            Player.Initialize(this);
            Initialized = true;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
