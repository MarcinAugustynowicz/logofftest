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
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.ExpressApp.Xpo;
using System.Timers;
namespace LogOffTest.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class LogOutController : WindowController
    {
        //time of day, at which user should be logged out
        private TimeSpan logoutTime = new TimeSpan(23,9,0);
        //date at which user should be logged out, calculated at login from current time and logoutTime
        private DateTime logoutDate;
        //event that is called just before logging off due to inactivity
        public event EventHandler<EventArgs> LoggingOff;
        //timer used for logging of at logoutTime
        private Timer LogOffTimer;       
        private IdleControllerBase cont;
        private bool started;
        public LogOutController()
        {
            started = false;
            TargetWindowType = WindowType.Main;            
            InitializeComponent();            
        }
        protected override void OnActivated()
        {
            base.OnActivated();  
        }        
        public void Setup(object sender, EventArgs e)
        {
            if(started==false)
            {
                //set logoutdate to next time of day of logouttime and start timer
                if (DateTime.Now.TimeOfDay > logoutTime)
                {
                    logoutDate = DateTime.Today.Add(logoutTime).AddDays(1);
                }
                else
                {
                    logoutDate = DateTime.Today.Add(logoutTime);
                }
                LogOffTimer = new Timer();
                LogOffTimer.Interval = (logoutDate - DateTime.Now).TotalMilliseconds;
                LogOffTimer.SynchronizingObject = (ISynchronizeInvoke)Frame.Template;
                LogOffTimer.Elapsed += LogOff;
                LogOffTimer.AutoReset = false;
                LogOffTimer.Start();
                started = true;
            }
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            cont = Frame.GetController<IdleControllerBase>();
            cont.UserIdle += LogOff;
            Frame.TemplateChanged += Setup;
        }        
        public void LogOff(object sender, EventArgs e)
        {   
            LoggingOff?.Invoke(this, new EventArgs());
            Application.LogOff();                                                 
        }        
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            cont.UserIdle -= LogOff;
            started = false;            
        }
    }
}