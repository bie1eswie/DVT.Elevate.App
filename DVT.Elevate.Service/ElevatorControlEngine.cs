using DVT.Elevate.Domian.Configuration;
using DVT.Elevator.Abstract.Elevator;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service
{
    public class ElevatorControlEngine : BackgroundService
    {
        private readonly IElevatorControlCenter _elevatorControlCenter;
        private readonly IOptions<ConfigurationOptions> _appConfig;

        public ElevatorControlEngine(IElevatorControlCenter elevatorControlCenter, IOptions<ConfigurationOptions> appConfig)
        {
            _elevatorControlCenter = elevatorControlCenter;
            _appConfig = appConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await _elevatorControlCenter.UpdateElevatorStates();
                await Task.Delay(50000, stoppingToken);
            }
        }
    }
}
