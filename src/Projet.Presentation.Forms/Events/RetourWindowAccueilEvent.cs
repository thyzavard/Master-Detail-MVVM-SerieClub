using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Presentation.Forms.Events
{
    public class RetourWindowAccueilEvent
    {
        #region singleton

        private static RetourWindowAccueilEvent _evenement;

        private RetourWindowAccueilEvent()
        {

        }

        public static RetourWindowAccueilEvent GetInstance()
        {
            if (_evenement == null)
            {
                _evenement = new RetourWindowAccueilEvent();
            }
            return _evenement;
        }

        #endregion

        public event EventHandler Handler;

        public void OnRetourWindowAccueilHandler(EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
    }
}
