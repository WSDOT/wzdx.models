using System;
using Wzdx.v4.Devices;
using Xunit;

namespace Wzdx.Models.Tests.v4
{
    public class FieldDeviceFeatureFactoryTests
    {
        [Fact]
        public void FactoryShouldSupportAllFieldDeviceTypes()
        {
            foreach (var deviceType in Enum.GetValues<FieldDeviceType>())
            {
                var source = Guid.NewGuid().ToString();
                var id = Guid.NewGuid().ToString();
                var feature = FieldDeviceFeatureFactory.CreateFeature(source, id, deviceType);
                
                Assert.NotNull(feature);
            }
        }
    }
}
