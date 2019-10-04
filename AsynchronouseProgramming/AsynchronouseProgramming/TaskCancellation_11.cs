
namespace AsynchronouseProgramming
{
    /// <summary>
    /// The general pattern for implementing the cooperative cancellation model is:
    ///  Instantiate a CancellationTokenSource object, which manages and sends cancellation notification to the individual cancellation tokens.
    ///   Pass the token returned by the CancellationTokenSource.Token property to each task or thread that listens for cancellation.
    ///  Call the CancellationToken.IsCancellationRequested method from operations that receive the cancellation token.Provide a mechanism for each task or thread to respond to a cancellation request.Whether you choose to cancel an operation, and exactly how you do it, depends on your application logic.
    //  Call the CancellationTokenSource.Cancel method to provide notification of cancellation.This sets the CancellationToken.IsCancellationRequested property on every copy of the cancellation token to true.
    //  Call the Dispose method when you are finished with the CancellationTokenSource object.
    /// </summary>
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main()
        {
            //Show the state of task when using same tokensource or different one.
            var tokenSource = new CancellationTokenSource();
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

            var task = Task.Run(() =>
            {
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {
                    if (ct.IsCancellationRequested)
                    {
                        Console.WriteLine("Executed the loop");
                        ct.ThrowIfCancellationRequested();
                    }

                }
            }, tokenSource2.Token);


            tokenSource.Cancel();

            try
            {
                var x = task;
                task.GetAwaiter().GetResult();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"General exception thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource.Dispose();
            }

            Console.ReadKey();
        }
    }
}