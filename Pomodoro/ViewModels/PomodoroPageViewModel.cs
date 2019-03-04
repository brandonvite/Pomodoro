using System;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class PomodoroPageViewModel : NotificationObject
    {
        #region Propiedades

        private Timer timer;

        private TimeSpan ellapsed;

        public TimeSpan Ellapsed
        {
            get { return ellapsed; }
            set
            {
                ellapsed = value;
                OnpropertyChanged();
            }
        }

        private bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnpropertyChanged();
            }
        }

        #endregion Propiedades

        #region Commands

        public ICommand StartOrPauseCommand { get; set; }

        #endregion Commands

        public PomodoroPageViewModel()
        {
            InitializeTimer();
            StartOrPauseCommand = new Command(StartOrPauseCommandExecute);
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(1));
        }

        private void StartTimer()
        {
            timer.Start();
            IsRunning = true;
        }

        private void StopTimer()
        {
            timer.Stop();
            IsRunning = false;
        }

        private void StartOrPauseCommandExecute(object obj)
        {
            if (IsRunning)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }
    }
}
