using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using DVT.Elevator.Abstract.Building;
using DVT.Elevator.Abstract.Elevator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Building
{
    public class Building: IBuilding
    {
        public int NumberOfFloors {  get; set; } 
        public List<PassengerElevator>  PassengerElevators { get; set; }
        public Queue<ElevatorRequest> RequestQueue { get; set; }

        public Building() 
        {
            PassengerElevators = new List<PassengerElevator>();
            RequestQueue = new Queue<ElevatorRequest>();
        }
        public Building(int numberOfFloors)
        {
            PassengerElevators = new List<PassengerElevator>();
            RequestQueue = new Queue<ElevatorRequest>();
            NumberOfFloors = numberOfFloors;
        }
        public Task<bool> EnqueueElevatorRequest(ElevatorRequest request)
        {
            this.RequestQueue.Enqueue(request);
            return Task.FromResult(true);
        }

        public void UpdatePassengerElevator(PassengerElevator passengerElevator)
        {
            this.PassengerElevators.ForEach(x =>
            {
                if (x.Id == passengerElevator.Id)
                {
                    x = passengerElevator;
                }
            });
        }

        public Task<IEnumerable<PassengerElevator>> GetAvailableElevatorsByType(ElevatorType elevatorType, ElevatorMovement direction)
        {
            var result = this.PassengerElevators.Where(x=>x.ElevatorType == elevatorType && (x.Direction == direction || x.Direction == ElevatorMovement.Stationery));
            return Task.FromResult(result);
        }
    }
}
