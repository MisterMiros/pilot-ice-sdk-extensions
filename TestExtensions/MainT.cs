using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ascon.Pilot.SDK;
using Ascon.Pilot.Theme.Controls;
using SE = Ascon.Pilot.SDK.Extensions;
using Ascon.Pilot.SDK.Extensions;
using Ascon.Pilot.SDK.Extensions.DeepCopies;
using System.ComponentModel.Composition;
using System.Xml.Linq;
using System.Collections;

namespace TestExtensions
{
    [Export(typeof(IMainMenu))]
    public class MainT : IMainMenu
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
            Start();
        }

        public void Start()
        {
            Node root = new Node(_repo.GetRootObjectNew());
            string name = "0";
        }

        const string MENU_NAME = "testitem";

        public void OnMenuItemClick(string itemName)
        {
            if (itemName == MENU_NAME)
            {
                var window = new PureWindow();
                window.Content = new TestControl();
            }
        }

        public void BuildMenu(IMenuHost menuHost)
        {
            menuHost.AddItem(MENU_NAME, "Открыть окно", null, menuHost.GetItems().Count());
        }
    }

    class Node
    {
        public readonly IDataObject Object;
        public IEnumerable<Node> Children;

        public Node(IDataObject @object)
        {
            Object = @object.Copy();
            Children = @object.GetChildren().Select(child => new Node(child)).ToArray();
        }

        public int Count
        {
            get
            {
                return Children.Any() ? Children.Sum(node => node.Count + 1): 0;
            }
        }

        public override string ToString()
        {
            return $"{Object.DisplayName} - {Object.Type.Name} - {this.Count}";
        }
    }
}
