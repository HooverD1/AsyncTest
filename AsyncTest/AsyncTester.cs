using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncTest
{
    public class AsyncTester
    {
        public Task<int> PrintEvens()
        {
            /*
             * Notice how this method contains a task definition with a return statement and a return statement for the method
             * The method returns type Task -- the work being done in this method gets defined as a Task (Task t1)...
             * ...The task returns a value of type int -- this is set where t1 is defined as "Task<int> t1"
             * Using "Task t1" would imply a void return type for the task!!
             * The return value of the task is called its "result". It's treated just like a return value that the "await PrintEvens()" resolves to.
             * The method's Task return type is returned here using "return t1". This hands off the tasks handle to the await statement which then resolves to the Task's return value once complete.
             */
            Task<int> t1 = Task.Factory.StartNew(() =>
            {
                int i;
                for (i = 0; i < 1000; i += 2)
                {
                    Console.WriteLine(i);
                }
                return i;
            });
            return t1;
        }

        public Task<int> PrintOdds()
        {
            Task<int> t1 = Task.Factory.StartNew(() =>
            {
                int i;
                for (i = 1; i < 1000; i += 2)
                {
                    Console.WriteLine(i);
                }
                return i;
            });
            return t1;
        }
        
    }
}
