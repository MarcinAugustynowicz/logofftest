using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace LogOffTest.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class ActivityReportController : WindowController
    {
        //class that sends activity events registered by a specific window to IdleController that is attached to MainWindow
        private ActivityReporter reporter;
        public ActivityReportController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //THIS IS NOT WORKING: Showing event is not firing for child windows
            ((WinWindow)Frame).Showing += SetupNotifier;
            
            // Perform various tasks depending on the target Window.            
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }        
        public void SetupNotifier(object sender, EventArgs e)
        {
            reporter = new ActivityReporter(((WinWindow)Frame).Form);
            ((WinWindow)Frame).Form.KeyPreview = true;
            //THIS IS NOT WORKING: MainWindow is null when this event is fired
            reporter.Activity += Application.MainWindow.GetController<IdleController>().ResetTimer;
        }
    }
}
