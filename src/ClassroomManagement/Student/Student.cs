﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using Student.Interfaces;

namespace Student
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class Student : Actor, IStudent
    {

        public string Username { get; set; }
        public int CurrentStep { get; set; }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see http://aka.ms/servicefabricactorsstateserialization

            if (!this.StateManager.TryGetStateAsync<string>("Username").Result.HasValue)
            {

                this.StateManager.SetStateAsync<string>("Username", "");
                this.StateManager.SetStateAsync<int>("CurrentStep", 0);

            }

            return this.StateManager.TryAddStateAsync("count", 0);
        }
        

        public Task<string> GetUsernameAsync()
        {
            return this.StateManager.GetStateAsync<string>("Username");
        }

        public Task SetUsernameAsync(string username)
        {
            return this.StateManager.SetStateAsync<string>("Username", username);
        }

        public Task<int> GetCurrentStepAsync()
        {
            return this.StateManager.GetStateAsync<int>("CurrentStep");
        }

        public Task SetCurrentStepAsync(int currentStep)
        {
            return this.StateManager.SetStateAsync<int>("CurrentStep", currentStep);

        }
    }
}
