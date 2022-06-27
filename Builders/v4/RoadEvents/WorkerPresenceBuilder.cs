using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.RoadEvents
{
    /// <summary>
    /// Provides an immutable builder of a v4 WorkerPresence class
    /// </summary>
    public sealed class WorkerPresenceBuilder : Builder<WorkerPresence>
    {
        public WorkerPresenceBuilder(bool arePresent) :
            this(new List<Action<WorkerPresence>>(), presence => presence.AreWorkersPresent = arePresent)
        {
            
        }

        private WorkerPresenceBuilder(IEnumerable<Action<WorkerPresence>> configuration, Action<WorkerPresence> step) : 
            base(configuration, step)
        {

        }

        [Pure]
        public WorkerPresenceBuilder WithConfidence(WorkerPresenceConfidence value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Confidence = value);
        }

        [Pure]
        public WorkerPresenceBuilder WithNoConfidence()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Confidence = null);
        }

        [Pure]
        public WorkerPresenceBuilder WithMethod(WorkerPresenceMethod value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Method = value);
        }

        [Pure]
        public WorkerPresenceBuilder WithNoMethod()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Method = null);
        }

        [Pure]
        public WorkerPresenceBuilder WithPresenceLastConfirmed(DateTimeOffset value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.WorkerPresenceLastConfirmedDate= value.UtcDateTime);
        }

        [Pure]
        public WorkerPresenceBuilder WithNoPresenceLastConfirmed()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.WorkerPresenceLastConfirmedDate = null);
        }

        [Pure]
        public WorkerPresenceBuilder WithDefinition(WorkerPresenceDefinition value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Definition.Add(value));
        }

        [Pure]
        public WorkerPresenceBuilder WithoutDefinition(WorkerPresenceDefinition value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Definition.Remove(value));
        }

        [Pure]
        public WorkerPresenceBuilder WithoutNoDefinition()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Definition.Clear());
        }

        protected override Func<WorkerPresence> ResultFactory { get; } = () => new WorkerPresence();
    }
}