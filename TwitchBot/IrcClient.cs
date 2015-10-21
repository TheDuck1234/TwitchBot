using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TwitchBot
{
    class IrcClient
    {
        private readonly string _userName;
        private string _channel;

        private TcpClient _tcpClient;
        private StreamReader _inputReader;
        private StreamWriter _outputWriter;

        public IrcClient(string ip, int port,string username,string password)
        {
            _userName = username;
            _tcpClient = new TcpClient(ip,port);
            _inputReader = new StreamReader(_tcpClient.GetStream());
            _outputWriter = new StreamWriter(_tcpClient.GetStream());

            _outputWriter.WriteLine("PASS " + password);
            _outputWriter.WriteLine("NICK " + username );
            _outputWriter.WriteLine("USER "+ username+ " 8 * :"+ username);
            _outputWriter.Flush();
        }

        public void JoinRoom(string channel)
        {
            _channel = channel;
            _outputWriter.WriteLine("JOIN #"+ channel);
            _outputWriter.Flush();
        }

        public void SendIrcMessage(string message)
        {
            _outputWriter.WriteLine(message);
            _outputWriter.Flush();
        }

        public void SendChatMessage(string message)
        {
            SendIrcMessage(":"+_userName+"!"+_userName+"@"+_userName
                +"tmi.twitch.tv PRIVMSG #"+ _channel+ " :"+message);
        }

        public string ReadMessage()
        {
            string message = _inputReader.ReadLine();
            return message;
        }
    }
}
