using DVT.Elevate.Domian.Elevator;
using DVT.Elevator.Interface.Building;
using DVT.Elevator.Interface.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Building
{
    public class Building: IBuilding
    {
        public int NumberOfFloors {  get; set; } 
        public List<ElevatorBase> Elevators { get; set; }
        public Queue<ElevatorRequest> RequestQueue { get; set; }

        public Building() 
        {
             Elevators = new List<ElevatorBase>();
             RequestQueue = new Queue<ElevatorRequest>();
        }

        public Task<bool> EnqueueElevatorRequest(ElevatorRequest request)
        {
            this.RequestQueue.Enqueue(request);
            return Task.FromResult(true);
        }

        public Task<bool> ProcessElevatorRequestQueue()
        {
            throw new NotImplementedException();
        }
    }
}
