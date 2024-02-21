using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Elevator
{
    public class ElevatorBackgroundService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var _timer = new PeriodicTimer(new TimeSpan(1));
            }

            return Task.CompletedTask;
        }
    }
}
