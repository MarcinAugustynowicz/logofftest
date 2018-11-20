using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActivityWinForms
{
    public class ActivityReporter : IMessageFilter
    {
        private Form form;        
        // following is a standard way of creating event handlers
        // but can be simply written like this: "public event EventHandler Inactive;"
        private event EventHandler activity;
        public event EventHandler Activity
        {
            add { activity += value; }
            remove { activity -= value; }
        }
        protected virtual void ReportActivity()
        {
            Debug.Write("ACTIVITY");
            activity?.Invoke(this, EventArgs.Empty);
        }
        public ActivityReporter(Form form)
        {
            this.form = form;
            // register monitored events on the form - all these events represent user activity
            // TODO: this is not complete, its just an example of how this can work
            
            form.MouseClick += Form_MouseClick;
            form.Activated += Form_Activated;
            form.Deactivate += Form_Deactivate;
            form.KeyDown += Form_KeyDown;
            
            // I know that this is not enough to catch all move events, 
            // but that's why we have this testing project
            form.Move += Form_Move;

            Application.AddMessageFilter(this);
        }
        private void Form_Move(object sender, EventArgs e)
        {
            ReportActivity();
        }
        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            ReportActivity();
        }
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            ReportActivity();
        }
        private void Form_Deactivate(object sender, EventArgs e)
        {
            ReportActivity();
        }
        private void Form_Activated(object sender, EventArgs e)
        {
            ReportActivity();
        }
        const int WM_MOUSEMOVE = 0x0200;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                if (Form.ActiveForm == form)
                    ReportActivity();
            }
            return false;
        }
    }
}