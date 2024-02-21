using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Interface.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Elevator
{
    internal class ElevatorFactoryService : IElevatorFactoryService
    {
        public Task<ElevatorBase> CreateElevator(ElevatorType elevatorType)
        {
            ElevatorBase elevator = null;
            switch(elevatorType)
            {
                case ElevatorType.Passenger:
                    elevator = new PassengerElevator();
                    return Task.FromResult(elevator);
                default:
                    return Task.FromResult(elevator);
            }
        }
    }
}
