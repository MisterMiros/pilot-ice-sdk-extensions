using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using Ascon.Pilot.SDK.Extensions;
using Ascon.Pilot.SDK.Extensions.DeepCopies;
using System.ComponentModel.Composition;

namespace TestExtensions
{
    [Export(typeof(IDataPlugin))]
    public class MainT : IDataPlugin
    {
        IObjectsRepository _repo;

        [ImportingConstructor]
        public MainT(IObjectsRepository repo, IAttributeFormatParser parser)
        {
            _repo = repo;
            Extensions.AttributeFormatParser = parser;
            Extensions.Repository = repo;
            Extensions.UseDeepCopies = true;
            Extensions.Start(Start);
        }

        public void Start()
        {
            var plans = _repo.GetChildrenByQuery("/RPM_Plan").ToArray();
            string name = "0";
        }
    }
}
