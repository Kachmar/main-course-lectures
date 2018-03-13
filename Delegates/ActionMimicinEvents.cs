using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Delegates
{
    public class ActionMimicinEvents
    {
        static void Main(string[] args)
        {
            var smsSender = new SmsSender();
            var emailSender = new EmailSender();
            JokeGenerator jokeGenerator = new JokeGenerator();
            jokeGenerator.Subscribe(emailSender.MailJoke);
            jokeGenerator.Subscribe(smsSender.SmsJoke);

            jokeGenerator.UnSubscribe(smsSender.SmsJoke);
            emailSender = null;
            smsSender = null;
            jokeGenerator = null;
            //Please pay attention to this memory leak case: we have explicitly set jokeGenerator to null, but events still occur, so Timer object lives in memory, and the object, that contain handling method live as well
            GC.Collect();
            Console.ReadLine();
        }

    }

    public class EmailSender
    {
        public void MailJoke(string joke)
        {
            Console.WriteLine("Email: " + joke);
        }
    }

    public class SmsSender
    {
        public void SmsJoke(string joke)
        {
            Console.WriteLine("SMS: " + joke);
        }
    }
    public class JokeGenerator
    {
        private Action<string> jokeDelegate;

        private int currentJokeIndex = -1;
        private string[] jokesCollection = new[]
                                               {
                                                   @"A bear walks into a bar and says to the bartender, 'I'll have a pint of beer and a.......... packet of peanuts.'
                                                   The bartender asks, 'Why the big pause?",
                                                   @"Can a kangaroo jump higher than a house? Of course, a house doesn’t jump at all",
                                                   @"Anton, do you think I’m a bad mother? My name is Paul"
                                                };

        public void Subscribe(Action<string> jokeDelegate)
        {
            Delegate mainDel = System.Delegate.Combine(jokeDelegate, this.jokeDelegate);
            this.jokeDelegate = mainDel as Action<string>;
        }

        public void UnSubscribe(Action<string> jokeDelegate)
        {
            Delegate mainDel = System.Delegate.Remove(this.jokeDelegate, jokeDelegate);
            this.jokeDelegate = mainDel as Action<string>;
        }

        public JokeGenerator()
        {
            Timer timer = new Timer(4000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.jokeDelegate != null)
            {
                this.currentJokeIndex++;
                if (this.jokesCollection.Length > this.currentJokeIndex)
                {
                    this.jokeDelegate.Invoke(this.jokesCollection[this.currentJokeIndex]);
                }
            }
        }
    }

}
