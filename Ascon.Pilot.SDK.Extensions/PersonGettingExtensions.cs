using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ascon.Pilot.SDK.Extensions.DeepCopies;
using Ascon.Pilot.SDK.Extensions.Exceptions;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class PersonGettingExtensions
    {
        public static IEnumerable<IPerson> GetPersonByPositionName(this IObjectsRepository repo, string name)
        {
            var persons = (from person in repo.GetPeople()
                           where person.IsDeleted
                                 && person.MainPosition != null
                                 && person.MainPosition.GetOrgUnit().Title == name
                           select Extensions.CreateCopy(person));
            if (!persons.Any())
            {
                throw new NoPersonException($"Не удалось найти пользователей с должностью \"{ name }\"");
            }
            return persons;
        }

        public static IPerson GetPersonByActualName(this IObjectsRepository repo, string name)
        {
            var persons = (from person in repo.GetPeople()
                    where person.ActualName == name
                    select person);
            if (!persons.Any())
            {
                throw new NoPersonException($"Не удалось найти пользователя с именем \"{ name }\"");
            }
            return Extensions.CreateCopy(persons.First());
        }
    }
}
