using System;
using System.Collections.Generic;
using Wsdot.Wzdx.Core;
using Wsdot.Wzdx.v4.WorkZones;

namespace Wsdot.Wzdx.v4.Builders
{
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

        public WorkerPresenceBuilder WithConfidence(WorkerPresenceConfidence value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Confidence = value);
        }

        public WorkerPresenceBuilder WithNoConfidence()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Confidence = null);
        }
        
        public WorkerPresenceBuilder WithMethod(WorkerPresenceMethod value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Method = value);
        }

        public WorkerPresenceBuilder WithNoMethod()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Method = null);
        }

        public WorkerPresenceBuilder WithPresenceLastConfirmed(DateTimeOffset value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.WorkerPresenceLastConfirmedDate= value.UtcDateTime);
        }

        public WorkerPresenceBuilder WithNoPresenceLastConfirmed()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.WorkerPresenceLastConfirmedDate = null);
        }

        public WorkerPresenceBuilder WithDefinition(WorkerPresenceDefinition value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Definition.Add(value));
        }
        
        public WorkerPresenceBuilder WithoutDefinition(WorkerPresenceDefinition value)
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Definition.Remove(value));
        }
        
        public WorkerPresenceBuilder WithoutNoDefinition()
        {
            return new WorkerPresenceBuilder(Configuration, presence => presence.Definition.Clear());
        }

        protected override Func<WorkerPresence> ResultFactory { get; } = () => new WorkerPresence();
    }
}