using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Description: A Program designed to prompt a user
    // for a decision using a for loop. Also desinged 
    // to show knowledge of finch controls
    // Application Type: Console
    // Author: Trevor W. Sepanik
    // Dated Created: 10/02/19 
    // Last Modified: 11/06/19
    //
    // **************************************************

    class Program
    {
        public enum Command
        {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            SOUNDBUZZER,
            BUZZEROFF,
            DONE
            
        }
        static void Main(string[] args)
        {
            

            DisplayWelcomeScreen();

            DisplayLoginRegisterOption();

            DisplayMainMenu();

            DisplayClosingScreen();
        }

        static bool DisplayLoginRegisterOption()
        {
            string menuChoice;

            bool quitApplication = false;


            DisplayScreenHeader("Login Menu");

            DisplayContinuePrompt();

            do
            {

                Console.WriteLine();

                Console.WriteLine("Please choose an option");

                Console.WriteLine("a.) Register");

                Console.WriteLine();

                Console.WriteLine("b.) Login");

                Console.WriteLine();

                Console.WriteLine("q.) quit");

                menuChoice = Console.ReadLine();



                switch (menuChoice)
                {
                    case "a":

                        DisplayLoginRegister();

                        DisplayContinuePrompt();



                        break;

                    case "b":

                        DisplayLoginScreen();

                        DisplayContinuePrompt();





                        break;

                    case "c":

                        return quitApplication;

                    default:

                        Console.WriteLine();

                        Console.WriteLine("Please indicate your choice with a letter.");

                        Console.WriteLine();

                        DisplayContinuePrompt();

                        break;

                }
            } while (!quitApplication);

            return quitApplication;

        }


        static string[] DisplayLoginRegister()
        {
            string dataPath = @"Data\Register.txt";

            string passWord;

            string userName;

            string[] UserandPass = new string[2];

            DisplayScreenHeader("Registration Screen");

            Console.WriteLine("Thank you for creating an account!");

            Console.WriteLine();

            Console.WriteLine("Please enter username:");

            userName = Console.ReadLine();

            UserandPass[0] = userName;

            Console.WriteLine();

            Console.WriteLine("Please enter password:");

            passWord = Console.ReadLine();

            UserandPass[1] = passWord;

            Console.WriteLine();

            File.WriteAllLines(dataPath, UserandPass);

            return UserandPass;


        }

        static bool DisplayLoginScreen()
        {
            string dataPath = "Data\\Register.txt";

            string[] loginInfo;

            bool correct = false;

            string userName;

            string validUserName;

            string passWord;

            string validPassword;

            loginInfo = File.ReadAllLines(dataPath);
            validUserName = loginInfo[0];
            validPassword = loginInfo[1];

            Console.WriteLine();

            do
            {

                Console.WriteLine("Username:");

                userName = Console.ReadLine();

                if (userName == validUserName)
                {
                    Console.WriteLine();

                    Console.WriteLine("Password:");

                    Console.WriteLine();

                    passWord = Console.ReadLine();

                  if (passWord == validPassword)
                  {
                        DisplayMainMenu();
                  }
                }
                else
                {
                    Console.WriteLine("Incorrect Username or Password.");
                }

            } while (correct);

            return correct;

        }
        static void SetTheme()
        {
            string dataPath = @"Data\Theme.txt";

            string foreGroundColorString;
            ConsoleColor foreGroundColor;

            foreGroundColorString = File.ReadAllText(dataPath);
            Enum.TryParse(foreGroundColorString, out foreGroundColor);

            Console.ForegroundColor = foreGroundColor;
        }
        static void DisplayMainMenu()
        {
            //
            // instantiate a Finch object
            //

            Finch finchRobot = new Finch();

            bool finchRobotConnected = false;
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("a) Connect Finch Robot");

                Console.WriteLine();

                Console.WriteLine("b) Talent Show");

                Console.WriteLine();

                Console.WriteLine("c) Data Recorder");

                Console.WriteLine();

                Console.WriteLine("d) Alarm System");

                Console.WriteLine();

                Console.WriteLine("e) User Programming");

                Console.WriteLine();

                Console.WriteLine("f) Disconnect Finch Robot");

                Console.WriteLine();

                Console.WriteLine("q) Quit");

                Console.WriteLine();

                Console.WriteLine("Enter Choice:");

                Console.WriteLine();

                menuChoice = Console.ReadLine();


                //
                // Process user's Choice
                //
               
                switch (menuChoice)
                {
                    case "a":
                        finchRobotConnected = DisplayConncectFinchRobot(finchRobot);

                        break;

                    case "b":
                        if (finchRobotConnected)
                        {
                            DisplayTalentShow(finchRobot);

                            DisplayContinuePrompt();
                        }
                        else
                        {
                            Console.WriteLine();

                            Console.WriteLine("Please return to the Main Menu and connect to the Finch Robot.");

                            DisplayContinuePrompt();
                        }

                        break;

                    case "c":
                        DisplayDataRecorder(finchRobot);

                        break;

                    case "d":
                        DisplayAlarmSystem(finchRobot);

                        break;

                    case "e":
                       
                        DisplayUserProgramming(finchRobot);

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);

                        break;

                    case "q":
                        quitApplication = true;

                        break;

                    default:
                        Console.WriteLine();

                        Console.WriteLine("Please indicate your choice with a letter.");

                        DisplayContinuePrompt();

                        break;  
                }

            } while (!quitApplication);        
        }      
        /// User Programming    
        static void DisplayUserProgramming(Finch finchRobot)
        {

            string menuChoice;

            bool quitApplication = false;

            (int motorSpeed, int ledBrightnessR, int ledBrightnessG, int ledBrightnessB, int waitSeconds, int Buzznoise) commandParameters;

            commandParameters.motorSpeed = 0;

            commandParameters.ledBrightnessR = 0;

            commandParameters.ledBrightnessG = 0;

            commandParameters.ledBrightnessB = 0;

            commandParameters.waitSeconds = 0;

            commandParameters.Buzznoise = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get the user's menu choice
                //
                Console.WriteLine("a) Set Command Parameters");

                Console.WriteLine();

                Console.WriteLine("b) Add Commands");

                Console.WriteLine();

                Console.WriteLine("c) View Commands");

                Console.WriteLine();

                Console.WriteLine("d) Execute Commands");

                Console.WriteLine();

                Console.WriteLine("e) Write Commands to Data File");

                Console.WriteLine();

                Console.WriteLine("f) Read Commands from Data File");

                Console.WriteLine();

                Console.WriteLine("q) Quit");

                Console.WriteLine();

                Console.WriteLine("Enter Choice:");

                Console.WriteLine();

                menuChoice = Console.ReadLine();


                //
                // Process user's Choice
                //
                switch (menuChoice)
                {
                    case "a":
                       
                        commandParameters =  DisplayGetCommandParameters();
                                              
                        break;

                    case "b":
                       
                        DisplayListOfFinchCommands();

                        DisplayGetFinchCommands(commands);
                   
                        break;

                    case "c":
                       
                        DisplayFinchCommands(commands);
                      
                        break;

                    case "d":
                       
                        DisplayEcecuteFinchCommands(finchRobot, commands, commandParameters);
                          
                        break;

                    case "e":

                        DisplayWriteUserProgrammingData(commands);

                        break;

                    case "f":

                        commands = DisplayReadUserProgrammingData();

                        break;

                    case "q":
                       
                        quitApplication = true;

                        break;

                    default:

                        Console.WriteLine();

                        Console.WriteLine("Please indicate your choice with a letter.");

                        DisplayContinuePrompt();


                        break;
                }

            } while (!quitApplication);    

        }

        static List<Command> DisplayReadUserProgrammingData()
        {
            string dataPath = @"Data\Data.txt";

            List<Command> commands = new List<Command>();

            string[] commandsString;

            DisplayScreenHeader("Load Commands from Data File");

            Console.WriteLine("Ready to load the commands from the data file.");

            DisplayContinuePrompt();

            commandsString = File.ReadAllLines(dataPath);

            //
            // Create a list of command
            //
            Command command;

            foreach (string commandString in commandsString)
            {
                Enum.TryParse(commandString, out command);

                commands.Add(command);

            }

            Console.WriteLine();

            Console.WriteLine("Commands loaded sucesfully");

            DisplayContinuePrompt();

            return commands;
        }

        static void DisplayWriteUserProgrammingData(List<Command> commands)
        {
            string dataPath = @"Data\Data.txt";

            List<string> commandsString = new List<string>();

            DisplayScreenHeader("Save Commands to the Data File");

            Console.WriteLine("Ready to save the commands to the data file.");

            DisplayContinuePrompt();

            //
            // create list of command strings
            //
            foreach (Command command in commands)
            {
                commandsString.Add(command.ToString());
            }

            File.WriteAllLines(dataPath, commandsString.ToArray());

            Console.WriteLine();

            Console.WriteLine("Commands have been succesfully saved.");

            DisplayContinuePrompt();
        }

        static void DisplayListOfFinchCommands()
        {
            DisplayScreenHeader("Finch Robot Commands");

            Console.WriteLine("Please type your commands from the following options listed below.");

            Console.WriteLine("MOVEFORWARD" + "" + "------> Moves Finch forwards.");

            Console.WriteLine("MOVEBACKWARD" + "" + "------> Moves Finch backwards.");

            Console.WriteLine("STOPMOTORS" + "" + "------> Stops the Finch from moving");

            Console.WriteLine("WAIT" + "" + "------> Makes the Finch wait.");

            Console.WriteLine("TURNRIGHT" + "" + "------> Turns the Finch to the right.");

            Console.WriteLine("TURNLEFT" + "" + "------> Turns the finch to the left.");

            Console.WriteLine("LEDON" + "" + "------> Turns the Finch's lights on.");

            Console.WriteLine("LEDOFF" + "" + "------> Turns the Finch's lights off.");

            Console.WriteLine("SOUNDBUZZER " + "" + "------> Turns the Finch's Buzzer on.");

            Console.WriteLine("BUZZEROFF " + "" + "------> Turns the Finch's Buzzer off (Recomended If SOUNDBUZZER Was Selected.");

            Console.WriteLine("DONE" + "" + "------> Type when you are completed with your commands.");
        }


        static void DisplayEcecuteFinchCommands(Finch finchRobot,
            List<Command> commands,
             (int motorSpeed, int ledBrightnessR, int ledBrightnessG, int ledBrightnessB, int waitSeconds, int Buzznoise) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;

            int ledBrightnessR = commandParameters.ledBrightnessR;

            int ledBrightnessG = commandParameters.ledBrightnessG;

            int ledBrightnessB = commandParameters.ledBrightnessB;

            int waitMiliSeconds = commandParameters.waitSeconds * 1000;

            int buzzerSound = commandParameters.Buzznoise;

            DisplayScreenHeader("Execute Finch Commands");

            // info and pause
            Console.ReadLine();

            Console.WriteLine("The finch is about to perform your commands.");

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:

                        break;

                    case Command.MOVEFORWARD:

                        finchRobot.setMotors(motorSpeed, motorSpeed);

                        break;

                    case Command.MOVEBACKWARD:

                        finchRobot.setMotors(-motorSpeed, -motorSpeed);

                        break;

                    case Command.STOPMOTORS:

                        finchRobot.setMotors(0, 0);

                        break;

                    case Command.WAIT:

                        finchRobot.wait(waitMiliSeconds);

                        break;

                    case Command.TURNRIGHT:

                        finchRobot.setMotors(0, motorSpeed);

                        break;

                    case Command.TURNLEFT:

                        finchRobot.setMotors(motorSpeed, 0);

                        break;

                    case Command.LEDON:

                        finchRobot.setLED(ledBrightnessR, ledBrightnessG, ledBrightnessB);

                        break;

                    case Command.LEDOFF:

                        finchRobot.setLED(0, 0, 0);

                        break;

                    case Command.SOUNDBUZZER:

                        finchRobot.noteOn(0);

                        break;

                    case Command.BUZZEROFF:

                        finchRobot.noteOff();

                        break;

                    case Command.DONE:

                        break;

                    default:

                        break;
                }

            }
        }

        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }

            DisplayContinuePrompt();
        }

        static void DisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            string userResponse;

            //info for the user;
            while (command != Command.DONE)
            {
                Console.Write("Enter Command:");
                userResponse = Console.ReadLine().ToUpper();
                Enum.TryParse(userResponse, out command);
                // validate 
                if (command != Command.NONE)
                {
                    commands.Add(command);

                }  

            }
            // echo commands

            DisplayContinuePrompt();
        }
        static (int motorSpeed, int ledBrightnessR, int ledBrightnessG, int ledBrightnessB, int waitSeconds, int Buzznoise) DisplayGetCommandParameters()
        {
            

          (int motorSpeed, int ledBrightnessR, int ledBrightnessG, int ledBrightnessB, int waitSeconds, int Buzznoise) commandParameters;

            commandParameters.motorSpeed = 0;

            commandParameters.ledBrightnessR = 0;

            commandParameters.ledBrightnessG = 0;

            commandParameters.ledBrightnessB = 0;

            commandParameters.waitSeconds = 0;

            commandParameters.Buzznoise = 0;

            bool correct = false;

            bool correctR = false;

            bool correctG = false;

            bool correctB = false;

            bool correctWait = false;

            bool correctBuzz = false;


            Console.Clear();

            
            
                do
                {
                    Console.WriteLine();

                    Console.Write("Please enter how quickly you would like the finch move \n\n " +
                        "Please enter an integer between [1 - 255] \n\n" +
                        "Please enter Integer:");

                    Console.WriteLine();

                    if (int.TryParse(Console.ReadLine(), out commandParameters.motorSpeed))
                    {
                        Console.WriteLine($"You have chose to set the finch speed to {commandParameters.motorSpeed}");

                        Console.WriteLine();

                        correct = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: Please make sure to enter an integer.");

                        Console.WriteLine();
                    }
                } while (correct != true);

                DisplayContinuePrompt();

                Console.Clear();

                do
                {
                    Console.WriteLine();

                    Console.Write("If you would like to display the color RED, please enter the integer '255'. \n\n" +
                        "If you would like to display the color GREEN or BLUE please enter the integer '0'. \n\n" +
                        "Please enter integer:");

                    Console.WriteLine();

                    if (int.TryParse(Console.ReadLine(), out commandParameters.ledBrightnessR))
                    {
                        Console.WriteLine($"You have chose to set the color Red to {commandParameters.ledBrightnessR}");

                        Console.WriteLine();

                        correctR = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: please make sure to enter an integer.");

                        Console.WriteLine();
                    }

                } while (correctR != true);

                DisplayContinuePrompt();

                Console.Clear();

                Console.WriteLine();
                do
                {

                    Console.WriteLine("If you would like to display the color GREEN, at this time please enter the integer '255'. \n\n" +
                        "If you would like to display the color BLUE, please enter the integer '0'. \n\n " +
                        "If you have already chose an integer previously, feel free to enter any integer between[1 - 255] to experiment with colors.");

                    Console.WriteLine();

                    if (int.TryParse(Console.ReadLine(), out commandParameters.ledBrightnessG))
                    {
                        Console.WriteLine($"You have chose to set the color Green to {commandParameters.ledBrightnessG}");

                        Console.WriteLine();

                        correctG = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: please make sure to enter an integer.");

                        Console.WriteLine();
                    }

                } while (correctG != true);

                DisplayContinuePrompt();

                Console.Clear();

                do
                {

                    Console.WriteLine();

                    Console.WriteLine("If you would like to display the color BLUE, please enter the integer '255'. \n\n" +
                        "If you have already chose an integer previously, feel free to enter any integer between [1-255] to experiment with  \n\n " +
                        "colors.");

                    Console.WriteLine();

                    if (int.TryParse(Console.ReadLine(), out commandParameters.ledBrightnessB))
                    {
                        Console.WriteLine($"You have chose to set the color Blue to {commandParameters.ledBrightnessB}");

                        Console.WriteLine();

                        correctB = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: please make sure to enter an integer.");

                        Console.WriteLine();
                    }

                } while (correctB != true);

                DisplayContinuePrompt();

                Console.Clear();

                do
                {
                    Console.WriteLine();

                    Console.Write("Enter the amount of time you would like the Finch to pause for.\n\n" +
                        "Keep in mind that if you chose the integer '1' (for example) whenever you program WAIT, the Finch will wait one second.\n\n" +
                        "Please enter integer:");

                    Console.WriteLine();

                    if (int.TryParse(Console.ReadLine(), out commandParameters.waitSeconds))
                    {
                        Console.WriteLine($"You have chose to set the wait time to {commandParameters.waitSeconds}");

                        Console.WriteLine();

                        correctWait = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: please make sure to enter an integer.");

                        Console.WriteLine();
                    }

                } while (correctWait != true);

            DisplayContinuePrompt();

            Console.Clear();

            do
            {

                Console.WriteLine();

                Console.Write("Enter the frequency of buzz sound you would like the finch to produce.\n\n" +
                    "Frequency is measured in HZ and is input as an integer.  \n\n" +
                    "Please enter integer:");

                Console.WriteLine();

                if (int.TryParse(Console.ReadLine(), out commandParameters.Buzznoise))
                {
                    Console.WriteLine($"You have chose to set the frequency of sound to {commandParameters.Buzznoise}");

                    correctBuzz = true;

                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Error: Please make sure to enter an integer.");

                    Console.WriteLine();
                }

            } while (correctBuzz != true);


            Console.WriteLine("Command Parameters have been set.");

                DisplayContinuePrompt();

                return commandParameters;  

        }

        /// <summary>
        /// Alarm System
        /// </summary>
        static void DisplayAlarmSystem(Finch finchRobot)
        {
            string alarmType;
            int MaxSeconds;
            double threshold;
            bool thresholdExceeded;

            DisplayScreenHeader("Sound the Alarm!");

            alarmType = DisplayGetAlarmType();

            MaxSeconds = DisplayGetMaxSeconds();

            threshold = DisplayGetThreshold(finchRobot, alarmType);

            thresholdExceeded = MonitorCurrentLightLevels(finchRobot, threshold, MaxSeconds);

            thresholdExceeded = MonitorCurrentTemperatureLevels(finchRobot, threshold, MaxSeconds);

            Console.WriteLine();

            if (thresholdExceeded)
            {
                if (alarmType == "light")
                {
                    Console.WriteLine("Maximum Light Level Exceeded");
                }
                else
                {
                    Console.WriteLine("Maximum Temperature Level Exceeded");
                }
            }
            else
            {
                Console.WriteLine("Maximum Time Exceeded");
            }


            DisplayContinuePrompt();
        }

        static bool MonitorCurrentTemperatureLevels(Finch finchRobot, double threshold, double MaxSeconds)
        {
            bool thresholdExceeded = false;
            double currentTemperatureLevel;
            double seconds = 0;

            DisplayScreenHeader("Monitor Temperature Levels");

            while (!thresholdExceeded && seconds <= MaxSeconds)
            {
                currentTemperatureLevel = finchRobot.getTemperature();
        
                Console.WriteLine($"Maximum Temperature Level: {threshold}");

                Console.WriteLine($"Current Temperature Level: {currentTemperatureLevel}");

                if (currentTemperatureLevel > threshold) thresholdExceeded = true;

                finchRobot.wait(500);

                seconds += 0.5;
            }

            return thresholdExceeded;
        }

        static bool MonitorCurrentLightLevels(Finch finchRobot, double threshold, double maxSeconds)
        {
            bool thresholdExceeded = false;
            int currentLightLevel;
            double seconds = 0;

            finchRobot.setLED(0, 255, 0);

          

                DisplayScreenHeader("Monitor Light Levels");

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                currentLightLevel = finchRobot.getLeftLightSensor();

                Console.WriteLine($"The Current Light Level Of the finch is: {currentLightLevel}");

                Console.WriteLine();

                Console.WriteLine($"Finch's Threshold Level: {threshold}");
               
                if (currentLightLevel > threshold) thresholdExceeded = true;

                finchRobot.wait(500);

                seconds += 0.5;
            }


            return thresholdExceeded;
        }

        static double DisplayGetThreshold(Finch finchRobot, string alarmType)
        {
            double threshold = 0;
            string userResponse;
            string lightlevel;
            string tempLevel;
            bool correct = false;

            DisplayScreenHeader("Threshold Value");

            switch (alarmType)
            {
                case "light":

                    
                        Console.WriteLine("Thank you for choosing the light feature!");

                    do { 
                        Console.WriteLine();
                    
                        Console.WriteLine($"Current Light Level: {finchRobot.getLeftLightSensor()}");

                        Console.WriteLine();

                        do
                        {

                            Console.WriteLine("Enter maximum light level you would like the finch to reach [0 - 255]:");

                            lightlevel = Console.ReadLine();

                            Console.WriteLine();

                            Console.WriteLine($"You have chosen to set the maxium light level at {lightlevel} is this correct (Y or N)");

                            Console.WriteLine();

                            userResponse = Console.ReadLine();

                            if (double.TryParse(lightlevel, out threshold))
                            {
                                Console.WriteLine($"You have chose to set the new light level at {threshold}");

                                correct = true;
                            }

                            else
                            {
                                Console.WriteLine($"Plese make sure to enter a integer for light level.");
                            }

                        } while (correct != true);
                           
                    } while (userResponse != "Y");

                    break;

                case "temperature":

                    Console.WriteLine("Thank you for choosing the temperature feature!");

                    do
                    {
                        Console.WriteLine();

                        Console.WriteLine($"Current Temperature: {finchRobot.getTemperature()}");

                        Console.WriteLine();

                        do
                        {

                            Console.WriteLine("Enter the Maximum temperature you would like the finch to reach:");

                            Console.WriteLine();

                            tempLevel = Console.ReadLine();

                            Console.WriteLine();

                            Console.WriteLine($"You have chosen to set the maximum temperature for the Finch at {tempLevel}. Is this correct (Y or N)");

                            userResponse = Console.ReadLine();

                            Console.WriteLine();

                            if (double.TryParse(tempLevel, out threshold))
                            {
                                Console.WriteLine($"You have chose to set the temperature threshold at {threshold}.");

                                correct = true;
                            }
                            else
                            {
                                Console.WriteLine($" {tempLevel} is not an integer. Please make sure to enter an integer when setting temperature threshold.");
                            }

                        } while (correct != true);

                    } while (userResponse != "Y");

                    break;     
            }

            DisplayContinuePrompt();

            return threshold;

        }

        static int DisplayGetMaxSeconds()
        {
            string numberOfSeconds;

            int userInt;

            bool correct = false;


            do
            {
          
               Console.WriteLine();

               Console.WriteLine("Please enter the number of seconds you would like me to monitor the finch Robot \n \n Number of seconds (integer):");

               Console.WriteLine();

               numberOfSeconds = Console.ReadLine();

               Console.WriteLine();

                if (int.TryParse(numberOfSeconds, out userInt))
                {
                    Console.WriteLine($"You have chose {userInt} seconds.");

                    correct = true;
                }
                else
                {
                    Console.WriteLine($"{numberOfSeconds} is not an integer");
                }

            } while (correct != true );

            DisplayContinuePrompt();
             
            return userInt;  
        }

        static string DisplayGetAlarmType()
        {
            string userResponse;
            string alarmType;
            do
            {

                do
                {
                    Console.WriteLine("Enter alarm type you would like to observe, (Please choose light or temperature) \n \n Alarm Type:");

                    Console.WriteLine();

                    alarmType = Console.ReadLine();

                    Console.WriteLine();

                    Console.WriteLine($"You have chose {alarmType}! Is this correct (Y or N)");

                    Console.WriteLine();

                    userResponse = Console.ReadLine().ToUpper();

                } while (userResponse != "Y");

                if (alarmType != "light" && alarmType != "temperature")
                {
                    Console.WriteLine();

                    Console.WriteLine("I did not recognize that response.");

                    Console.WriteLine();
                }

            } while (alarmType != "light" && alarmType != "temperature");

            Console.Clear();

            return alarmType;
                
        }
        /// <summary>
        /// Data Recorder
        /// </summary>
        static void DisplayDataRecorder(Finch finchRobot)
        {
            double dataPointFrequency;
            int numberOfDataPoints;
            string userResponse;
            string answer;
            // Recieve Data frequency from user and recieve the number of data points the user would like displayed.
            // Show the list in an array created by the user 

            DisplayScreenHeader("Data Recorder");


            dataPointFrequency = DisplayGetDataPointFrequency();

            numberOfDataPoints = DisplayGetNumberOfDataPoints();

            double[] lightsensorL = new double[numberOfDataPoints];

            double[] lightsensorR = new double[numberOfDataPoints];

            double[] temperatures = new double[numberOfDataPoints];

            do { 
            do { 
            Console.WriteLine("Would You Like To Record The Finch's Light Sensors or Temperatures? (Please enter 'LIGHT SENSORS' or 'TEMPERATURES')" );

                Console.WriteLine();
           
                userResponse = Console.ReadLine().ToUpper();

                Console.WriteLine();

                Console.WriteLine($"You chose {userResponse}. Is this correct? (Y or N)");

                Console.WriteLine();

                answer = Console.ReadLine().ToUpper();

            } while (answer != "Y");
            
                if (userResponse == "LIGHT SENSORS")
                {
                    DisplayGetDataLeftLightSensor(numberOfDataPoints, dataPointFrequency, lightsensorL, finchRobot);

                    DisplayGetDataRightLightSensor(numberOfDataPoints, dataPointFrequency, lightsensorR, finchRobot);

                    DisplaySensorData(lightsensorR, lightsensorL);

                    RecieveSumOfSensorsReadings(lightsensorL, lightsensorR, numberOfDataPoints);

                }

                else if (userResponse == "TEMPERATURES")
                {
                    DisplayGetData(numberOfDataPoints, dataPointFrequency, temperatures, finchRobot);

                    DisplayData(temperatures);

                    DisplayContinuePrompt();
                }

                else
                {
                    Console.WriteLine();

                    Console.WriteLine("Did not Recognize Response");

                    DisplayContinuePrompt();
                }
            } while (userResponse != "LIGHT SENSORS" && userResponse != "TEMPERATURES");
        }

        static void DisplayGetDataLeftLightSensor(int numberOfDataPoints,
          double dataPointFrequency,
          double[] lightsensorL,
          Finch finchRobot)
        {
            DisplayScreenHeader("Get Left Light Sensor Readings");

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                lightsensorL[index] = finchRobot.getLeftLightSensor();

                int milliseconds = (int)(dataPointFrequency * 1000);

                finchRobot.wait(milliseconds);

                Console.WriteLine($"Left Light Sensor {index + 1}: {lightsensorL[index]}");
            }

            DisplayContinuePrompt();
        }
        static void DisplayGetDataRightLightSensor(int numberOfDataPoints,
            double dataPointFrequency,
            double[] lightsensorR,
            Finch finchRobot)
        {
            DisplayScreenHeader("Get Right Light Sensor Readings");

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                lightsensorR[index] = finchRobot.getRightLightSensor();

                int milliseconds = (int)(dataPointFrequency * 1000);

                finchRobot.wait(milliseconds);

                Console.WriteLine($"Right Light Sensor {index + 1}: {lightsensorR[index]}");
            }

            DisplayContinuePrompt();
        }

        static void RecieveSumOfSensorsReadings(double[] lightsensorL, double[] lightsensorR, int numberOfDataPoints)
        {
            double sum = 0;
            double sum1 = 0;

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                sum += lightsensorL[index];
            }

            Console.WriteLine($"The sum of the Left Light Sensor is {sum}.");

            Console.WriteLine();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                sum1 += lightsensorR[index];
            }

            Console.WriteLine($"The sum of the Right Light Sensor is {sum1}.");

            Console.WriteLine();

            double average = ((sum + sum1) / (numberOfDataPoints * 2));

            Console.WriteLine();

            Console.WriteLine($"The Average Light Sensor Reading is: {average}");

            DisplayContinuePrompt();

        }

        static void DisplaySensorData(double[] lightsensorR, double[] lightsensorL)
        {
            DisplayScreenHeader("Light Sensor Data (Combined)");

            Console.WriteLine();

            Console.WriteLine("Here is a list of the previously recorded light sensor Data");

            Console.WriteLine();

            for (int index = 0; index < lightsensorL.Length; index++)
            {
                Console.WriteLine($"Left Light Senor {index + 1}: {lightsensorL[index]}");
            }

            Console.WriteLine();

            for (int index = 0; index < lightsensorR.Length; index++)
            {
                Console.WriteLine($"Right Light Senor {index + 1}: {lightsensorR[index]}");
            }

            Console.WriteLine();

            DisplayContinuePrompt();       

        }
        static void DisplayData(double[] temperatures)
        {
            //This will display the list of data previously recorded 

            Console.WriteLine("Here is the list of the Data I recorded for you");

            DisplayScreenHeader("Temperatures");

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}");
            }

            DisplayContinuePrompt();
        }
        
        static void DisplayGetData(int numberOfDataPoints, 
            double dataPointFrequency,
            double[] temperatures,
            Finch finchRobot)
        {
            //This will recieve data from the user and show the data being collected in real time

            DisplayScreenHeader("Get Temperatures");

            for (int index = 0; index < numberOfDataPoints; index++)
            {
               temperatures[index] = finchRobot.getTemperature();

                int milliseconds = (int)(dataPointFrequency * 1000);

                finchRobot.wait(milliseconds);

                Console.WriteLine($"Temperature {index + 1}: {temperatures[index]}" );
            }
            DisplayContinuePrompt();
        }

        static int DisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            bool correct = false;

            DisplayScreenHeader("Number of Data Points");

            Console.WriteLine();

            Console.WriteLine("Now Lets Decide How Many Intervals Of The Finch's Temperature You Would Like To Record");

            do
            {

                Console.WriteLine();

                Console.Write("Enter the Number of Data Points You Would Like To See:");

                if (int.TryParse(Console.ReadLine(), out numberOfDataPoints))
                {
                    Console.WriteLine();

                    Console.WriteLine($"You have Chose {numberOfDataPoints}");

                    correct = true;
                }
                else
                {
                    Console.WriteLine();

                    Console.WriteLine("Error: Please enter a valid integer:");
                }

                DisplayContinuePrompt();

            } while (correct != true);

            return numberOfDataPoints;
        }

        static double DisplayGetDataPointFrequency()
        {
            double dataPointFrequency;

            bool correct = false;

            DisplayScreenHeader("Data Point Frequency");

            do
            {

                Console.WriteLine();

                Console.WriteLine("Here Is Where You Will Decide How Quickly You Want To Record The Finches Temperatures");

                Console.WriteLine();

                Console.Write("Enter The Frequency Of Recordings You Would Like To See (Seconds):");

                if (double.TryParse(Console.ReadLine(), out dataPointFrequency))
                {
                    Console.WriteLine();

                    Console.WriteLine($"You have chose {dataPointFrequency} seconds.");

                    correct = true;
                }
                else
                {
                    Console.WriteLine();

                    Console.WriteLine("Error: Please enter an integer.");
                }

                DisplayContinuePrompt();

            } while (correct != true);

            return dataPointFrequency;
        }
        /// <summary>
        /// The Finch Talent Show
        /// </summary>
        static void DisplayTalentShow(Finch finchRobot)
        {
            DisplayScreenHeader("Welcome to the Talent Show");

            Console.WriteLine("You are about to watch the Finch Robot show his moves to the crowd");

            DisplayContinuePrompt();

            for (int lightLevel = 0; lightLevel < 255; lightLevel++)
            {
                finchRobot.setLED(lightLevel, lightLevel, lightLevel);
            }

            DisplayNationalAnthem(finchRobot);

            DisplayBreakDance(finchRobot);
        }
        /// <summary>
        /// Finch Break Dance
        /// </summary>
         static void DisplayBreakDance(Finch finchRobot)
        {
            // Display Rainbow nose
            finchRobot.setLED(0, 0, 255);

            finchRobot.wait(350);

            finchRobot.setLED(100, 0, 255);

            finchRobot.wait(350);

            finchRobot.setLED(100, 200, 175);

            finchRobot.wait(350);

            finchRobot.setLED(38, 134, 225);

            finchRobot.wait(350);

            finchRobot.setLED(201, 189, 255);

            finchRobot.wait(350);

            finchRobot.setLED(45, 230, 200);

            finchRobot.wait(150);

            finchRobot.setLED(255, 0, 0);

            finchRobot.wait(150);

            finchRobot.setLED(103, 200, 255);

            finchRobot.wait(150);

            finchRobot.setLED(134, 8, 255);

            finchRobot.wait(150);

            finchRobot.setLED(0, 0, 250);

           // Do a 360 then move forwards, then move back.. Repeat!
            finchRobot.setMotors(255, 0);

            finchRobot.wait(3000);

            finchRobot.setMotors(0, 255);

            finchRobot.wait(1000);

            finchRobot.setMotors(250, 250);

            finchRobot.wait(3000);

            finchRobot.setMotors(-250, -250);

            finchRobot.wait(1000);

            finchRobot.setMotors(255, 0);

            finchRobot.wait(3000);

            finchRobot.setMotors(0, 255);

            finchRobot.wait(3000);

            finchRobot.setMotors(250, 250);

            finchRobot.wait(1000);

            finchRobot.setMotors(-250, -250);

            finchRobot.wait(1000);

            finchRobot.setMotors(255, 0);

            finchRobot.wait(3000);

            finchRobot.setMotors(0, 255);

            finchRobot.wait(3000);

            finchRobot.disConnect();
            

        }
        /// <summary>
        /// Disconnect The Finch
        /// </summary>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            DisplayScreenHeader("Disconncect Finch Robot");

            Console.WriteLine();

            Console.WriteLine("Ready to disconnect the finch robot.");

            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine();

            Console.WriteLine("Finch robot is now disconnected.");

            DisplayContinuePrompt();
        }
        /// <summary>
        /// Connecting the Finch Robot
        /// </summary>
        static bool DisplayConncectFinchRobot(Finch finchRobot)
        {
            bool finchRobotConnected = false;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("Ready to connect to the Finch robot. Press any key to continue when the USB cable is connected to the Finch and your \n computer.");

            DisplayContinuePrompt();

            finchRobotConnected = finchRobot.connect();

            if (finchRobotConnected)
            {
                finchRobot.setLED(0, 255, 0);

               // finchRobot.noteOn(15000);

                finchRobot.wait(1000);

                finchRobot.noteOff();

                Console.WriteLine();

                Console.WriteLine("Finch robot is now connected.");
            }
            else
            {
                Console.WriteLine();

                Console.WriteLine("Finch Robot Not Detected");
            }

            DisplayContinuePrompt();

            return finchRobotConnected;
        }
        /// <summary>
        /// The National Anthem in (Hz)
        /// </summary>
        static void DisplayNationalAnthem(Finch finchRobot)
        {
            finchRobot.noteOn(500); // O

            finchRobot.wait(500);

            finchRobot.noteOn(300); // O

            finchRobot.wait(200);

            finchRobot.noteOn(100); // say

            finchRobot.wait(500);

            finchRobot.noteOn(300); // can

            finchRobot.wait(500);

            finchRobot.noteOn(500); // you

            finchRobot.wait(500);

            finchRobot.noteOn(650); // see
            
            finchRobot.wait(1000);

            finchRobot.noteOn(750); // by

            finchRobot.wait(500);

            finchRobot.noteOn(700); // the

            finchRobot.wait(250);

            finchRobot.noteOn(650); // dawn's

            finchRobot.wait(500);

            finchRobot.noteOn(300); // ear -

            finchRobot.wait(500);

            finchRobot.noteOn(350); // - ly

            finchRobot.wait(500);

            finchRobot.noteOn(400); // light

            finchRobot.wait(1000);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(400); // what

            finchRobot.wait(300);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(400); // so

            finchRobot.wait(500);

            finchRobot.noteOn(750); // proud -

            finchRobot.wait(500);

            finchRobot.noteOn(700); // -ly

            finchRobot.wait(300);

            finchRobot.noteOn(650); // we

            finchRobot.wait(500);

            finchRobot.noteOn(600); // hailed

            finchRobot.wait(1000);

            finchRobot.noteOn(550); // at

            finchRobot.wait(300);

            finchRobot.noteOn(600); // the

            finchRobot.wait(300);

            finchRobot.noteOn(650); // twi -

            finchRobot.wait(500);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(650); // - light's

            finchRobot.wait(500);

            finchRobot.noteOn(500); // last

            finchRobot.wait(500);

            finchRobot.noteOn(300); // gleam -

            finchRobot.wait(500);

            finchRobot.noteOn(100); // - ing

            finchRobot.wait(500);

            finchRobot.noteOff();

            finchRobot.wait(200);

            finchRobot.noteOn(500); // Whose

            finchRobot.wait(500);

            finchRobot.noteOn(300); // broad

            finchRobot.wait(300);

            finchRobot.noteOn(100); // stripes

            finchRobot.wait(500);

            finchRobot.noteOn(300); // and

            finchRobot.wait(500);

            finchRobot.noteOn(500); // bright

            finchRobot.wait(500);

            finchRobot.noteOn(650); // stars

            finchRobot.wait(1000);

            finchRobot.noteOn(750); // thro'

            finchRobot.wait(300);

            finchRobot.noteOn(700); // the

            finchRobot.wait(300);

            finchRobot.noteOn(650); // per -

            finchRobot.wait(500);

            finchRobot.noteOn(300); // - il

            finchRobot.wait(500);

            finchRobot.noteOn(350); // - ous

            finchRobot.wait(500);

            finchRobot.noteOn(400); // fight

            finchRobot.wait(1000);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(400); // o'er

            finchRobot.wait(300);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(400); // the

            finchRobot.wait(500);

            finchRobot.noteOn(750); // ram -

            finchRobot.wait(500);

            finchRobot.noteOn(700); // -parts

            finchRobot.wait(300);

            finchRobot.noteOn(650); // we

            finchRobot.wait(500);

            finchRobot.noteOn(600); // watched

            finchRobot.wait(1000);

            finchRobot.noteOn(550); // were so

            finchRobot.wait(300);

            finchRobot.noteOn(600); // gal -

            finchRobot.wait(400);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(600); // - lant

            finchRobot.wait(500);

            finchRobot.noteOn(400); // - ly

            finchRobot.wait(500);

            finchRobot.noteOn(300); // stream -

            finchRobot.wait(500);

            finchRobot.noteOn(100); // - ing

            finchRobot.wait(500);

            finchRobot.noteOn(750); // And

            finchRobot.wait(225);

            finchRobot.noteOff();

            finchRobot.wait(50);

            finchRobot.noteOn(750); // the 

            finchRobot.wait(225);

            finchRobot.noteOff();

            finchRobot.wait(50);

            finchRobot.noteOn(750); // rock -

            finchRobot.wait(500);

            finchRobot.noteOn(800); // - et's

            finchRobot.wait(500);

            finchRobot.noteOn(850); // red

            finchRobot.noteOff();

            finchRobot.wait(50);

            finchRobot.noteOn(850); // glare

            finchRobot.wait(1000);

            finchRobot.noteOn(800); // the

            finchRobot.wait(250);

            finchRobot.noteOn(750); // bombs

            finchRobot.wait(250);

            finchRobot.noteOn(700); // burst
            
            finchRobot.wait(500);

            finchRobot.noteOn(750); // ing

            finchRobot.wait(500);

            finchRobot.noteOn(800); // in 

            finchRobot.wait(500);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(800); // air

            finchRobot.wait(1000);

            finchRobot.noteOff();

            finchRobot.wait(100); 

            finchRobot.noteOn(800); // gave

            finchRobot.wait(500);

            finchRobot.noteOn(750); // proof

            finchRobot.wait(1000);

            finchRobot.noteOn(700); // thro '

            finchRobot.wait(250);

            finchRobot.noteOn(650); // the

            finchRobot.wait(250);

            finchRobot.noteOn(600); // night

            finchRobot.wait(1000);

            finchRobot.noteOn(550); // that

            finchRobot.wait(250);

            finchRobot.noteOn(600); // our

            finchRobot.wait(250);

            finchRobot.noteOn(650); // flag

            finchRobot.wait(500);

            finchRobot.noteOn(300); // was

            finchRobot.wait(500);

            finchRobot.noteOn(350); // still

            finchRobot.wait(500);

            finchRobot.noteOn(400); // there

            finchRobot.wait(1000);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(400); // O 

            finchRobot.wait(500);

            finchRobot.noteOn(550); // say

            finchRobot.wait(400);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(550); // does

            finchRobot.wait(400);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(550); // that

            finchRobot.wait(250);

            finchRobot.noteOn(500);

            finchRobot.wait(250);

            finchRobot.noteOn(450); // star

            finchRobot.wait(400);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(450); // spang -

            finchRobot.wait(400);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(450); // - l'd

            finchRobot.wait(400);

            finchRobot.noteOn(700); // ban - 

            finchRobot.wait(500);

            finchRobot.noteOn(800); // ne -

            finchRobot.wait(250);

            finchRobot.noteOn(750); // - er

            finchRobot.wait(250);

            finchRobot.noteOn(700); // ye -

            finchRobot.wait(250);

            finchRobot.noteOn(650); // - t

            finchRobot.wait(250);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(650); // Wa -

            finchRobot.wait(500);

            finchRobot.noteOn(600); // - ve

            finchRobot.wait(500);

            finchRobot.noteOff();

            finchRobot.wait(500);

            finchRobot.noteOn(500); // O'er

            finchRobot.wait(200);

            finchRobot.noteOff();

            finchRobot.wait(100);

            finchRobot.noteOn(500); // the

            finchRobot.wait(200);

            finchRobot.noteOn(650); // land

            finchRobot.wait(800);

            finchRobot.noteOn(700); // of

            finchRobot.wait(200);

            finchRobot.noteOn(750); // th -

            finchRobot.wait(250);

            finchRobot.noteOn(800); // - e

            finchRobot.wait(250);

            finchRobot.noteOn(850); // free

            finchRobot.wait(1000);

            finchRobot.noteOn(650); // and 

            finchRobot.wait(250);

            finchRobot.noteOn(700); // the

            finchRobot.wait(250);

            finchRobot.noteOn(750); // home

            finchRobot.wait(1000);

            finchRobot.noteOn(800); // of

            finchRobot.wait(250);

            finchRobot.noteOn(700); // the

            finchRobot.wait(250);

            finchRobot.noteOn(650);

            finchRobot.wait(1500); // brave?

            finchRobot.noteOff();
        }

        static void DisplayWelcomeScreen()
        {
            String s = "Welcome to the Finch Crontrol Application";

            Console.Clear();

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine();

            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); 

            Console.WriteLine(s);

            Console.WriteLine();

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();

            Console.WriteLine();

            Console.WriteLine("\t\tThank you for using Finch Control!");

            Console.WriteLine();

            DisplayContinuePrompt();
        }

        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            string s = "Press any key to continue.";

            Console.WriteLine();

            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);

            Console.WriteLine(s);

            Console.WriteLine();

            Console.ReadKey();

            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine();

            Console.SetCursorPosition((Console.WindowWidth - headerText.Length) / 2, Console.CursorTop);

            Console.WriteLine(headerText);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

        }

 
        #endregion
    }
}
