using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.Diagnostics.Objects;
using Sitecore.DiagnosticsTool.Core.Categories;
using Sitecore.DiagnosticsTool.TestRunner;
using Sitecore.DiagnosticsTool.TestRunner.Base;
using Sitecore.DiagnosticsTool.Tests.ECM;
using Sitecore.DiagnosticsTool.Tests.UnitTestsHelper;
using Sitecore.DiagnosticsTool.Tests.UnitTestsHelper.Context;
using Sitecore.DiagnosticsTool.Tests.UnitTestsHelper.Resources;
using Xunit;

namespace Sitecore.DiagnosticsTool.Tests.UnitTests.ECM
{
    [TestClass]
    public class DefaultDefinitionDatabaseTests : DefaultDefinitionDatabase
    {
        [Fact]
        public void TestPassed()
        {
            var sitecoreConfiguration = new SitecoreInstance
            {
                ServerRoles = new[] { ServerRole.ContentDelivery },
                Configuration = new XmlDocument().Create("/configuration/sitecore/settings/setting[@name='Analytics.DefaultDefinitionDatabase' and @value='web']"),
                Version = new SitecoreVersion(8, 2, 3, 170407)
            };

            UnitTestContext
                .Create(this)
                .AddResource(sitecoreConfiguration)
                .Process(this)
                .Done();

        }

        [Fact]
        public void TestFailed()
        {
            var sitecoreConfiguration = new SitecoreInstance
            {
                ServerRoles = new[] { ServerRole.ContentDelivery },
                Configuration = new XmlDocument().Create("/configuration/sitecore/settings/setting[@name='Analytics.DefaultDefinitionDatabase' and @value='master']"),
                Version = new SitecoreVersion(8, 2, 3, 170407)
            };

            UnitTestContext
                .Create(this)
                .AddResource(sitecoreConfiguration)
                .Process(this)
                .MustReturn(new TestOutput(TestResultState.Warning, ErrorMessage))
                .Done();
        }
    }
}
