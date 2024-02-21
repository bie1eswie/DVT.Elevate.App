using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Interface.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Elevator
{
    public class PassengerElevator : ElevatorBase
    {
        public int PassengerLimit { get; set; }
        public int CurrentNumberOfPassengersOnBoard { get; set; }

        public override void MoveElevator(ElevatorMovement elevatorMovement)
        {
            throw new NotImplementedException();
        }
    }
}
