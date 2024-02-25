using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Abstract.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Elevator
{
    public class PassengerElevatorFactoryService : IElevatorFactoryService
    {
        public PassengerElevatorFactoryService() { }
        public Task<ElevatorBase> CreateElevator()
        {
            ElevatorBase elevator = new PassengerElevator();
            return Task.FromResult(elevator);
        }
    }
}
