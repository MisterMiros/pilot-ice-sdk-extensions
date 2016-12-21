using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using SE = Ascon.Pilot.SDK.Extensions;
using Ascon.Pilot.SDK.Extensions;
using Ascon.Pilot.SDK.Extensions.DeepCopies;
using System.ComponentModel.Composition;
using System.Xml.Linq;

namespace TestExtensions
{
    [Export(typeof(IDataPlugin))]
    public class MainT : IDataPlugin
    {
        IObjectsRepository _repo;
        IObjectModifier _om;

        [ImportingConstructor]
        public MainT(IObjectsRepository repo, IAttributeFormatParser parser, IObjectModifier om)
        {
            _repo = repo;
            _om = om;
            SE.Extensions.AttributeFormatParser = parser;
            SE.Extensions.Repository = repo;
            SE.Extensions.Start(Start);
        }

        public void Start()
        {
            /*var folders = _repo.GetChildrenByQuery("/RPM_Plan_Folder").ToArray();
            var children = folders.ToDictionary(folder => folder, folder => folder.GetChildren().ToArray());
            var plan = _repo.GetChildrenByQuery("/RPM_Plan").Where(obj => obj.DisplayName.Contains("Тестовый план 2")).First();
            var doc = XElement.Parse(plan.Attributes["Xml"] as string);
            doc.Element("items").RemoveAll();
            var builder = _om.EditById(plan.Id);
            builder.SetAttribute("Xml", doc.ToString(SaveOptions.DisableFormatting));
            _om.Apply();*/
            var tasks = _repo.GetTasks().Select(task => task.Copy()).ToArray();
            string name = "0";
        }
    }
}
