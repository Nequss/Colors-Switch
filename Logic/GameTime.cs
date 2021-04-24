namespace Colors_Switch.Logic
{
    public class GameTime
    {
        private float _deltaTime = 0f;
        private float _timeScale = 0f;

        public GameTime()
        {
        }

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
        public void Update(float deltaTime, float totalTimeElapsed)
        {
            _deltaTime = deltaTime;
            this.totalTimeElapsed = totalTimeElapsed;
        }
    }
}