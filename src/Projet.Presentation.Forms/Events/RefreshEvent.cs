using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class RefreshEvent
    {
        #region Singleton

        private static RefreshEvent _evenement;
        private RefreshEvent()
        {

        }

        public static RefreshEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new RefreshEvent();
            }
            return _evenement;
        }

        #endregion

        public event EventHandler Handler;

        public void OnRefreshAcceuilHandler(EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}
