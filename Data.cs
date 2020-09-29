using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;


class Data : Game
{
    string name;
    string line;
    int value = 0;
    char key = 'a';
    bool updateableValue;
    
    // creates a SudukoLibrary object
    SudokuLibrary sudoku = new SudokuLibrary
    {
        puzzles = new List<Puzzle>()
    };

    public Data()
    {
        string checkFile = @"C:\Users\Koala\Documents\C#\Sample.dat";

        if (File.Exists(checkFile))
        {
            //Deserializes puzzle object
            //Creates a FileStream to read the serialized object
            FileStream fileStream = new FileStream(checkFile, FileMode.Open);

            //Creates a BinaryFormatter and deserializes the object
            BinaryFormatter formatter = new BinaryFormatter();
            SudokuLibrary deserializedSample = (SudokuLibrary)formatter.Deserialize(fileStream);

            fileStream.Close();
            sudokufun = deserializedSample;
        }


        // if there is no "saved game" file on the player's computer
        else
        {
            // Reads the file and displays it line by line  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"C:\Users\Koala\Documents\C#\sudokufiles\sudoku_easy.txt");

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("Puzzle "))
                {
                    // resets key everytime a new puzzle is read
                    key = 'a';
                    name = $"puzzle{line.Split("Puzzle ")[1]}";
                    sudoku.puzzles.Add(new Puzzle
                    {
                        Name = name,
                        Complete = false,
                        squares = new List<Square>()
                    }
                    );
                }
                else
                {
                    // resets value everytime a new non-"Puzzle " line is read
                    value = 0;

                    // reads same line until all 9 numbers have been added to dictionary
                    for (int num = 1; num < 10; num++)
                    {
                        Puzzle temp = sudoku.puzzles.Find(puzzle => puzzle.Name == name);

                        // 0 in file means a non-preset value
                        if (line[value] == '0')
                        {
                            updateableValue = true;
                        }

                        else
                        {
                            updateableValue = false;
                        }

                        temp.squares.Add(new Square { 
                                                        Name = string.Concat(key, num), 
                                                        Value = Int16.Parse(line[value].ToString()), 
                                                        Updateable = updateableValue 
                                                    });
                        value++; value++;
                    }

                    // increases key by one char before new line is read
                    key++;
                }
            }
            file.Close();
            sudokufun = sudoku;
        }
        introduction = "Welcome to Suduko!\n";
    }
}
    

