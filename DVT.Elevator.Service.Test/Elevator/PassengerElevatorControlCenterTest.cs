using DVT.Elevate.Domian.Configuration;
using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Service.Elevator;
using Microsoft.Extensions.Options;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Service.Test.Elevator
{
    internal class PassengerElevatorControlCenterTest
    {
        PassengerElevatorControlCenter passengerElevatorControlCenter;
        [SetUp]
        public void SetUp()
        {
            IOptions<ConfigurationOptions> options = Options.Create<ConfigurationOptions>(new()
            {
                 CheckUpdateTime = 1000,
                 NumberOfFloors = 10,
                 PassengerLimit = 10,             
            });
            passengerElevatorControlCenter = new PassengerElevatorControlCenter(new PassengerElevatorFactoryService(),options );
        }

        [Test] 
        public async Task ProcessElevatorRequestQueue_New_Elevator_Test() 
        {
            // setup 
            var request = new ElevatorRequest()
            {
                Direction = Elevate.Domian.Enums.ElevatorMovement.Up,
                ElevatorType = Elevate.Domian.Enums.ElevatorType.Passenger,
                FloorNumber = 1,
                NumberOfPassengers = 1,
                RequestedFloorNumber = 6,
            };
            //actual
           var elevator = await passengerElevatorControlCenter.ProcessElevatorRequestQueue(request);

            //assert
            Assert.IsNotNull(elevator);
            Assert.IsInstanceOf(typeof(PassengerElevator), elevator);
        }
        [Test]
        public async Task ProcessElevatorRequestQueue_Request_Number_of_passengers_Greater_Than_Limit_Elevator_Test()
        {
            // setup 
            var request = new ElevatorRequest()
            {
                Direction = Elevate.Domian.Enums.ElevatorMovement.Up,
                ElevatorType = Elevate.Domian.Enums.ElevatorType.Passenger,
                FloorNumber = 1,
                NumberOfPassengers = 20,
                RequestedFloorNumber = 6,
            };
            //actual
            var exception = await Should.ThrowAsync<ArgumentOutOfRangeException>(() => passengerElevatorControlCenter.ProcessElevatorRequestQueue(request));

            //assert
            exception.ShouldNotBeNull();
            exception.Message.ShouldBe("Specified argument was out of the range of valid values. (Parameter 'The number of passengers is more than the passenger limit of the elevators')");
        }
        [Test]
        public async Task ProcessElevatorRequestQueue_Request_Destination_Greater_Than_Floors_In_The_Building_Test()
        {
            // setup 
            var request = new ElevatorRequest()
            {
                Direction = Elevate.Domian.Enums.ElevatorMovement.Up,
                ElevatorType = Elevate.Domian.Enums.ElevatorType.Passenger,
                FloorNumber = 2,
                NumberOfPassengers = 2,
                RequestedFloorNumber = 12,
            };
            //actual
            var exception = await Should.ThrowAsync<ArgumentOutOfRangeException>(() => passengerElevatorControlCenter.ProcessElevatorRequestQueue(request));

            //assert
            exception.ShouldNotBeNull();
            exception.Message.ShouldBe("Specified argument was out of the range of valid values. (Parameter 'The foor of destination can not be greater than the number of floors in the building')");
        }
    }
}
