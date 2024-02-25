﻿using DVT.Elevate.Domian.Building;
using DVT.Elevate.Domian.Configuration;
using DVT.Elevate.Domian.Elevator;
using DVT.Elevate.Domian.Enums;
using DVT.Elevate.Domian.Exceptions;
using DVT.Elevator.Abstract.Elevator;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Service.Elevator
{
    public class PassengerElevatorControlCenter: IElevatorControlCenter
    {
        private readonly IElevatorFactoryService _elevatorFactoryService;
        private readonly IOptions<ConfigurationOptions> _appConfig;
        private Building building { get; set; }
        public PassengerElevatorControlCenter(IElevatorFactoryService elevatorFactoryService, IOptions<ConfigurationOptions> appConfig)
        {
            _elevatorFactoryService = elevatorFactoryService;
            building = new Building(appConfig.Value.NumberOfFloors);
            _appConfig = appConfig;
        }
        /// <summary>
        /// Create and add the elevator to the elevator list
        /// </summary>
        /// <param name="elevatorRequest"></param>
        /// <returns>Passenger Elevator</returns>
        private async Task<PassengerElevator> createElevator(ElevatorRequest elevatorRequest)
        {
            var newElevator = await _elevatorFactoryService.CreateElevator();
            switch (elevatorRequest.ElevatorType)
            {
                case ElevatorType.Passenger:
                    var newPassengerElevator = (PassengerElevator)newElevator;
                    newPassengerElevator.Direction = elevatorRequest.Direction;
                    newPassengerElevator.ElevatorState = ElevatorState.InMotion;
                    newPassengerElevator.CurrentNumberOfPassengersOnBoard = elevatorRequest.NumberOfPassengers;
                    newPassengerElevator.PassengerLimit = _appConfig.Value.PassengerLimit;
                    newPassengerElevator.CurrentFloorNumber = elevatorRequest.FloorNumber;
                    newPassengerElevator.RequestsOnBoard.Add(elevatorRequest);
                    newElevator = newPassengerElevator;              
                    building.PassengerElevators.Add(newPassengerElevator);
                break;
            }
            return newElevator as PassengerElevator;
        }
        /// <summary>
        /// process user elevator request
        /// </summary>
        /// <param name="nextRequest"></param>
        /// <returns>Passenger Elevator</returns>
        /// <exception cref="ElevatorNonFoundException"></exception>
        public async Task<ElevatorBase?> ProcessElevatorRequestQueue(ElevatorRequest nextRequest)
        {
            if(nextRequest.NumberOfPassengers > _appConfig.Value.PassengerLimit)
            {
                throw new ArgumentOutOfRangeException("The number of passengers is more than the passenger limit of the elevators");
            }
            if(nextRequest.RequestedFloorNumber > _appConfig.Value.NumberOfFloors)
            {
                throw new ArgumentOutOfRangeException("The foor of destination can not be greater than the number of floors in the building");
            }
            PassengerElevator nearestElevator = new PassengerElevator();
            Console.WriteLine("--------------------------------------------------------------");
            var availableElevators = await building.GetAvailableElevatorsByType(nextRequest.ElevatorType,nextRequest.Direction);
            //add new elevator if there is no availble elevator
            //send the elevator to the requested floor
            if(availableElevators == null || !availableElevators.Any())
            {
                nearestElevator =  await this.createElevator(nextRequest);
                return nearestElevator;
            }
            //Sort the available elevators by floor number
            var sortedList = availableElevators.OrderBy(x => x.CurrentFloorNumber).ToArray();
            switch (nextRequest.Direction)
            {
                case ElevatorMovement.Up:
                    //filter the list of elevators for the folowing conditions
                    //The current floor of the elevator is less than the request's floor number
                    //The number of pasengers on the elevator is less than the passenger limit that the elevatr can take
                    //The number of passengers in the current request + the number of passengers on the elevator is less than the passenger limit

                    var elevatorsBellow = sortedList.Where(x => (x.CurrentFloorNumber <= nextRequest.FloorNumber) && 
                                                                (x.CurrentNumberOfPassengersOnBoard < x.PassengerLimit) && 
                                                                (x.PassengerLimit > (x.CurrentNumberOfPassengersOnBoard + nextRequest.NumberOfPassengers)));

                    nearestElevator = elevatorsBellow.MaxBy(x => x.CurrentFloorNumber);
                    break;
                case ElevatorMovement.Down:
                    //Filter the list of elevators for the folowing conditions
                    //The current floor of the elevator is greater than the request's floor number
                    //The number of passengers in the current request + the number of passengers on the elevator is less than the passenger limit
                    //The number of pasengers on the elevator is less than the passenger limit that the elevatr can take

                    var elevatorsAbove = sortedList.Where(x => (nextRequest.FloorNumber > x.CurrentFloorNumber) &&
                                                               (x.CurrentNumberOfPassengersOnBoard < x.PassengerLimit) &&
                                                               (x.PassengerLimit > (x.CurrentNumberOfPassengersOnBoard + nextRequest.NumberOfPassengers)));
                    nearestElevator = elevatorsAbove.MinBy(x => x.CurrentFloorNumber);
                    break;
            }
            //We have picked an elevator that is already in use and we are going to update the data
            if (nearestElevator != null)
            {
                var newPassengerElevator = nearestElevator;
                newPassengerElevator.Direction = nextRequest.Direction;
                newPassengerElevator.ElevatorState = ElevatorState.InMotion;
                newPassengerElevator.CurrentNumberOfPassengersOnBoard += nextRequest.NumberOfPassengers;
                newPassengerElevator.RequestsOnBoard.Add(nextRequest);
                building.UpdatePassengerElevator(newPassengerElevator);
                return nearestElevator;
            }
            //we should never get to this point but just an edge case to look our for
            else
            {
                throw new ElevatorNonFoundException("There was no elevator found at the moment");
            }
        }
        /// <summary>
        /// Show elevator data
        /// </summary>
        /// <returns></returns>
        public Task ShowElevatorState()
        {
            Console.WriteLine($"Object hash code {this.GetHashCode()}");
            Console.WriteLine("===================Elevator States======================");
            foreach(var elevator in building.PassengerElevators)
            {
                Console.WriteLine($"Elevator Name              {elevator.Id}");
                Console.WriteLine($"Elevator Mode              {elevator.ElevatorState}");
                Console.WriteLine($"Elevator Direction         {elevator.Direction}");
                Console.WriteLine($"Elevator Capacity          {elevator.CurrentNumberOfPassengersOnBoard}");
                Console.WriteLine($"Elevator Floor No          {elevator.CurrentFloorNumber}");
                Console.WriteLine("---------------------------------------------------------");
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Update elevator movement
        /// </summary>
        /// <returns></returns>
        public Task UpdateElevatorStates()
        {
            foreach (var elevator in building.PassengerElevators)
            {
                if (elevator.Direction == ElevatorMovement.Up && elevator.CurrentFloorNumber < building.NumberOfFloors)
                {
                    elevator.MoveElevator(elevator.Direction);
                    if (elevator.CurrentFloorNumber == building.NumberOfFloors) elevator.ElevatorState = ElevatorState.Stationary;
                }
                else if (elevator.Direction == ElevatorMovement.Down && elevator.CurrentFloorNumber > 0)
                {
                    elevator.MoveElevator(elevator.Direction);
                    if (elevator.CurrentFloorNumber == 0) elevator.ElevatorState = ElevatorState.Stationary;
                }
                else
                {
                    throw new InvalidElevatorMoveException($"PassengerElevatorControlCenter - UpdateElevatorStates: The move was invalid for elevator {elevator.Id} at floor number{elevator.CurrentFloorNumber}");
                }
                var requests = elevator.RequestsOnBoard.Where(x => x.RequestedFloorNumber == elevator.CurrentFloorNumber).ToList();
                elevator.CurrentNumberOfPassengersOnBoard -= (requests != null && requests.Any()) ? requests.Sum(x=>x.NumberOfPassengers): 0;
            }

           return Task.CompletedTask;
        }

        public int GetBuildingNumberOfFloors()
        {
            lock(building)
            {
                return building.NumberOfFloors;
            }
        }
    }
}