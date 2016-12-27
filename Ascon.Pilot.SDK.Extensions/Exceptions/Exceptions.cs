using Ascon.Pilot.SDK.Extensions.DeepCopies;
using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Ascon.Pilot.SDK.Extensions
{
    [Serializable]
    public class NoPersonException : Exception
    {
        public NoPersonException() : base() { }
        public NoPersonException(string message) : base(message) { }
        public NoPersonException(string message, Exception innerException) : base(message, innerException) { }
        protected NoPersonException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class DataObjectException : Exception
    {
        readonly IDataObject _dataObject;
        public IDataObject DataObject
        {
            get
            {
                return _dataObject;
            }
        }

        protected const string guidName = "Data Object Guid";

        public DataObjectException(IDataObject dataObject) : base()
        {
            _dataObject = DeepDataObject.CreateCopy(dataObject);
        }
        public DataObjectException(string message, IDataObject dataObject, Exception innerException) : base(message, innerException)
        {
            _dataObject = DeepDataObject.CreateCopy(dataObject);
        }
        public DataObjectException(string message, IDataObject dataObject) : base(message)
        {
            _dataObject = DeepDataObject.CreateCopy(dataObject);
        }
        protected DataObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Guid id = new Guid(info.GetString(guidName));
            _dataObject = DeepDataObject.CreateCopy(Extensions.Repository.Get<IDataObject>(id));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Data Object Title", _dataObject.DisplayName);
            info.AddValue(guidName, _dataObject.Id);
            info.AddValue("Data Object Type", _dataObject.Type.Name);
            base.GetObjectData(info, context);
        }
    }

    [Serializable]
    public class AttributeValueException : DataObjectException
    {
        public AttributeValueException(IDataObject dataObject) : base(dataObject) { }
        public AttributeValueException(string message, IDataObject dataObject) : base(message, dataObject) { }
        public AttributeValueException(string message, IDataObject dataObject, Exception innerException) : base(message, dataObject, innerException) { }
        protected AttributeValueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class TypeException : Exception
    {
        readonly IType _type;
        public IType Type
        {
            get
            {
                return _type;
            }
        }

        protected const string idName = "Type Id";

        public TypeException(IType type) : base()
        {
            _type = DeepType.CreateCopy(type);
        }
        public TypeException(string message, IType type) : base(message)
        {
            _type = DeepType.CreateCopy(type);
        }
        public TypeException(string message, IType type, Exception innerException) : base(message, innerException)
        {
            _type = DeepType.CreateCopy(type);
        }
        protected TypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _type = DeepType.CreateCopy(Extensions.Repository.GetType(info.GetInt32(idName)));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Type Name", _type.Name);
            info.AddValue("Type Title", _type.Title);
            info.AddValue(idName, _type.Id);
            base.GetObjectData(info, context);
        }

    }

    [Serializable]
    public class NoAttributeException : TypeException
    {
        public NoAttributeException(IType type) : base(type) { }
        public NoAttributeException(string message, IType type) : base(message, type) { }
        public NoAttributeException(string message, IType type, Exception innerException) : base(message, type, innerException) { }
        protected NoAttributeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class AttributeConfigurationException : TypeException
    {
        public AttributeConfigurationException(IType type) : base(type) { }
        public AttributeConfigurationException(string message, IType type) : base(message, type) { }
        public AttributeConfigurationException(string message, IType type, Exception innerException) : base(message, type, innerException) { }
        protected AttributeConfigurationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class TaskObjectException : Exception
    {
        readonly ITaskObject _task;
        public ITaskObject Task
        {
            get
            {
                return _task;
            }
        }

        protected const string guidName = "Task Object Guid";

        public TaskObjectException(ITaskObject task) : base()
        {
            _task = DeepTaskObject.CreateCopy(task);
        }
        public TaskObjectException(string message, ITaskObject task) : base(message)
        {
            _task = DeepTaskObject.CreateCopy(task);
        }
        public TaskObjectException(string message, ITaskObject task, Exception innerException) : base(message, innerException)
        {
            _task = DeepTaskObject.CreateCopy(task);
        }
        protected TaskObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Guid id = new Guid(info.GetString(guidName));
            _task = DeepTaskObject.CreateCopy(Extensions.Repository.Get<ITaskObject>(id));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue(guidName, _task.Id);
            info.AddValue("Task Object Title", _task.Title ?? string.Empty);
            info.AddValue("Task Object Initiator Login", _task.Initiator?.Login ?? string.Empty);
            info.AddValue("Task Object Executor Login", _task.Executor?.Login ?? string.Empty);
            info.AddValue("Task Object Description", _task.Description ?? string.Empty);
            info.AddValue(guidName, _task.Id);
            base.GetObjectData(info, context);
        }
    }

    [Serializable]
    public class PersonException : Exception
    {
        readonly IPerson _person;
        public IPerson Person
        {
            get
            {
                return _person;
            }
        }

        protected const string idName = "Person Id";

        public PersonException(IPerson person) : base()
        {
            _person = DeepPerson.CreateCopy(person);
        }
        public PersonException(string message, IPerson person) : base(message)
        {
            _person = DeepPerson.CreateCopy(person);
        }
        public PersonException(string message, IPerson person, Exception innerException) : base(message, innerException)
        {
            _person = DeepPerson.CreateCopy(person);
        }
        protected PersonException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _person = Extensions.Repository.GetPerson(info.GetInt32("idName"));
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Person Name", _person.DisplayName ?? string.Empty);
            info.AddValue("Person Login", _person.Login);
            info.AddValue(idName, _person.Id);
            base.GetObjectData(info, context);
        }
    }
}
