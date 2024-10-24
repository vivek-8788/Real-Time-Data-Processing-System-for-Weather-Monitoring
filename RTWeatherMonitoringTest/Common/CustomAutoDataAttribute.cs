using AutoFixture;
using AutoFixture.Xunit2;

namespace RealTimeWeatherMonitoringTest.Common;

public class CustomAutoDataAttribute : AutoDataAttribute
{
    public CustomAutoDataAttribute(params Type[] customizationTypes)
        : base(() =>
        {
            var fixture = new Fixture();

            var customizations = customizationTypes
                .Select(t => Activator.CreateInstance(t) as ICustomization)
                .Where(c => c != null);

            foreach (var customization in customizations)
                fixture.Customize(customization);

            return fixture;
        })
    {
    }
}