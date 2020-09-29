using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net.Http.Headers;
using System.Net;

[Serializable()]
public class SudokuLibrary
{
    // stores all suduko puzzles
    public List<Puzzle> puzzles { get; set; }

    public void PrintPuzzleMenu()
    {
        Console.WriteLine("PUZZLE MENU: ");
        foreach (var p in puzzles)
        {
            if (p.Complete)
            {
                Console.WriteLine($"{p.Name}: Completed");
            }
            else
            {
                Console.WriteLine(p.Name);
            }
        }
    }
    public Puzzle SelectAPuzzle(string name)
    {
        return this.puzzles.Find(puzzle => puzzle.Name == name);
    }

    public void SaveGame()
    {
        //creates a FileStream to write the serialized output
        //to a file on the computer
        FileStream fileStream = new FileStream(@"C:\Users\Koala\Documents\C#\Sample.dat", FileMode.Create);

        //creates a BinaryFormatter object to serialize the object
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, this);
    }
}

[Serializable()]
public class Puzzle
{
    // stores all square objects 
    public List<Square> squares { get; set; }


    // used for iteration 
    string[] Rows = new string[] {"a1","a2","a3","a4","a5","a6","a7","a8","a9",
                                  "b1","b2","b3","b4","b5","b6","b7","b8","b9",
                                  "c1","c2","c3","c4","c5","c6","c7","c8","c9",
                                  "d1","d2","d3","d4","d5","d6","d7","d8","d9",
                                  "e1","e2","e3","e4","e5","e6","e7","e8","e9",
                                  "f1","f2","f3","f4","f5","f6","f7","f8","f9",
                                  "g1","g2","g3","g4","g5","g6","g7","g8","g9",
                                  "h1","h2","h3","h4","h5","h6","h7","h8","h9",
                                  "i1","i2","i3","i4","i5","i6","i7","i8","i9" };

    string[] Columns = new string[] {"a1","b1","c1","d1","e1","f1","g1","h1","i1",
                                     "a2","b2","c2","d2","e2","f2","g2","h2","i2",
                                     "a3","b3","c3","d3","e3","f3","g3","h3","i3",
                                     "a4","b4","c4","d4","e4","f4","g4","h4","i4",
                                     "a5","b5","c5","d5","e5","f5","g5","h5","i5",
                                     "a6","b6","c6","d6","e6","f6","g6","h6","i6",
                                     "a7","b7","c7","d7","e7","f7","g7","h7","i7",
                                     "a8","b8","c8","d8","e8","f8","g8","h8","i8",
                                     "a9","b9","c9","d9","e9","f9","g9","h9","i9"};

    string[] Boxes = new string[] {"a1","a2","a3","b1","b2","b3","c1","c2","c3",
                                   "a4","a5","a6","b4","b5","b6","c4","c5","c6",
                                   "a7","a8","a9","b7","b8","b9","c7","c8","c9",
                                   "d1","d2","d3","e1","e2","e3","f1","f2","f3",
                                   "d4","d5","d6","e4","e5","e6","f4","f5","f6",
                                   "d7","d8","d9","e7","e8","e9","f7","f8","f9",
                                   "g1","g2","g3","h1","h2","h3","i1","i2","i3",
                                   "g4","g5","g6","h4","h5","h6","i4","i5","i6",
                                   "g7","g8","g9","h7","h8","h9","i7","i8","i9"};

    public string Name { get; set; }
    
    // true if puzzle is solved
    public bool Complete { get; set; }
    public void PrintPuzzle()
    {
        Console.WriteLine("\n    1 2 3  4 5 6  7 8 9");
        Console.WriteLine("   +-----+------+------+");

        // stored values for each row
        string[] a = new string[9];

        char x = 'a'; 
        int i = 0; 
        int j = 0;

        foreach (string item in Rows)
        {
            Square temp = this.squares.Find(square => square.Name == item);
            a[i] = temp.Value.ToString();

            if (a[i] == "0")
            {
                a[i] = ".";
            }
            i++;

            if (i == 9)
            {
                i = 0;
                Console.WriteLine($" {x} |{a[0]} {a[1]} {a[2]}| {a[3]} {a[4]} {a[5]}| {a[6]} {a[7]} {a[8]}|");
                j++; 
                x++;

                if (j == 3)
                {
                    j = 0;
                    Console.WriteLine("   +-----+------+------+");
                }
            }
        }
    }

