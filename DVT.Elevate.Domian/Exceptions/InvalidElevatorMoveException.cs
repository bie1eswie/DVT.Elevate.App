﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Exceptions
{
    public class InvalidElevatorMoveException: Exception
    {
        public InvalidElevatorMoveException(string message)
    : base(message)
        {
        }
    }
}
