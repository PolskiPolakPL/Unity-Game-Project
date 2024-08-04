using System; 
namespace PolskiPolakPL.Utils.Timer
{
    /// <summary>
    /// Timer class from tutorial extended by PolskiPolakPL. Tutorial link: 
    /// <seealso href="https://youtu.be/pRjTM3pzqDw"></seealso>
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// Remaning time in seconds.
        /// </summary>
        public float RemaningSeconds;



        /// <summary>
        /// Time passed in seconds.
        /// </summary>
        public float SecondsPassed = 0;



        private bool isLooping = false;

        /// <summary>
        /// Public getter of 'isLooping' boolean.
        /// </summary>
        public bool IsLooping
        {
            get { return isLooping; }
            set { isLooping = value; }
        }


        private bool isPaused = false;



        /// <summary>
        /// Public getter of 'isPaused' boolean.
        /// </summary>
        public bool IsPaused
        {
            get { return isPaused; }
        }


        /// <summary>
        /// Timer Action Event invoked at the end of counting time.
        /// </summary>
        public event Action OnTimerEnd;



        private float duration;



        /// <param name="duration">Duration of the timer in seconds</param>
        public Timer(float duration)
        {
            this.duration = duration;
            RemaningSeconds = duration;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="duration">Duration of the timer in seconds</param>
        /// <param name="isLooping">Does timer loop over time?</param>
        public Timer(float duration, bool isLooping)
        {
            this.duration = duration;
            RemaningSeconds = duration;
            this.isLooping = isLooping;
        }



        /// <summary>
        /// Method used to move time one tick. Recommended use in <c>Update()</c> or <c>FixedUpdate()</c> methods.
        /// </summary>
        /// <param name="deltaTime">time difference between ticks</param>
        public void Tick(float deltaTime)
        {
            if (isPaused)
                return;
            if(RemaningSeconds == 0)
            {
                if (isLooping)
                    RemaningSeconds = duration;
                return;
            }
            RemaningSeconds -= deltaTime;
            SecondsPassed += deltaTime;
            CheckForTimerEnd();
        }


        /// <summary>
        /// Method that paused the timer.
        /// </summary>
        public void Pause()
        {
            isPaused = true;
        }



        /// <summary>
        /// Method that resumes the timer. 
        /// </summary>
        public void Resume()
        {
            isPaused = false;
        }


        private void CheckForTimerEnd()
        {
            if(RemaningSeconds > 0)
                return;
            RemaningSeconds = 0;
            OnTimerEnd?.Invoke();
        }
    }
}