    public void Calculate()
    {
        // to store Rows, Columns and Boxes
        List<string> bigList = new List<string>();

        bigList.AddRange(Rows);
        bigList.AddRange(Columns);
        bigList.AddRange(Boxes);

        int total = 0; 
        int i = 0;
        foreach (string item in bigList)
        {
            Square temp = this.squares.Find(square => square.Name == item);
            total += temp.Value;
            i++;

            if (i == 9)
            {
                // if the sum of a row, column och box is not 45
                if (total != 45)
                {
                    this.Complete = false;
                    return;
                }
                total = 0;
                i = 0;
            }
        }
        // if every row, column and box sums up to 45, the puzzle is complete
        this.Complete = true;
    }
}

[Serializable()]
public class Square
{
    public string Name { get; set; }
    public int Value { get; set; }

    // to revent player from updating preset numbers
    // true if value is 0 when the object is created
    public bool Updateable { get; set; }
}

public class Game
{
    protected SudokuLibrary sudokufun;
    protected string introduction = "";
    protected Puzzle playPuzzle;
    public void Start()
    {
        Console.WriteLine(introduction);
        InputLoop();
    }
    public void InputLoop()
    {
        while (true)
        {
            // forces player to pick a puzzle to play at the start
            // OR
            // forces player to pick a new puzzle, once player finishes a puzzle
            while (playPuzzle == null || playPuzzle.Complete)
            {
                Console.WriteLine("Pick a Puzzle from the menu by typing its name!");
                sudokufun.PrintPuzzleMenu();
                string inputPuzzleName = Console.ReadLine();
                playPuzzle = sudokufun.puzzles.Find(puzzle => puzzle.Name == inputPuzzleName);
            }

            playPuzzle.PrintPuzzle();
            Console.WriteLine("To change a square, type: 'change __ to _'  (Example: change a3 to 5)");
            string input = Console.ReadLine();

            if (input == "quit")
            {
                Console.WriteLine("Do you want to save before leaving? |yes/no/cancel|");
                while (true)
                {
                    input = Console.ReadLine();
                    if (input == "yes")
                    {
                        sudokufun.SaveGame();
                        Console.WriteLine("Game saved");
                        return;
                    }

                    else if (input == "no")
                    {
                        return;
                    }

                    else if (input == "cancel")
                    {
                        break;    
                    }

                    else
                    {
                        Console.WriteLine("Wrong format. Try again.");
                    }
                }
            }

            if (input.StartsWith("change"))
            {
                var regex = new Regex(@"change ([\w\s]+) to ([\w\s]+)", RegexOptions.IgnoreCase);
                var match = regex.Match(input);

                if (match.Success)
                {
                    try
                    {
                        string name = match.Groups[1].Value;
                        int value = Int16.Parse(match.Groups[2].Value);

                        Square temp = playPuzzle.squares.Find(square => square.Name == name);

                        if (temp == null)
                        {
                            Console.WriteLine("The square you tried to change does not exist.");
                        }

                        else if (temp.Updateable == true && value >= 1 && value <= 9)
                        {
                            temp.Value = value;
                        }

                        else
                        {
                            Console.WriteLine("You cannot update preset numbers. Pick a number between 1 & 9.");
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                // calculates puzzle (to see whether playPuzzle.Completed is true or false)
                playPuzzle.Calculate();

                if (playPuzzle.Complete)
                {
                    Console.WriteLine("Congratulations! You finished the puzzle successfully!");
                }
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Game game = new Data();  
        game.Start();       
    }
}