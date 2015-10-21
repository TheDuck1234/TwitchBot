using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    class Program
    {

        //password from twitchapps.com/tmi

        static void Main(string[] args)
        {
            var irc = new IrcClient("irc.twitch.tv",6667, "cogwheelbot", "oauth:ffiiryof5w501xlz43qbk75k9xcmv1");
            irc.JoinRoom("faddei");
            var stopWatch = Stopwatch.StartNew(); 
            while (true)
            {
                string message = irc.ReadMessage();

                if(!string.IsNullOrEmpty(message)) {
                    Console.WriteLine(message);
                    if (message.ToLower().Contains("!hallo"))
                    {
                        irc.SendChatMessage("Testing Testing");
                    }
                    if (message.ToLower().Contains("!uptime"))
                    {
                        DateTime time = new DateTime(stopWatch.ElapsedTicks);
                        string uptime = time.ToString("HH:mm:ss");
                        irc.SendChatMessage("Time: "+uptime);

                    }
                }

            }
                
        }
    }
}
