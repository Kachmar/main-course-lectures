using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Delegates
{
    public class Events
    {
        static void Main(string[] args)
        {
            var smsSender = new SmsSender( );
            var emailSender= new EmailSender( );
            JokeGenerator jokeGenerator = new JokeGenerator();
            jokeGenerator.JokeGenerated += (emailSender.MailJoke);
            jokeGenerator.JokeGenerated += (smsSender.SmsJoke);

            Console.WriteLine("Hit enter to remove SMS sender");
            Console.ReadLine();
            jokeGenerator.JokeGenerated -= (smsSender.SmsJoke);
            jokeGenerator = null;
            Console.ReadLine();
        }

    }

    public class EmailSender
    {
        public void MailJoke(string joke) { }
    }

    public class SmsSender
    {
        public void SmsJoke(string joke) { }
    }
    public class JokeGenerator
    {
        public event Action<string> JokeGenerated;
        private int currentJokeIndex = -1;
        private string[] jokesCollection = new[]
                                               {
                                                   @"A bear walks into a bar and says to the bartender, 'I'll have a pint of beer and a.......... packet of peanuts.'
                                                   The bartender asks, 'Why the big pause?",
                                                   @"Can a kangaroo jump higher than a house? Of course, a house doesn’t jump at all",
                                                   @"Anton, do you think I’m a bad mother? My name is Paul"
                                                };


        public JokeGenerator()
        {
            Timer timer = new Timer(4000);
            timer.Start();
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this.JokeGenerated != null)
            {
                this.currentJokeIndex++;
                if (this.jokesCollection.Length > this.currentJokeIndex)
                {
                    this.JokeGenerated.Invoke(this.jokesCollection[this.currentJokeIndex]);
                }
            }
        }
    }

}
