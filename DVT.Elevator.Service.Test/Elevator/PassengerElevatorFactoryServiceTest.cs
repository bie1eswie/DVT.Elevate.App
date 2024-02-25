using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Service.Elevator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Service.Test.Elevator
{
    internal class PassengerElevatorFactoryServiceTest
    {
        PassengerElevatorFactoryService passengerElevatorFactoryService;
        [SetUp]
        public void SetUp()
        {
            passengerElevatorFactoryService = new PassengerElevatorFactoryService();
        }

        [Test]
        public async Task CreateElevator_Success_Test()
        {
            var passengerElevator = await passengerElevatorFactoryService.CreateElevator();
            Assert.IsNotNull(passengerElevator);
            Assert.IsInstanceOf(typeof(PassengerElevator),passengerElevator);
        }
    }
}
