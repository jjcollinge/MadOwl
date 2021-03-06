﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;

namespace Student.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IStudent : IActor
    {

        Task<string> GetUsernameAsync();
        Task SetUsernameAsync(string username);

        Task<int> GetCurrentStepAsync();
        Task SetCurrentStepAsync(int currentStep);
        
    }
}
