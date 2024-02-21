using DVT.Elevate.Domian.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Interface.Building
{
    public interface IBuilding
    {
        Task<bool> EnqueueElevatorRequest(ElevatorRequest request);
        Task<bool> ProcessElevatorRequestQueue();
    }
}
