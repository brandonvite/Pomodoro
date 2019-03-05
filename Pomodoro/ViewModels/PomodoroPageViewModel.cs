using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class PomodoroPageViewModel : NotificationObject
    {
        #region Propiedades

        private Timer timer;

        private TimeSpan ellapsed;

        private int pomodoroDuration;

        private int breakDuration;

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

        private bool isInBreak;

        public bool IsInBreak
        {
            get { return isInBreak; }
            set
            {
                isInBreak = value;
                OnpropertyChanged();
            }
        }

        private int duration;

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
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
            LoadConfigureValues();
            StartOrPauseCommand = new Command(StartOrPauseCommandExecute);
        }

        private void LoadConfigureValues()
        {
            pomodoroDuration = (int)Application.Current.Properties[Literals.PomodoroDuration];
            breakDuration = (int)Application.Current.Properties[Literals.BreakDuration];
            Duration = pomodoroDuration * 60;
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Ellapsed = Ellapsed.Add(TimeSpan.FromSeconds(1));

            if (IsRunning && Ellapsed.TotalSeconds == pomodoroDuration * 60)
            {
                IsRunning = false;
                IsInBreak = true;
                Ellapsed = TimeSpan.Zero;

                await SavePomodorAsync();
            }

            if (IsInBreak && Ellapsed.TotalSeconds == breakDuration * 60)
            {
                IsRunning = true;
                IsInBreak = false;
                Ellapsed = TimeSpan.Zero;
            }
        }

        private async Task SavePomodorAsync()
        {
            List<DateTime> history;

            if (Application.Current.Properties.ContainsKey(Literals.History))
            {
                var json = Application.Current.Properties[Literals.History].ToString();
                history = JsonConvert.DeserializeObject<List<DateTime>>(json);
            }
            else
            {
                history = new List<DateTime>();
            }

            history.Add(DateTime.Now);

            var serializedObject = JsonConvert.SerializeObject(history);

            Application.Current.Properties[Literals.History] = serializedObject;

            await Application.Current.SavePropertiesAsync();
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
