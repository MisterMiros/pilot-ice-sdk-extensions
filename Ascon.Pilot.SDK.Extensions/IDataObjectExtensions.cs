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

        public static IEnumerable<IDataObject> GetAttributeRefObjects(this IDataObject dataObject, string name)
        {
            string values = GetAttributeValue(dataObject, name) as string;
            if (values == null)
            {
                throw new AttributeValueException($"У объекта неверно задан атрибут \"{name}\"", dataObject);
            }

            XmlElement configuration = dataObject.Type.GetAttributeConfiguration(name);
            string format;
            try
            {
                format = configuration.Attributes["StringFormat"].Value;
            }
            catch
            {
                throw new AttributeConfigurationException($"У аттрибут \"{name}\" неверно указан параметр StringFormat", dataObject.Type);
            }


            IDataObject source = dataObject.Type.GetSourceForAttribute(name);
            ObjectGetter<IDataObject> getter = new ObjectGetter<IDataObject>();
            IEnumerable<IDataObject> refObjects =
                getter.GetObjects(source.Id.ToArray(), OnlyEmtpyTypesSearcher.GetInstance());

            string message = string.Empty;
            foreach (string value in values.Split(';'))
            {
                foreach (var refObject in refObjects)
                {
                    string objName = string.Empty;
                    try { objName = format.FormatFromDictionary(refObject.Attributes); }
                    catch { continue; }
                    if (value == objName)
                    {
                        yield return refObject;
                        break;
                    }
                }
            }
        }

        private class OnlyEmtpyTypesSearcher : IObjectSearcher<IDataObject>
        {
            private static OnlyEmtpyTypesSearcher _instance = new OnlyEmtpyTypesSearcher();

            private OnlyEmtpyTypesSearcher() { }

            public static OnlyEmtpyTypesSearcher GetInstance()
            {
                return _instance;
            }

            public IEnumerable<IDataObject> SearchNext(IDataObject @object, ObjectGetter<IDataObject> getter)
            {
                if (@object.Type.Children.Any())
                {
                    foreach (IDataObject child in getter.GetObjects(@object.Children, this))
                    {
                        yield return child;
                    }
                }
                else
                {
                    yield return @object;
                }
            }
        }
    }
}