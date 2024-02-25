using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Abstract.Elevator;
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
        public List<ElevatorRequest> RequestsOnBoard { get; set; }

        public PassengerElevator()
        {
           RequestsOnBoard = new List<ElevatorRequest>();
           Id = Guid.NewGuid();
        }

        public override void MoveElevator(ElevatorMovement elevatorMovement)
        {
            switch (elevatorMovement)
            {
                case ElevatorMovement.Up:
                    CurrentFloorNumber++; break;
                case ElevatorMovement.Down:
                    CurrentFloorNumber--; break;
                    
            }
        }
    }
}
