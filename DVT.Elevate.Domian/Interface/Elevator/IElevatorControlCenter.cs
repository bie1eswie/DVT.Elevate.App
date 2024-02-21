using DVT.Elevate.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Interface.Elevator
{
    public interface IElevatorControlCenter
    {
        Task CreateElevator(ElevatorType elevatorType);
    }
}
