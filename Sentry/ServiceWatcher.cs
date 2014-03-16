using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using BusinessFacade;
using BusinessFacade.Interface;
using BusinessFacade.Models;

namespace Sentry
{
    /// <summary>
    /// To monitor the services
    /// </summary>
    public partial class ServiceWatcher : ServiceBase
    {
        private IEnumerable<ServiceDto> services;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceWatcher"/> class.
        /// </summary>
        public ServiceWatcher()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class,
        /// executes when a Start command is sent to the service by the Service Control Manager (SCM) or
        /// when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            Main();
            var interval = Double.Parse(ConfigurationManager.AppSettings["Interval"]);
            this.timer = new Timer(interval * 60 * 1000); //5 mins
            this.timer.Start();
            this.timer.AutoReset = true;
            this.timer.Elapsed += new ElapsedEventHandler(this.timer_elapsed);
        }

        /// <summary>
        /// Handles the elapsed event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void timer_elapsed(object sender, ElapsedEventArgs args)
        {
            Main();
        }

        /// <summary>
        /// When implemented in a derived class,
        /// executes when a Stop command is sent to the service by the Service Control Manager (SCM).
        /// Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private void Main()
        {
            IQueryFor<EmptyParameter, IEnumerable<ServiceDto>> serviceQuery = new ServiceQuery();
            services = serviceQuery.ExecuteQueryWith(new EmptyParameter());
            services.ToList().ForEach(service => ServiceMonitor.SaveResults(ServiceMonitor.InvokeService(service)));
        }
    }
}
