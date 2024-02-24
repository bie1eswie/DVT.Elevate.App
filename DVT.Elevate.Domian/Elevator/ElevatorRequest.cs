using DVT.Elevate.Domian.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Elevator
{
    public class ElevatorRequest
    {
        public int FloorNumber {  get; set; }
        public int NumberOfPassengers {  get; set; }
        public ElevatorType ElevatorType { get; set; }
        public ElevatorMovement Direction { get; set; }
        public int RequestedFloorNumber {  get; set; }
    }
}
