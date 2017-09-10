using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Diagnostics.Base;
using Sitecore.Diagnostics.Objects;
using Sitecore.DiagnosticsTool.Core.Categories;
using Sitecore.DiagnosticsTool.Core.Output;
using Sitecore.DiagnosticsTool.Core.Tests;
using Sitecore.DiagnosticsTool.Tests.General.Health;

namespace Sitecore.DiagnosticsTool.Tests.ECM
{
    public class ListManagement : Test
    {
        public override IEnumerable<Category> Categories => new[] { Category.Ecm };

        public override string Name => "ListManagement disabled on CD";

        public override IEnumerable<ServerRole> ServerRoles => new[] { ServerRole.ContentDelivery };

        public override bool IsActual(IReadOnlyCollection<ServerRole> roles, ISitecoreVersion sitecoreVersion, ITestResourceContext data)
        {
            return sitecoreVersion.Major>=8 && ServerRoles.Intersect(roles).Any();
        }

        public override void Process(ITestResourceContext data, ITestOutputContext output)
        {
            IList<string> filesToBeDisabled=new List<string>();
            Assert.ArgumentNotNull(data, nameof(data));
            foreach (var filePath in data.SitecoreInfo.IncludeFiles.Keys)
            {
                if (filePath.Contains("Sitecore.Listmanagement"))
                {
                    filesToBeDisabled.Add(filePath);
                }
            }
            if (filesToBeDisabled.Count>0)
            {
             output.Error(ErrorMessage(filesToBeDisabled));   
            }
            
        }

        private ShortMessage ErrorMessage(IList<string> filesToBeDisabled)
        {
            return "Files in App_Cofig\\Include\\ListManagement\\ folder should be disabled.";
        }
    }
}
