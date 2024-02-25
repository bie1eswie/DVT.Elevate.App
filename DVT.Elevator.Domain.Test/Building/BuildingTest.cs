using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevator.Domain.Test.Building
{
    internal class BuildingTest
    {
        DVT.Elevate.Domian.Building.Building building;

        [SetUp]
        public void SetUp()
        {
            building = new Elevate.Domian.Building.Building(10);
            building.PassengerElevators = new List<Elevate.Domian.Elevator.PassengerElevator> 
            {
                new Elevate.Domian.Elevator.PassengerElevator()
                {
                     CurrentFloorNumber = 1,
                     CurrentNumberOfPassengersOnBoard = 1,
                     Direction = Elevate.Domian.Enums.ElevatorMovement.Up,
                     ElevatorState = Elevate.Domian.Enums.ElevatorState.InMotion,
                     Id = new Guid("cebb852e-ad63-4fd4-8d0c-501dc4815758"),
                     ElevatorType = Elevate.Domian.Enums.ElevatorType.Passenger,
                     PassengerLimit = 10,             
                },
                new Elevate.Domian.Elevator.PassengerElevator()
                {
                     CurrentFloorNumber = 9,
                     CurrentNumberOfPassengersOnBoard = 5,
                     Direction = Elevate.Domian.Enums.ElevatorMovement.Down,
                     ElevatorState = Elevate.Domian.Enums.ElevatorState.InMotion,
                     Id = new Guid("7f85eb40-3330-471a-a3e9-131aa2978781"),
                     ElevatorType = Elevate.Domian.Enums.ElevatorType.Passenger,
                     PassengerLimit = 10,
                }
            };
        }

        [Test]
        public async Task Get_Available_Elelvators_Going_Down()
        {
            var availableElevators = await building.GetAvailableElevatorsByType(Elevate.Domian.Enums.ElevatorType.Passenger,Elevate.Domian.Enums.ElevatorMovement.Down);

            Assert.That(availableElevators, Is.Not.Null);
            Assert.That(availableElevators.Any(), Is.True);

        }
        [Test]
        public async Task Get_Available_Elelvators_Going_Up()
        {
            var availableElevators = await building.GetAvailableElevatorsByType(Elevate.Domian.Enums.ElevatorType.Passenger, Elevate.Domian.Enums.ElevatorMovement.Up);

            Assert.That(availableElevators, Is.Not.Null);
            Assert.That(availableElevators.Any(), Is.True);

        }
    }
}
