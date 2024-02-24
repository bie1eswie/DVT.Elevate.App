using DVT.Elevate.Domian.Abstract.App;
using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using DVT.Elevate.Service.Helpers;
using DVT.Elevator.Abstract.Elevator;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Elevator
{
    public class ElevatorApp: IElevatorApp
    {
        private readonly IElevatorControlCenter _controlCenter;

        public ElevatorApp(IElevatorControlCenter controlCenter)
        {
            _controlCenter = controlCenter;
        }
        public async void Execute()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("==============Thapelo Motubatse's Elevator Challenge==============");
                    Console.WriteLine("Please follow the menu below to request an elevator");
                    Console.WriteLine();
                    Console.WriteLine();
                    var floorNumber = InputValidationHelper.GetReuiredIntegerInputFromStandardInput($"Please enter your floor number between 0 representing ground floor, and {_controlCenter.GetBuildingNumberOfFloors()} the last floor");
                    var peopleWaiting = InputValidationHelper.GetReuiredIntegerInputFromStandardInput("Please enter the number of people waiting for the elevator on your floor");
                    var requestedFloor = InputValidationHelper.GetReuiredIntegerInputFromStandardInput("Please enter your floor of destination");

                    var elevatorRequest = new ElevatorRequest()
                    {
                        Direction = floorNumber < requestedFloor? ElevatorMovement.Up: ElevatorMovement.Down,
                        ElevatorType = ElevatorType.Passenger,
                        FloorNumber = floorNumber,
                        NumberOfPassengers = peopleWaiting,
                        RequestedFloorNumber = requestedFloor
                    };

                   await  _controlCenter.ProcessElevatorRequestQueue(elevatorRequest);
                   await _controlCenter.ShowElevatorState();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }
    }
}
