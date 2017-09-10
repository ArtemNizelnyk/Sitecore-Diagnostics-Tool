using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sitecore.Diagnostics.Base;
using Sitecore.Diagnostics.Objects;
using Sitecore.DiagnosticsTool.Core.Categories;
using Sitecore.DiagnosticsTool.Core.Tests;
using Sitecore.DiagnosticsTool.Tests.ECM.Helpers;

namespace Sitecore.DiagnosticsTool.Tests.ECM
{
    public class DefaultDefinitionDatabase : Test
    {
        public override string Name => "Default Definition Database should be switched to 'web' on CD servers";

        public override IEnumerable<Category> Categories => new[] { Category.Ecm };
        public override IEnumerable<ServerRole> ServerRoles => new[] { ServerRole.ContentDelivery };

        public override bool IsActual(IReadOnlyCollection<ServerRole> roles, ISitecoreVersion sitecoreVersion, ITestResourceContext data)
        {
            return sitecoreVersion.Major >= 8 && ServerRoles.Intersect(roles).Any();
        }

        [NotNull]
        protected string ErrorMessage => "'web' database should be marked as a Default Definition Database on CD.";

        public override void Process(ITestResourceContext data, ITestOutputContext output)
        {
            Assert.ArgumentNotNull(data, nameof(data));

            if (data.SitecoreInfo.GetSetting("Analytics.DefaultDefinitionDatabase") != "web")
            {
                output.Warning(ErrorMessage);
            }
        }
    }
}
