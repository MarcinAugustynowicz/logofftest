using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class ActivityController : ActivityControllerBase
    {
        private IdleNotifier notifier;        
        public ActivityController()
        {
            InitializeComponent();
            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ((WinWindow)Frame).Showing += SetupNotifier;
        }
        public void SetupNotifier(object sender, EventArgs e)
        {            
            notifier = new IdleNotifier(((WinWindow)Frame).Form, idleTime);
            ((WinWindow)Frame).Form.KeyPreview = true;
            notifier.Idle += base.ReportIdleEvent;
        }        
    }
}
