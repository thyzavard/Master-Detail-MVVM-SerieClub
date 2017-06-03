using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class WindowClosedEvent
    {
        #region Singleton

        private static WindowClosedEvent _evenement;
        private WindowClosedEvent()
        {

        }

        public static WindowClosedEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new WindowClosedEvent();
            }
            return _evenement;
        }

        #endregion

        public event EventHandler Handler;

        public void OnWindowClosedHandler(EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}
