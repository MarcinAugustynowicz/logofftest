using System;
using System.Collections.Generic;
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
using System.Timers;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.ExpressApp.Xpo;

namespace LogOffTest.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class LogOutController : WindowController
    {
        System.Timers.Timer loopTimer;
        bool started = false;
        int idleTime = 10;
        int logoutHour = 0;
        int logoutMinute = 18;
        DateTime lastActivity = DateTime.Now;
        public LogOutController()
        {
            TargetWindowType = WindowType.Main;
            InitializeComponent();
            loopTimer = new System.Timers.Timer();
            loopTimer.Elapsed += new ElapsedEventHandler(Check);
            loopTimer.Interval = 1000;
        }        
        protected override void OnActivated()
        {
            base.OnActivated();                                    
        }
        public delegate void fun();
        public fun commits;
        public void Check(object sender, ElapsedEventArgs e)
        {
            if (Application != null)
            {
                if (DateTime.Now - lastActivity > new TimeSpan(0, 0, idleTime) || (DateTime.Now.Hour == logoutHour && DateTime.Now.Minute == logoutMinute))
                {
                    commits();
                    Application.LogOff();                    
                    loopTimer.Stop();
                    lastActivity = DateTime.Now + new TimeSpan(0, 0, 1);
                }
            }
        }
        public void UpdateTime()
        {
            if (started == false)
            {
                loopTimer.SynchronizingObject = (ISynchronizeInvoke)Frame.Template;
                loopTimer.Start();
                started = true;
            }
            lastActivity = DateTime.Now;            
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();            
        }
    }
}
