using Projet.Entite.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class OpenInfoSerieEvent
    {
        #region singleton

        private static OpenInfoSerieEvent _evenement;

        private OpenInfoSerieEvent()
        {

        }

        public static OpenInfoSerieEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new OpenInfoSerieEvent();
            }
            return _evenement;
        }

        #endregion

        public event EventHandler Handler;

        public void OnOpenInfoSerieHandler(SerieEventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}
