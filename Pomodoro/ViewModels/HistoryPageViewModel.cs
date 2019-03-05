using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Pomodoro.ViewModels
{
    public class HistoryPageViewModel : NotificationObject
    {
        private ObservableCollection<DateTime> pomodoros;

        public ObservableCollection<DateTime> Pomodoros
        {
            get { return pomodoros; }
            set
            {
                pomodoros = value;
                OnpropertyChanged();
            }
        }

        public HistoryPageViewModel()
        {
            LoadHistory();
        }

        private void LoadHistory()
        {
            if(App.Current.Properties.ContainsKey(Literals.History))
            {
                var json = App.Current.Properties[Literals.History].ToString();
                var history = JsonConvert.DeserializeObject<List<DateTime>>(json);

                Pomodoros = new ObservableCollection<DateTime>(history);
            }
        }
    }
}
