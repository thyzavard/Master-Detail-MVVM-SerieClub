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
   public class ViewAccueil : INotifyPropertyChanged
    {
        private String profilButtonText;

        public ICommand TestCommand
        {
            get;
            set;
        }

        public String ProfilButtonText
        {
            get
            {
                return profilButtonText;
            }
            set
            {
                profilButtonText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProfilButtonText"));
            }

        }
        public ICommand ChangeTextCommand
        {
            get;
            set;

        }


        public ViewAccueil()
        {
            ChangeTextCommand = new RelayCommand(() => ProfilButtonText="J'ai change");
            TestCommand = new RelayCommand(() => this.ToString());
            ProfilButtonText = "Text";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
