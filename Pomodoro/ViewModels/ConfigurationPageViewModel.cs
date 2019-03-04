using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class ConfigurationPageViewModel : NotificationObject
    {
        private const string PomodoroDuration = "PomodoroDuration";
        private const string BreakDuration = "BreakDuration";

        #region Propiedades

        private ObservableCollection<int> pomodoroDurations;

        public ObservableCollection<int> PomodoroDurations
        {
            get { return pomodoroDurations; }
            set
            {
                pomodoroDurations = value;
                OnpropertyChanged();
            }
        }

        private ObservableCollection<int> breakDurations;

        public ObservableCollection<int> BreakDurations
        {
            get { return breakDurations; }
            set
            {
                breakDurations = value;
                OnpropertyChanged();
            }
        }

        private int selectedPomodoroDuration;

        public int SelectedPomodoroDuration
        {
            get { return selectedPomodoroDuration; }
            set
            {
                selectedPomodoroDuration = value;
                OnpropertyChanged();
            }
        }

        private int selectedBreakDurations;

        public int SelectedBreakDurations
        {
            get { return selectedBreakDurations; }
            set
            {
                selectedBreakDurations = value;
                OnpropertyChanged();
            }
        }

        #endregion propiedades

        #region Commands

        public ICommand SaveCommand { get; set; }

        public ConfigurationPageViewModel()
        {
            LoadPomodoroDurations();
            LoadBreakDurations();
            LoadConfiguration();
            SaveCommand = new Command(SaveCommandExecute);
        }

        private void LoadConfiguration()
        {
            if(Application.Current.Properties.ContainsKey(PomodoroDuration))
            {
                SelectedPomodoroDuration = (int)Application.Current.Properties[PomodoroDuration];
            }

            if (Application.Current.Properties.ContainsKey(BreakDuration))
            {
                SelectedBreakDurations = (int)Application.Current.Properties[BreakDuration];
            }
        }

        private void LoadBreakDurations()
        {
            BreakDurations = new ObservableCollection<int>();
            BreakDurations.Add(1);
            BreakDurations.Add(5);
            BreakDurations.Add(10);
            BreakDurations.Add(25);
        }

        private void LoadPomodoroDurations()
        {
            PomodoroDurations = new ObservableCollection<int>();
            PomodoroDurations.Add(1);
            PomodoroDurations.Add(5);
            PomodoroDurations.Add(10);
            PomodoroDurations.Add(25);
        }

        private async void SaveCommandExecute()
        {
            Application.Current.Properties[PomodoroDuration] = SelectedPomodoroDuration;
            Application.Current.Properties[BreakDuration] = SelectedBreakDurations;

            await Application.Current.SavePropertiesAsync();

        }

        #endregion Commands
    }
}
