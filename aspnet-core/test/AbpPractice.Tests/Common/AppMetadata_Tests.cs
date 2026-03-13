using Shouldly;
using System;
using Xunit;

namespace AbpPractice.Tests.Common;

public class AppMetadata_Tests
{
    [Fact]
    public void Version_Should_Be_Set()
    {
        AppVersionHelper.Version.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ReleaseDate_Should_Not_Be_In_The_Future()
    {
        AppVersionHelper.ReleaseDate.ShouldBeLessThanOrEqualTo(DateTime.Now);
    }

    [Fact]
    public void MultiTenantFact_Should_Not_Be_Skipped_When_MultiTenancy_Is_Enabled()
    {
        AbpPracticeConsts.MultiTenancyEnabled.ShouldBeTrue();

        var attribute = new MultiTenantFactAttribute();
        attribute.Skip.ShouldBeNull();
    }
}
