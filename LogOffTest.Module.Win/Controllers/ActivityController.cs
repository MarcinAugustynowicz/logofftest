using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
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
    public partial class ActivityController : WindowController
    {
        public ActivityController()
        {   
            InitializeComponent();
            TargetWindowType = WindowType.Main;
        }
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ((WinWindow)Frame).KeyDown += WindowController1_KeyDown;
            ((WinWindow)Frame).Showing += Setup;            
        }        
        System.Timers.Timer mousetimer;
        Point lastpos;
        void Setup(object sender, WinWindowShowingEventArgs e)
        {
            mousetimer = new System.Timers.Timer();
            mousetimer.Elapsed += MouseTimerLoop;
            mousetimer.Interval = 1000;
            mousetimer.AutoReset = true;
            mousetimer.Enabled = true;                        
            ((WinWindow)Frame).Form.Activated+= ActivatedEvent;
        }
        void WindowController1_KeyDown(object sender, KeyEventArgs e)
        {
            ReportActivity();
        }
        void ActivatedEvent(object sender, EventArgs e)
        {
            ReportActivity();
        }        
        void MouseTimerLoop(object senders, ElapsedEventArgs e)
        {
            if (lastpos!= System.Windows.Forms.Cursor.Position)
            {
                ReportActivity();
            }
            lastpos = System.Windows.Forms.Cursor.Position;            
        }
        void ReportActivity()
        {
            if (Application.MainWindow != null)
            {
                LogOutController cont = Application.MainWindow.GetController<LogOutController>();
                cont.UpdateTime();
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            mousetimer.Elapsed -= MouseTimerLoop;
            ((WinWindow)Frame).Form.Activated -= ActivatedEvent;
            ((WinWindow)Frame).KeyDown -= WindowController1_KeyDown;
            ((WinWindow)Frame).Showing -= Setup;
        }        
    }    
}
