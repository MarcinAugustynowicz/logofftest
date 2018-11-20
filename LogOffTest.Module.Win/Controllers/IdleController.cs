using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActivityWinForms;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using LogOffTest.Module.Controllers;

namespace LogOffTest.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class IdleController : IdleControllerBase
    {
        Timer timer;        
        public IdleController(TimeSpan idleThreshold)
        {            
            timer = new Timer();
            timer.Enabled = false;
            timer.Interval = (int)idleThreshold.TotalMilliseconds;
            timer.Tick += base.ReportIdleEvent;
        }

        protected override void OnActivated()
        {
            base.OnActivated();            
        }
        public void ResetTimer(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Start();
        }
        public void Start()
        {
            timer.Start();
        }
        public void Stop()
        {
            timer.Stop();
        }      

    }
}


