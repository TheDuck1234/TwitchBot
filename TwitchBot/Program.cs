using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
            XmlDocument doc = new XmlDocument();
            doc.Load("BlockNLoad.xml");

            while (true)
            {
                string message = irc.ReadMessage();
                if(!string.IsNullOrEmpty(message)) {
                    Console.WriteLine(message);
                    if (message.ToLower().Contains("!hallo"))
                    {
                        irc.SendChatMessage("Testing Testing");
                    }
                    if (message.ToLower().Contains("!add"))
                    {
                        string[] html = message.Split('!');
                        Console.WriteLine("open link: "+html[1].Substring(4));
                        System.Diagnostics.Process.Start(html[1].Substring(4));
                    }
                    if (message.ToLower().Contains("!cogwheel"))
                    {
                        List<string> list = new List<string>();
                        Random rnd = new Random();
                        int r = rnd.Next(list.Count);
                        XmlNode node = doc.SelectSingleNode("BlockNLoad");

                        XmlNodeList prop = node.SelectNodes("Cogwheel");
                        foreach (XmlNode n in prop)
                        {
                            list.Add(n.InnerText);
                        }
                        irc.SendChatMessage((string)list[r]);
                    }
                    if (message.ToLower().Contains("!uptime"))
                    {
                        DateTime time = new DateTime(stopWatch.ElapsedTicks);
                        string uptime = time.ToString("HH:mm:ss");
                        irc.SendChatMessage(time.Hour+ " hours "+ time.Minute + " mins");
                        irc.SendChatMessage(uptime);
                    }
                }

            }
                
        }
    }
}
