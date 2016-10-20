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
        public MainT(IObjectsRepository repo)
        {
            _repo = repo;
            Extensions.Initialize(repo, null);
            Extensions.UseDeepCopies = true;
            Extensions.Start(Start);
        }

        public void Start()
        {
            var objs = Extensions.Repository.GetChildrenByQuery("/project").ToArray();
            var customers = (from obj in objs
                            select obj.GetAttributeDataObjects("customer").ToArray()).ToArray();
            var gips = (from obj in objs
                        select obj.GetAttributePersons("GIP").ToArray()).ToArray();
            string name = "0";
        }
    }
}
