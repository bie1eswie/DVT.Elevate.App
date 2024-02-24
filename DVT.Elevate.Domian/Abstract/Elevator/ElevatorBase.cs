using DVT.Elevate.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Abstract.Elevator
{
    public abstract class ElevatorBase
    {
        public Guid Id { get; set; }
        public ElevatorType ElevatorType { get; set; }
        public ElevatorMovement Direction { get; set; }
        public ElevatorState ElevatorState { get; set; }
        public int CurrentFloorNumber { get; set; }
        public abstract void MoveElevator(ElevatorMovement elevatorMovement);
    }
}
