using System;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides a builder for a v4 WorkerPresence class
    /// </summary>
    public sealed class WorkerPresenceBuilder : IBuilder<WorkerPresence>
    {
        private BuilderConfiguration<WorkerPresence> Configuration { get; }
            = new BuilderConfiguration<WorkerPresence>();

        public WorkerPresenceBuilder(bool arePresent)
        {
            Configuration.Set(presence => presence.AreWorkersPresent, arePresent);
        }

        public WorkerPresenceBuilder WithConfidence(WorkerPresenceConfidence value)
        {
            Configuration.Set(presence => presence.Confidence, value);
            return this;
        }

        public WorkerPresenceBuilder WithNoConfidence()
        {
            Configuration.Default(presence => presence.Confidence);
            return this;
        }

        public WorkerPresenceBuilder WithMethod(WorkerPresenceMethod value)
        {
            Configuration.Set(presence => presence.Method, value);
            return this;
        }

        public WorkerPresenceBuilder WithNoMethod()
        {
            Configuration.Default(presence => presence.Method);
            return this;
        }

        public WorkerPresenceBuilder WithPresenceLastConfirmed(DateTimeOffset value)
        {
            Configuration.Set(presence => presence.WorkerPresenceLastConfirmedDate, value.ToUniversalTime());
            return this;
        }

        public WorkerPresenceBuilder WithNoPresenceLastConfirmed()
        {
            Configuration.Default(presence => presence.WorkerPresenceLastConfirmedDate);
            return this;
        }

        public WorkerPresenceBuilder WithDefinition(WorkerPresenceDefinition value)
        {
            Configuration.Combine(presence => presence.Definition, presence => presence.Definition.Add(value));
            return this;
        }

        public WorkerPresenceBuilder WithoutDefinition(WorkerPresenceDefinition value)
        {
            Configuration.Combine(presence => presence.Definition, presence => presence.Definition.Remove(value));
            return this;
        }

        public WorkerPresenceBuilder WithoutNoDefinition()
        {
            Configuration.Default(presence => presence.Definition);
            return this;
        }
        
        [Pure]
        public WorkerPresence Result()
        {
            var result = new WorkerPresence();
            Configuration.ApplyTo(result);
            return result;
        }
    }
}