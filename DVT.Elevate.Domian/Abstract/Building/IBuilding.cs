using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Abstract.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Abstract.Building
{
    public interface IBuilding
    {
        Task<bool> EnqueueElevatorRequest(ElevatorRequest request);
        Task<IEnumerable<PassengerElevator>> GetAvailableElevatorsByType(ElevatorType elevatorType, ElevatorMovement direction);
    }
}
