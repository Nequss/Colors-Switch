using System;
using System.Collections.Generic;
using System.Text;

namespace Colors_Switch.Logic
{
    public class GameTime
    {
        private float _deltaTime = 0f;
        private float _timeScale = 1f;

        public float deltaTime
        {
            get { return _deltaTime * timeScale; }
            set { _deltaTime = value; }
        }

        public float deltaTimeRaw
        {
            get { return _deltaTime; }
        }

        public float timeScale
        {
            get { return _timeScale; }
            set { _timeScale = value; }
        }

        public float totalTimeElapsed 
        {
            get;
            protected set;
        }

        public GameTime()
        {

        }

        public void Update(float deltaTime, float totalTimeElapsed)
        {
            _deltaTime = deltaTime;
            this.totalTimeElapsed = totalTimeElapsed;
        }
    }
}
