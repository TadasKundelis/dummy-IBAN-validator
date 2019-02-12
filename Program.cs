using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IBAN_validator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is IBAN validator");
            while (true)
            {
                Console.WriteLine("\nType F To validate a file or S to validate a single code:");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "f")
                {
                    Console.WriteLine("Please enter the path to file location:");
                    string inputDirectory = Console.ReadLine();
                    Console.WriteLine("Please enter the file name:");
                    string inputFileName = Console.ReadLine();
                    string inputPath = $"{inputDirectory}\\{inputFileName}";
                    string outputFileName = null;
                    string outputPath = null;
                    try
                    {
                        outputFileName = Path.GetFileNameWithoutExtension(inputPath);
                        outputPath = $"{inputDirectory}\\{outputFileName}.out";
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"\n{exception.Message}");
                        continue;
                    }

                    if (Path.GetExtension(inputPath) != ".txt")
                    {
                        Console.WriteLine("\nWrong Format! Only .txt files are allowed.");
                        continue;
                    }

                    if (File.Exists(inputPath))
                    {
                        using (StreamWriter fileWriter = new StreamWriter(outputPath))
                        {
                            using (StreamReader fileReader = new StreamReader(inputPath))
                            {
                                while (fileReader.Peek() != -1)
                                {
                                    string code = fileReader.ReadLine();
                                    string output = String.Format("{0};{1}", code, Validator.Run(code));
                                    fileWriter.WriteLine(output);
                                }
                            }
                        }
                        Console.WriteLine($"\nDone, output was written to {outputPath}");
                    }
                    else
                    {
                        Console.WriteLine("\nFile not found! Please try again.");
                    }
                }
                else if (choice.ToLower() == "s")
                {
                    Console.WriteLine("Please enter the code:");
                    string code = Console.ReadLine();
                    Console.WriteLine("\n" + Validator.Run(code));
                }
                else
                {
                    Console.WriteLine("\nInvalid selection. Please try again.");
                }
            }
        }
    }
}

        