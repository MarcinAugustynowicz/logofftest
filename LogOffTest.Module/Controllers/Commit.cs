using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace LogOffTest.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class Commit : ViewController
    {
        private LogOutController cont;
        public Commit()
        {
            InitializeComponent();            
        }        
        public void commit(object sender,EventArgs e)
        {
            //commit changes in the object space
            ObjectSpace.CommitChanges();
        }        
        protected override void OnActivated()
        {
            //subscribe to LoggingOff event of the LogOutController
            base.OnActivated();
            cont=Application.MainWindow.GetController<LogOutController>();
            cont.LoggingOff += commit;
        }        
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.            
            base.OnDeactivated();
            cont.LoggingOff -= commit;
        }
    }
}
