﻿using System;
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
            Extensions.Initialize(repo, parser);
            Extensions.UseDeepCopies = true;
            Extensions.Start(Start);
        }

        public void Start()
        {
            var objs = Extensions.Repository.GetChildrenByQuery("/project").Select(obj => DeepDataObject.CreateCopy(obj)).ToArray();
            var people = _repo.GetPeople().Select(person => DeepPerson.CreateCopy(person)).ToArray();
            var org = _repo.GetOrganisationUnits().Select(ou => DeepOrganisationUnit.CreateCopy(ou)).ToArray();
            string name = "0";
        }
    }
}
