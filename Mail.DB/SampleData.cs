using Mail.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DB
{
    public class SampleData
    {
        public static void Initialize(DataContext context)
        {
            if (!context.Users.Any() || !context.Groups.Any())
            {
                User[] users = new User[] {
                     new User{ Name = "TestName1", Surname = "TestSurname1", Email = "iamanton45@gmil.com" },
                     new User{ Name = "TestName2", Surname = "TestSurname2", Email = "iamanton@ukr.net" },
                };

                Group[] groups = new Group[] {
                    new Group {Name = "Oll Users" },
                    new Group {Name = "Test Name"}
                };

                context.Groups.AddRange(groups);
                context.Users.AddRange(users);

                //users[0].Groups.Add(groups[0]);
                //users[1].Groups.Add(groups[0]);
                //users[0].Groups.Add(groups[1]);

                context.SaveChanges();
            }
        }
    }
}
