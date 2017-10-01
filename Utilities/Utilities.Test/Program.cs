using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Mongo;
using Utilities.Mongo.Models;

namespace Utilities.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoContext context = new MongoContext("MessageSendingSystem");
            Test test = new Test();
            test._id = "asdfasdfasdf";
            test.Name = "1234";
            test.Employee = new Employee();
            test.Employee.id = "adsf";
            test.Employee.Address = "1234r";
            context.Add<Test>(test);
        }
    }

    public class Test : EntityModel
    {
        public string Name { get; set; }

        public Employee Employee { get; set; }
    }

    public class Employee
    {
        public string id { get; set; }

        public string Address { get; set; }
    }
}
