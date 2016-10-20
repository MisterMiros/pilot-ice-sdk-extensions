using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ascon.Pilot.SDK.Extensions.Exceptions;
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
            IAttribute attribute = dataObject.Type.GetAttribute(name);
            object value;
            if (!dataObject.Attributes.TryGetValue(name, out value) && attribute.IsObligatory)
            {
                throw new AttributeValueException(
                    $"У объекта не задан обязательный атрибут \"{name}\"", dataObject);
            }
            return value;
        }

        public static IEnumerable<IDataObject> GetAttributeDataObjects(this IDataObject dataObject, string name)
        {
            var config = dataObject.Type.GetAttributeConfiguration(name);
            string format = config.Attributes["StringFormat"].Value;
            string[] attrValues = GetAttributeValue(dataObject, name).ToString().Split(';');
            IEnumerable<IDataObject> possibleValues =
                Extensions.Repository.GetChildrenByQuery("/*", dataObject.Type.GetSourceForAttribute(name))
                .Where(dObj => dObj.Type.IsBase());
            string message = string.Empty;
            return from possibleValue in possibleValues
                   where attrValues.Contains(format.FormatFromDictionary(possibleValue.Attributes))
                   select possibleValue;
        }

        public static IEnumerable<IPerson> GetAttributePersons(this IDataObject dataObject, string name)
        {
            var config = dataObject.Type.GetAttributeConfiguration(name);
            if (config.Attributes["Kind"].Value != "OrgUnit")
            {
                throw new InvalidOperationException($"Аттрибут {name} не является ссылкой на организационную структуру");
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
    }
}