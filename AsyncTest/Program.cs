using System;
using System.Threading.Tasks;

namespace AsyncTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //RunTests is an async method, so calling it here does not block the main thread
            Task.WaitAll(RunTests());     //This line runs an async thread - so control continues on from here while the await lines in RunTests() run asynchronously 
            
            //!!!The non-await lines seem to run on the main thread? Or at least execute as though they do.
            //!!!Using Task.WaitAll() or Task.WaitAny() is like thread.join() -- it blocks the calling thread until the task completes
            Console.WriteLine("Last Main Line");
            Console.Read();
        }
        private static async Task RunTests()
        {
            //Use async method for methods you want to run as though they're on a separate thread.
            //Merge threads back together by returning as Task type and using Tasks.WaitAll()
            //Returning Task is rather odd - there is NO return statement in this method, yet I am returning Task
            //Returning void works, but then you have no handle for the thread to return to use Task.WaitAll() with
            
            AsyncTester tester = new AsyncTester();
            // await keyword can be placed before Task objects to run them on a threadpool thread
            // the async keyword on a method allows for the use of the await keyword
            // the async method - with or without await keywords - will internally run synchronously i.e. commands in series within the method
            // outside the async method, the async method caller runs its await statements (and anything after them) asynchronously
            Console.WriteLine("Is this synchronous?");  //This runs before "Last Main Line" -- implying it runs on the main thread
            int testNumber = await tester.PrintEvens();      //Await has to await a task -- so here, PrintEvens() returns a Task object
            //Await requires an "awaitable" -- as far as I can tell this is just a fancy way to say you have to use a Task with it.
            int testNumber2 = await tester.PrintOdds();     //After printing evens, this method moves synchronously onto printing odds
            Console.WriteLine("Last run tests line");   //this line executes after PrintOdds() -- basically you're queueing up things synchronously in a separate thread than the thread in which RunTests() was called

        }
    }
}
