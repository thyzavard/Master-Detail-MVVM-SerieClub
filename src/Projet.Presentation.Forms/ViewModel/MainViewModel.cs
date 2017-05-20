using Projet.Presentation.Forms.Commands;
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

        public ICommand OpenViewAcceuilCommand { get; set; }

        public ICommand OpenViewProfilCommand { get; set; }

        public ICommand OpenViewSerieCommand { get; set; }

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
            SelectedViewModel = new ViewAcceuil();


            OpenViewAcceuilCommand = new RelayCommand(v => SelectedViewModel = new ViewAcceuil(),
                c => SelectedViewModel.GetType() != typeof(ViewAcceuil));
            OpenViewProfilCommand = new RelayCommand(v => SelectedViewModel = new ViewProfil(),
                c => SelectedViewModel.GetType() != typeof(ViewProfil));
            OpenViewSerieCommand = new RelayCommand(v => SelectedViewModel = new ViewSerie(),
                c => SelectedViewModel.GetType() != typeof(ViewSerie));
        }

        private void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}

