using Example.Navigation.Presentation.App.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projet.Presentation.Forms.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private object selectedViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand OpenViewProfileCommand { get; set; }
        public ICommand OpenViewAcceuilCommand { get; set; }

        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public MainViewModel()
        {
            SelectedViewModel = new ViewProfil();
            SelectedViewModel = new ViewAccueil();

            OpenViewProfileCommand = new RelayCommand(v => SelectedViewModel = new ViewProfil(), c => SelectedViewModel.GetType() != typeof(ViewProfil));
            OpenViewAcceuilCommand = new RelayCommand(v => SelectedViewModel = new ViewAcceuil(), c => SelectedViewModel.GetType() != typeof(ViewAccueil));
        }


        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
