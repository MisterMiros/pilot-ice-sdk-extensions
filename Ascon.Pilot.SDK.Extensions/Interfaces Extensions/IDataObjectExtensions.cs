﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class IDataObjectExtensions
    {
        public static ITaskObject ToTask(this IDataObject dataObject)
        {
            if (dataObject.Type.Name == SystemTypeNames.TASK)
            {
                return Extensions.Repository.Get<ITaskObject>(dataObject.Id);
            }
            throw new InvalidCastException("Невозможно преобразовать объект в задачу");
        }

        public static object GetAttributeValue(this IDataObject dataObject, string name)
        {
            object value = null;
            dataObject.Attributes.TryGetValue(name, out value);
            return value;
        }

        public static IEnumerable<IDataObject> GetAttributeDataObjects(this IDataObject dataObject, string name)
        {
            string format;
            if (Extensions.AttributeFormatParser == null)
            {
                var config = dataObject.Type.GetAttributeXMLConfiguration(name);
                format = config.Attributes["StringFormat"].Value;
            }
            else
            {
                var config = dataObject.Type.GetAttributeConfiguration(name);
                format = config.StringFormat;
            }
            string[] attrValues = GetAttributeValue(dataObject, name).ToString().Split(';');
            IEnumerable<IDataObject> possibleValues =
                Extensions.Repository.GetChildrenByQuery("/*", dataObject.Type.GetSourceForAttribute(name))
                .Where(dObj => dObj.Type.IsBase());
            string message = string.Empty;
            return from possibleValue in possibleValues
                   where attrValues.Contains(possibleValue.FormatAttributes(format))
                   select possibleValue;
        }

        public static IEnumerable<IPerson> GetAttributePersons(this IDataObject dataObject, string name)
        {
            if (Extensions.AttributeFormatParser == null)
            {
                var config = dataObject.Type.GetAttributeXMLConfiguration(name);
                if (!config.HasAttribute("Kind") || config.Attributes["Kind"].Value != "OrgUnit")
                {
                    throw new InvalidOperationException($"Аттрибут {name} не является ссылкой на организационную структуру");
                }
            }
            else
            {
                var config = dataObject.Type.GetAttributeConfiguration(name);
                if (config.Kind != RefBookKind.OrgUnit)
                {
                    throw new InvalidOperationException($"Аттрибут {name} не является ссылкой на организационную структуру");
                }
            }
            string[] attrValues = GetAttributeValue(dataObject, name).ToString().Split(';');
            return from person in Extensions.Repository.GetPeople()
                   where attrValues.Contains(person.ActualName)
                   select Extensions.CreateCopy(person);
        }

        public static IEnumerable<IDataObject> GetChildren(this IDataObject dataObject)
        {
            return Extensions.Repository.Get<IDataObject>(dataObject.Children);
        }

        public static IDataObject GetParent(this IDataObject dataObject)
        {
            return Extensions.Repository.Get<IDataObject>(dataObject.ParentId);
        }

        public static string FormatAttributes(this IDataObject dataObject, string format)
        {
            if (Extensions.AttributeFormatParser == null)
            {
                return format.FormatFromDictionary(dataObject.Attributes);
            }
            else
            {
                return Extensions.AttributeFormatParser.AttributesFormat(format, dataObject.Attributes as Dictionary<string, object>);
            }
        }
    }
}