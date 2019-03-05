using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Pomodoro.ViewModels
{
    public class RootPageViewModel : NotificationObject
    {
        public ObservableCollection<string> menuItems;

        public ObservableCollection<string> MenuItems
        {
            get { return menuItems; }
            set
            {
                menuItems = value;
                OnpropertyChanged();
            }
        }

        public string selectedMenuItem;

        public string SelectedMenuItem
        {
            get { return selectedMenuItem; }
            set
            {
                selectedMenuItem = value;
                OnpropertyChanged();
            }
        }

        public RootPageViewModel()
        {
            MenuItems = new ObservableCollection<string>();
            menuItems.Add("Pomodoro");
            menuItems.Add("Historico");
            menuItems.Add("Configuración");

            PropertyChanged += RootPageViewModel_PropertyChanged;
        }

        void RootPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedMenuItem))
            {
                if (SelectedMenuItem == "Configuración")
                {
                    MessagingCenter.Send(this, "GoToConfiguration");
                }

                if (SelectedMenuItem == "Pomodoro")
                {
                    MessagingCenter.Send(this, "GoToPomodoro");
                }

                if (SelectedMenuItem == "Historico")
                {
                    MessagingCenter.Send(this, "GoToHistorico");
                }
            }
        }
    }
}
