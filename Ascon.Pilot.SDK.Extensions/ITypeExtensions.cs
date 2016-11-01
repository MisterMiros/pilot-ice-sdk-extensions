﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class ITypeExtensions
    {
        public static bool CanContain(this IType type, string name)
        {
            int id = Extensions.Repository.GetType(name).Id;
            return CanContain(type, id);
        }

        public static bool CanContain(this IType type, IType child)
        {
            return CanContain(type, child.Id);
        }

        public static bool CanContain(this IType type, int id)
        {
            if (type.Id == id) { return type.Children.Contains(id); }
            return CanContainRec(type, id);
        }

        private static bool CanContainRec(IType type, int id)
        {
            if (type.Id == id || type.Children.Contains(id)) { return true; }
            foreach (int childId in type.Children)
            {
                if (childId == type.Id) { continue; }
                IType child = Extensions.Repository.GetType(childId);
                if (CanContainRec(child, id))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool HasChild(this IType type, string name)
        {
            int id = Extensions.Repository.GetType(name).Id;
            return HasChild(type, id);
        }

        public static bool HasChild(this IType type, IType child)
        {
            return HasChild(type, child.Id);
        }

        public static bool HasChild(this IType type, int id)
        {
            return type.Children.Contains(id);
        }

        public static bool IsBase(this IType type)
        {
            return !type.Children.Any();
        }

        public static IAttribute GetAttribute(this IType type, string name)
        {
            IAttribute attribute = type.Attributes.FirstOrDefault((attr) => attr.Name == name);
            if (attribute == null)
            {
                throw new NoAttributeException
                    ($"Тип \"{type.Title}\" не содержит атрибут $\"{name}\"",
                    type);
            }
            return attribute;
        }

        public static XmlElement GetAttributeXMLConfiguration(this IType type, string name)
        {
            IAttribute attribute = type.GetAttribute(name);
            if (string.IsNullOrWhiteSpace(attribute.Configuration))
            {
                throw new AttributeConfigurationException($"Атрибут \"{attribute.Name}\" не содержит дополнительные параметры", type);
            }
            XmlElement configuration;
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(attribute.Configuration);
                configuration = doc.DocumentElement;
            }
            catch
            {
                throw new AttributeConfigurationException($"У аттрибута \"{attribute.Name}\" неверно сформированы дополнительные параметры", type);
            }
            return configuration;
        }

        public static IReferenceBookConfiguration GetAttributeConfiguration(this IType type, string name)
        {
            IAttribute attribute = type.GetAttribute(name);
            if (string.IsNullOrWhiteSpace(attribute.Configuration))
            {
                throw new AttributeConfigurationException($"Атрибут \"{attribute.Name}\" не содержит дополнительные параметры", type);
            }
            IReferenceBookConfiguration configuration;
            if (!Extensions.AttributeFormatParser
                .TryParseReferenceBookConfiguration(attribute.Configuration, out configuration))
            {
                throw new AttributeConfigurationException($"У аттрибута \"{attribute.Name}\" неверно сформированы дополнительные параметры", type);
            }
            return configuration;
        }

        public static IDataObject GetSourceForAttribute(this IType type, string name)
        {
            Guid source;
            if (Extensions.AttributeFormatParser == null)
            {
                XmlElement configuration = GetAttributeXMLConfiguration(type, name);
                if (!configuration.HasAttribute("Kind")
                    || configuration.Attributes["Kind"].Value != "Object")
                {
                    throw new AttributeConfigurationException($"Аттрибут \"{name}\" не ссылается на объект", type);
                }
                try { source = new Guid(configuration.Attributes["Source"].Value); }
                catch
                {
                    throw new AttributeConfigurationException($"У аттрибута \"{name}\" неверно указан параметр Source", type);
                }
            }
            else
            {
                IReferenceBookConfiguration configuration = GetAttributeConfiguration(type, name);
                if (!(configuration.Kind == RefBookKind.Object))
                {
                    throw new AttributeConfigurationException($"Аттрибут \"{name}\" не ссылается на объект", type);
                }
                source = configuration.Source;
            }                  
            return Extensions.Repository.Get<IDataObject>(source);
        }
    }
}
