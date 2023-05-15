using System;

namespace Examples.VerticalScrollerExample
{
    public class EventBus
    {
        //  Player
        public Action<int> PlayerDamaged;
        public Action<int> AddHealth;
        public Action<int> PlayerHealthChanged;
        public Action PlayerDead;

        //  Score
        public Action<int> AddScore;
        public Action<int> ScoreChanged;
        
        //  Gold
        public Action<int> AddGold;
        public Action<int> SpendGold;
        public Action<int> GoldChanged;
        
        //  Interactables
        public Action<Interactable> SpawnInteractable;
        public Action<Interactable> InteractableActivated;
        
        public Action<Interactable> DisposeInteractable;
        public Action<Interactable> InteractableDisposed;
        
        public Action<float> SlowPlayer;
        public Action<float> AddShield;
        public Action<float> RemoveTime;
        
        //  Levels
        public Action<Level> LevelSet;
        public Action LevelRestart;
        public Action NextLevel;
        public Action<Level> LevelPassed;
        public Action LevelTimePassed;
        
        //  Game
        public Action GameStart;
        public Action GameStop;
        
        public Action<int> SelectShip;
        public Action<float> LevelProgressChanged;
        public Action GoToMenu;
       
        private EventBus()
        {
        }
        
        private static EventBus _instance;
        public static EventBus Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventBus();

                return _instance;
            }
        }
    }
}