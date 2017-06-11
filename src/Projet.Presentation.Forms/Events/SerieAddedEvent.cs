using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class SerieAddedEvent
    {
        #region singleton

        private static SerieAddedEvent _evenement;

        private SerieAddedEvent()
        {

        }

        public static SerieAddedEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new SerieAddedEvent();
            }
            return _evenement;
        }

        #endregion

        public event EventHandler Handler;

        public void OnSerieAddedHandler(SerieEventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}
