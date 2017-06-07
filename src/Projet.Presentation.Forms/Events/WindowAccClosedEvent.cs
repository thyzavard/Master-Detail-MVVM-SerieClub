using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class WindowAccClosedEvent
    {
        #region Singleton

        private static WindowAccClosedEvent _evenement;
        private WindowAccClosedEvent()
        {

        }

        public static WindowAccClosedEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new WindowAccClosedEvent();
            }
            return _evenement;
        }

        #endregion

        public event EventHandler Handler;

        public void OnWindowAccClosedHandler(EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}
