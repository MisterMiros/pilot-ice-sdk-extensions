using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Ascon.Pilot.SDK.Extensions.DeepCopies;

namespace Ascon.Pilot.SDK.Extensions
{
    public static class PersonGettingExtensions
    {
        public static IEnumerable<IPerson> GetPersonByPositionName(this IObjectsRepository repo, string name)
        {
            var persons = (from person in repo.GetPeople()
                           where !person.IsDeleted
                                 && person.Positions.Any()
                                 && person.Positions.Select(pos => pos.GetOrgUnit()).Any(ou => ou.Title == name)
                           select Extensions.CreateCopy(person));
            if (!persons.Any())
            {
                throw new NoPersonException($"Не удалось найти пользователей с должностью \"{ name }\"");
            }
            return persons;
        }

        public static IPerson GetPersonByDisplayName(this IObjectsRepository repo, string name)
        {
            var persons = (from person in repo.GetPeople()
                    where person.DisplayName == name
                    select Extensions.CreateCopy(person));
            if (!persons.Any())
            {
                throw new NoPersonException($"Не удалось найти пользователя с именем \"{ name }\"");
            }
            return Extensions.CreateCopy(persons.First());
        }

        public static IPerson GetPersonByLogin(this IObjectsRepository repo, string login)
        {
            var persons = (from person in repo.GetPeople()
                           where person.Login == login
                           select Extensions.CreateCopy(person));
            if (!persons.Any())
            {
                throw new NoPersonException($"Не удалось найти пользователя с логином \"{ login }\"");
            }
            return Extensions.CreateCopy(persons.First());

        }
    }
}
