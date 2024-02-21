using DVT.Elevate.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Interface.Elevator
{
    public abstract class ElevatorBase
    {
        public string Name { get; set; } = string.Empty;
        public int CurrentFloorNumber { get; set; }
        public abstract void MoveElevator(ElevatorMovement elevatorMovement);
    }
}
