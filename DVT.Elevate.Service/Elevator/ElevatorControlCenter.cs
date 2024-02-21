using DVT.Elevate.Domian.Building;
using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Interface.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Elevator
{
    public class ElevatorControlCenter: IElevatorControlCenter
    {
        private readonly IElevatorFactoryService _elevatorFactoryService;
        private Building building { get; set; }
        public ElevatorControlCenter(IElevatorFactoryService elevatorFactoryService)
        {
            _elevatorFactoryService = elevatorFactoryService;
            building = new Building();
        }

        public async Task CreateElevator(ElevatorType elevatorType)
        {
            var newElevator = await _elevatorFactoryService.CreateElevator(elevatorType);
            building.Elevators.Add(newElevator);
        }
    }
}
