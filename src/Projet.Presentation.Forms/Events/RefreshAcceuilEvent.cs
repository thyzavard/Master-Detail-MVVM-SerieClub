using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class RefreshAcceuilEvent
    {
        #region Singleton

        private static RefreshAcceuilEvent _evenement;
        private RefreshAcceuilEvent()
        {

        }

        public static RefreshAcceuilEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new RefreshAcceuilEvent();
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
