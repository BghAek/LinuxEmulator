using Microsoft.AspNetCore.Mvc;
using System;
using Renci.SshNet;

namespace LinuxTerminalApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("commandline/{input}")]
        public string ProgrammingLanguages(string input)
        {



            string host = "Ip";
            string username = "username";
            string password = "password";
            string sudoPassword = "password";
            string output = " ";

            using (var client = new SshClient(host, username, password))
            {
                
                client.Connect();
                string currentDirectory = "~";

                output = $"[{currentDirectory}]$ ";
                string commandText = input;



                string fullCommand;
                if (commandText.StartsWith("cd "))
                {
                    string newDirectory = commandText.Substring(3).Trim();
                    fullCommand = $"cd {currentDirectory} && cd {newDirectory} && pwd";
                }
                else if (commandText.StartsWith("sudo"))
                {
                    fullCommand = $"cd {currentDirectory} && echo {sudoPassword} | sudo -S {commandText.Substring(5)}";
                }
                else
                {
                    fullCommand = $"cd {currentDirectory} && {commandText}";
                }

                var command = client.CreateCommand(fullCommand);
                string result = command.Execute();

                if (commandText.StartsWith("cd "))
                {
                    // Update current directory if cd command was successful
                    if (command.ExitStatus == 0)
                    {
                        currentDirectory = result.Trim();
                    }

                }
                else
                {
                    output = output + result;
                }

                return output;
                



            }



        }

    }
}
