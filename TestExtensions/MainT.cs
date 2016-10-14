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
            var objs = //Extensions.Repository.Get(">projectfolder").ToArray(); 
            Extensions.Repository
            .GetType("project")
            .GetAttributePossibleValues("customer")
            .Where(obj => obj.State == DataState.Loaded)
            .ToArray();
            var tasks = Extensions.Repository.GetTasks().ToArray();
            string name = "0";
        }
    }
}
