using System;
using System.Collections.ObjectModel;
using Ascon.Pilot.SDK;

namespace Ascon.Pilot.SDK.Extensions.DeepCopies
{
    public class DeepPerson : DeepCopy<IPerson>, IPerson
    {
        private DeepPerson() { }
        private DeepPerson(IPerson original) : base(original)
        {
            ActualName = original.ActualName + string.Empty;
            DisplayName = original.DisplayName + string.Empty;
            Id = original.Id;
            MainPosition = DeepPosition.CreateCopy(original.MainPosition);
            Login = original.Login + string.Empty;
        }

        public static new IPerson CreateCopy(IPerson original)
        {
            if (original == null || original is DeepCopy<IPerson>)
            {
                return original;
            }
            return new DeepPerson(original);
        }

        public bool Equals(IPerson person)
        {
            return Id == person.Id;
        }

        public string ActualName
        {
            get; private set;
        }

        public string DisplayName
        {
            get; private set;
        }

        public int Id
        {
            get; private set;
        }

        public IPosition MainPosition
        {
            get; private set;
        }

        public string Login
        {
            get; private set;
        }

        public string Comment
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsAdmin
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsDeleted
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ReadOnlyCollection<IPosition> Positions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Sid
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
