I came up with the idea of creating a sudoku game, since I enjoy playing sudoku. 
I assume you know how Sudoku works? Otherwise: https://www.learn-sudoku.com/sudoku-rules.html

I found a file on the internet containing 50 unsolved sudoku puzzles... You get the pleasure to solve those puzzles!
Pick a puzzle and solve it by providing commands. 

To pick a puzzle, type: "puzzle[number]", i.e. "puzzle20"

To change a square value, type: "change [square name] to [number]", i.e. "change a3 to 5"
Remember, you cannot change preset square values, and the provided number needs to be between 1 & 9. 

To quit the game, type: "quit". 
When doing this, you will also be given the option to save the game.

In order to save you time, I have almost solved   Puzzle01   for you. 
You only need to "add" the number 2 to one square. 
Solve this to make sure that my   Calculate() method in the Puzzle class   works!

The remaing 49 games are completely untouched. Play around with those too!

Enjoy!


Selected Grading Criteria:

1. Object-oriented design principles - Implementation uses OOP principles.
2. Interactive - The user can interact with the game by providing commands.
3. Data persistence. Ability to "save" and "reload" the game.


REMEMBER!! 
You need to change the file paths in the code, in order for the game to work.
There are TWO different paths:

FILE PATH ONE - to read in the Sudoku games. I will PROVIDE you with the Sudoku game text file. 
FILE PATH TWO - to save and read the serialized output.
