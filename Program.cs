using System;

Console.WriteLine("Enter your username: ");
string name = Console.ReadLine();
if (name.Length ==0)
{
    Console.WriteLine($"Hello User");

} else { Console.WriteLine($"Hello {name}"); }

static void Main()
{
    string user;
    do
    {
        Console.WriteLine();
        Console.WriteLine("'G' or 'Game' to play the game");
        Console.WriteLine("'S' or 'Shop' to go to the store");
        Console.WriteLine("'Exit' to exit the game");
        Console.Write("Enter your selection: ");
        user = Console.ReadLine().ToLower();
        if (user == "g" || user == "game")
        {
            Console.WriteLine("Starting up!");
            Playgame();
        }
        else if (user == "s" || user == "shop")
        {
            Console.WriteLine("Go to store!");
            Shopping();
        }
        else if (user == "exit")
        {
            Console.WriteLine("Game stop!");
            Console.WriteLine("Have a good one!");
            break;
        }
        else
        {
            Console.WriteLine($"'{user}' is an invalid selection");
            Console.WriteLine("");
        }

    } while (!(user == "g" || user == "game" || user == "s" || user == "shop" || user == "exit"));
}

static void Playagain()
{
    bool playmore = false;
    while (!playmore)
    {
        Console.WriteLine("Play again? (Yes/No):");
        Console.WriteLine("<< Enter 'B' To go back: ");

        string playAgain = Console.ReadLine().ToLower();
        if (playAgain == "y" || playAgain == "yes")
        {
            Playgame();
            break;
        }
        else if (playAgain == "n" || playAgain == "no")
        {
            playmore = true;
            break;
        }
        else if (playAgain == "b" || playAgain == "back")
        {
            Console.WriteLine();
            Main();
            break;
        }
        else
        {
            Console.WriteLine("Something went wrong!");
            Console.Write("Invalid input only 'Yes' or 'No': ");
            playAgain = Console.ReadLine().ToLower();
        }

    }
}
static void Playgame()
{

    string character;
    do
    {
        Console.WriteLine("Please enter your selection \"X\" or \"O\"");
        Console.WriteLine("Enter \"exit\" to stop the game");

        character = Console.ReadLine().ToLower();
        

        if (character == "x")
        {
            StartgameX();

        }
        else if (character == "o")
        {
            StartgameO();
        }
        else if (character == "exit") 
        {
            break;
        }
        else
                {
            Console.WriteLine($"'{character}' is an invalid selection");
            Console.WriteLine("");
        }
    } while (!(character == "x" || character == "o" || character == "exit"));
}
static void Shopping()
{
    int numStores;
    bool ValidInput = false;
    while (!ValidInput)
    {

        Console.Write("How many stores you went to? ");
        string storesInput = Console.ReadLine();
        // The function int.TryParse(storesInput, out numStores) will return only boolean value
        // Check if user input is integer -> it returns: true else: false
        if (int.TryParse(storesInput, out numStores) && numStores > 0)
        {
            ValidInput = true;
            double[][] storeData = new double[numStores][];

            for (int storeNum = 0; storeNum < numStores; storeNum++)
            {
                Console.Write($"The Number of stores: {storeNum + 1} ");
                Console.Write("How many items you purchased? ");

                int numItems = 0; //Fixing scoping problem by assign the default value for variable
                bool ValidItems = false;

                while (!ValidItems)
                {
                    string itemsInput = Console.ReadLine();
                    if (int.TryParse(itemsInput, out numItems) && numItems >= 0)
                    {
                        ValidItems = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid positive integer for the number of items.");
                        Console.Write($"For store Number {storeNum + 1}, how many items were purchased? ");
                    }
                }

                storeData[storeNum] = new double[numItems];

                for (int item = 0; item < numItems; item++)
                {
                    Console.Write($"Enter the cost of item #{item + 1}: $");

                    double itemCost = 0.0; //Fixing scoping problem by assign the default value for variable
                    bool isValidItemCost = false;

                    while (!isValidItemCost)
                    {
                        string costInput = Console.ReadLine();

                        if (double.TryParse(costInput, out itemCost) && itemCost >= 0)
                        {
                            isValidItemCost = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid non-negative double for the item cost.");
                            Console.Write($"Enter the cost of item #{item + 1}: $");
                        }
                    }

                    storeData[storeNum][item] = itemCost;
                }
            }

            // Display all the data at the end
            for (int storeNum = 0; storeNum < numStores; storeNum++)
            {
                Console.WriteLine();
                Console.WriteLine("You went to:");
                Console.WriteLine($"\n- Store number {storeNum + 1}:");

                /*double totalCost = storeData[storeNum].Sum();
                Console.WriteLine($"The total cost: ${totalCost}");*/
                //or I can loop through the row and plus every column
                double totalCost =0;
                for (int j = 0; j < storeData[storeNum].Length; j++)
                {
                    totalCost += storeData[storeNum][j];
                }
                Console.WriteLine($"The total cost: ${totalCost}");

                double maxItemCost = storeData[storeNum].Max();
                Console.WriteLine($"The most expensive item: ${maxItemCost}");

                double avgCost = totalCost / storeData[storeNum].Length;
                Console.WriteLine($"The average cost of items: ${avgCost}");
            }
            Console.WriteLine();
            Main();
            break;
        }
        else if (int.TryParse(storesInput, out numStores) && numStores == 0)
        {
            Console.WriteLine("You didn't go to any stores. Boring!");
            Console.WriteLine();
            Main();
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid positive integer for the number of stores.");
        }
    }
}
static void DisplayBoard(int[][] gridmap)
{
    Console.Clear(); //It will clear the previous code every time this function call.
    Console.WriteLine("*********");
    foreach (int[] row in gridmap)
    {
        foreach (int colValue in row)
        {
            if (colValue == 999)
            {
                Console.Write("[X]");
            }
            else if (colValue == 777)
            {
                Console.Write("[O]");
            }
            else
            {
                Console.Write($"[{colValue}]");
            }
           

        }
        Console.WriteLine();
    }
    Console.WriteLine("*********");

}
static bool CheckForWinner(int[][] grid, int playerValue)
{
    // Check rows
    for (int row = 0; row < grid.Length; row++)
    {
        if (grid[row][0] == playerValue && grid[row][1] == playerValue && grid[row][2] == playerValue)
            return true;
    }

    // Check columns
    for (int col = 0; col < grid[0].Length; col++)
    {
        if (grid[0][col] == playerValue && grid[1][col] == playerValue && grid[2][col] == playerValue)
            return true;
    }

    // Check diagonals
    if ((grid[0][0] == playerValue && grid[1][1] == playerValue && grid[2][2] == playerValue) ||
        (grid[0][2] == playerValue && grid[1][1] == playerValue && grid[2][0] == playerValue))
        return true;

    return false;
}
static bool CheckForWinnerX(int[][] grid)
{
    return CheckForWinner(grid, 999);
}
static bool CheckForWinnerO(int[][] grid)
{
    return CheckForWinner(grid, 777);
}
static void StartgameX()
{
    int rowsCount = 3;
    int colsNum = 3;
    int[][] gridmap = new int[rowsCount][];
    int[] moves = new int[9];
    int[] userSelection = { 1, 2, 3, 4, 5, 6, 7, 8, 9};
    int eachMove = 0;

    for (int i = 0; i < rowsCount; i++)
    {
        gridmap[i] = new int[colsNum];
    }

    int counter = 1;
    for (int rows = 0; rows < gridmap.Length; rows++)
    {
        for (int cols = 0; cols < gridmap[rows].Length; cols++)
        {
            gridmap[rows][cols] = counter;
            counter++;
        }
    }

    Random random = new Random();
    bool gameOver = false;

    while (!gameOver)
    {
        DisplayBoard(gridmap);
        // player's turn
        Console.Write("Enter a number from 1 to 9: ");
        
        int userText = int.Parse(Console.ReadLine());

        bool isMoveExisted = true;
        while (isMoveExisted || userText < 1 || userText > 9)
        {
            isMoveExisted = false;

            foreach (int move in moves)
            {
                if (move == userText)
                {
                    isMoveExisted = true;
                    break;
                }
            }

            if (isMoveExisted || userText < 1 || userText > 9)
            {
                Console.Write("Invalid number. Please enter a number from 1 to 9: ");
                userText = int.Parse(Console.ReadLine());
            }
        }
        moves[eachMove++] = userText;
        int userRow = 0;
        int userCol = 0;

        for (int row = 0; row < gridmap.Length; row++)
        {
            for (int col = 0; col < gridmap[row].Length; col++)
            {
                if (gridmap[row][col] == userText)
                {
                    gridmap[row][col] = 999;
                    userRow = row;
                    userCol = col;
                }
            }
        }
        // Check for player win
        if (CheckForWinner(gridmap, 999))
        {
            DisplayBoard(gridmap);
            Console.WriteLine("You win!");
            gameOver = true;
            break;
        }
        // Check for tie
        bool isTie = true;
        foreach (int[] row in gridmap)
        {
            foreach (int colValue in row)
            {
                if (colValue != 999 && colValue != 777)
                {
                    isTie = false;
                    break;
                }
            }
        }

        if (isTie)
        {
            DisplayBoard(gridmap);
            Console.WriteLine("It's a tie!");
            break;
        }

        // Computer's turn
        int computerText = random.Next(1, 10);

        //Console.WriteLine($"computer: {computerText}");
      /*Using array exitst method to check if random generator is in the array moves[move]
        Lamda used as for loop:
        foreach (int move in moves)
        {
            if(move == computerText)
            {
                break;
            }
        };*/
        while (Array.Exists(moves, move => move == computerText))
        {
            computerText = random.Next(1, 10);
        }
        
        moves[eachMove++] = computerText;
        /*foreach (int m in moves)
        {
            Console.WriteLine("move "+m);
        };*/
        int computerRow = 0;
        int computerCol = 0;
        for (int row = 0; row < gridmap.Length; row++)
        {
            for (int col = 0; col < gridmap[row].Length; col++)
            {
                if (gridmap[row][col] == computerText)
                {
                    gridmap[row][col] = 777;
                    computerRow = row;
                    computerCol = col;
                }
            }
        }
        // Check for computer win
        if (CheckForWinner(gridmap, 777))
        {
            DisplayBoard(gridmap);
            Console.WriteLine("You lost");
            Console.WriteLine("Computer wins!");
            gameOver = true;
            break;
        }

        // Check for a tie after the computer's move
        isTie = true;
        foreach (int[] row in gridmap)
        {
            foreach (int colValue in row)
            {
                if (colValue != 999 && colValue != 777)
                {
                    isTie = false;
                    break;
                }
            }
        }
        if (isTie)
        {
            DisplayBoard(gridmap);
            Console.WriteLine("It's a tie!");
            break;
        }
    }
    Playagain();
}
static void StartgameO()
{
    int rowsCount = 3;
    int colsNum = 3;
    int[][] gridmap = new int[rowsCount][];
    int[] moves = new int[9];
    int[] userSelection = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int eachMove = 0;

    for (int i = 0; i < rowsCount; i++)
    {
        gridmap[i] = new int[colsNum];
    }

    int counter = 1;
    for (int rows = 0; rows < gridmap.Length; rows++)
    {
        for (int cols = 0; cols < gridmap[rows].Length; cols++)
        {
            gridmap[rows][cols] = counter;
            counter++;
        }
    }

    Random random = new Random();
    bool gameOver = false;

    while (!gameOver)
    {
        // Computer's turn
        //This code works! But too long.
        int computerText;
        bool validMove = true;
        while (true)
        {
            computerText = random.Next(1, 10);
            Console.WriteLine($"comput" +$"er: {computerText}");

            for (int i = 0; i < moves.Length; i++)
            {
                if (moves[i] == computerText)
                {
                    computerText = random.Next(1, 10);
                    validMove = false;
                    break;
                }
            }

            if (validMove)
            {
                break;
            }
            else
            {
                validMove = true;
            }
        }
        moves[eachMove++] = computerText;
        foreach (int m in moves)
        {
            Console.WriteLine("move " + m);
        };
        int computerRow = 0;
        int computerCol = 0;
        for (int row = 0; row < gridmap.Length; row++)
        {
            for (int col = 0; col < gridmap[row].Length; col++)
            {
                if (gridmap[row][col] == computerText)
                {
                    gridmap[row][col] = 999;
                    computerRow = row;
                    computerCol = col;
                }
            }
        }
        //Display gridmap here!
        DisplayBoard(gridmap);

        if (CheckForWinner(gridmap, 999))
        {
            DisplayBoard(gridmap);
            Console.WriteLine("You lost");
            Console.WriteLine("Computer wins!");
            gameOver = true;
            break;
        }
        bool isTie = true;
        foreach (int[] row in gridmap)
        {
            foreach (int colValue in row)
            {
                if (colValue != 999 && colValue != 777)
                {
                    isTie = false;
                    break;
                }
            }
        }
        if (isTie)
        {
            DisplayBoard(gridmap);
            Console.WriteLine("It's a tie!");
            break;
        }

        // player's turn
        Console.Write("Enter a number from 1 to 9: ");
        int userText = int.Parse(Console.ReadLine());
        bool isMoveExisted = true;
        while (isMoveExisted || userText < 1 || userText > 9)
        {
            isMoveExisted = false;

            foreach (int move in moves)
            {
                if (move == userText)
                {
                    isMoveExisted = true;
                    break;
                }
            }

            if (isMoveExisted || userText < 1 || userText > 9)
            {
                Console.Write("Invalid number. Please enter a number from 1 to 9: ");
                userText = int.Parse(Console.ReadLine());
            }
        }
        moves[eachMove++] = userText;
        int userRow = 0;
        int userCol = 0;

        for (int row = 0; row < gridmap.Length; row++)
        {
            for (int col = 0; col < gridmap[row].Length; col++)
            {
                if (gridmap[row][col] == userText)
                {
                    gridmap[row][col] = 777;
                    userRow = row;
                    userCol = col;
                }
            }
        }

        if (CheckForWinner(gridmap, 777))
        {
            DisplayBoard(gridmap);
            Console.WriteLine("You win!");
            gameOver = true;
            break;
        }
        isTie = true;
        foreach (int[] row in gridmap)
        {
            foreach (int colValue in row)
            {
                if (colValue != 999 && colValue != 777)
                {
                    isTie = false;
                    break;
                }
            }
        }
        if (isTie)
        {
            DisplayBoard(gridmap);
            Console.WriteLine("It's a tie!");
            break;
        }
    }
    Playagain();
}
Main();