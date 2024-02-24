using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Abstract.Elevator
{
    public interface IElevatorControlCenter
    {
        Task<ElevatorBase?> ProcessElevatorRequestQueue(ElevatorRequest elevatorRequest);
        Task ShowElevatorState();
        Task UpdateElevatorStates();
        int GetBuildingNumberOfFloors();
    }
}
