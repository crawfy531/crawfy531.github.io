Jumper j = new Jumper();
j.startGame();
//this is the class that initalizes the program by starting the program using the Start game and the Playagain class to start another case of the program if the player wishes
//--------------------------------------------------------------------------------------------
public class Jumper{
    public void startGame(){
        Director d = new Director();
        d.director();
        Playagain();
    }
    private void Playagain(){
        Console.Write("Play again?[y,n]: ");
        string input = Console.ReadLine();
        if(input == "y"){
            startGame();
        }
        else if(input == "n"){

        }
        else{
            Console.WriteLine("Please enter ether a y for yes or a n for no");
            Playagain();
        }
    }
//this is the class that runs initilizes a new case of other classes, runs interacts with other classes in an order to run the game, and runs the rules of the game
//----------------------------------------------------------------------------------------------
public class Director{
        SecretWords s = new SecretWords();
        Player p = new Player();
        private bool win = false;
        private bool loose = false;
    public void director(){
        s.establishLettersInWord();
        s.spaceMaker();
        s.RevealingLine();
        p.MakePlayerParachute();
        while(win != true && loose != true){
            p.playerInput();
            LetterInWord();
            s.RevealingLine();
            p.MakePlayerParachute();
        WinLoose(s.revealWord,p.playerParachute);

        }
        if(win){
            Youwon();
        }
        else{
            Youlost();
        }

        
    }
    private void LetterInWord(){
        char guess = p.letterGeuss;
        List<char>answers = s.lettersOfWord;
        List<string>displayLetters = s.revealWord;

        if(answers.Contains(guess)){
            string letter = guess.ToString();
            while(answers.Contains(guess)){
                
                int correct = answers.IndexOf(guess);
                answers.RemoveAt(correct);
                answers.Insert(correct,' ');

                displayLetters.RemoveAt(correct);
                displayLetters.Insert(correct,letter);
            }


            
        }
        else{
            p.Wrong();
        }
    }

        private void WinLoose(List<string> wordSoFar,List<string> parachute){
        if(! wordSoFar.Contains("_")){
            win = true;
        }
        else if(! parachute.Contains("  O  ")){
            loose = true;
        }
        else{
        }

        }
    private void Youwon(){
        Console.WriteLine("congratulations, you won!");
    }
    private void Youlost(){
        Console.WriteLine("Sorry... You Lost.");
    }
}
}
// this class establishes a word that the player must guess, it also shows the player's progress of the word by showing spaces or letters
//---------------------------------------------------------------------------------------------
public class SecretWords{
    private string[] listOfWords = new string[]{"happy", "angry", "depres", "animal", "platypus", "fish", "horse", "home", "silly","condition"};
    public List<string> revealWord = new List<string> ();
    public List<char> lettersOfWord = new List<char>();
    private int RandomIndex(){
        Random r = new Random();
        int index = r.Next(0,9);
        return index;
    }
    private string establishSecretWord(){
        int index = RandomIndex();
        string secretWord = listOfWords[index];
        return secretWord;
    }
    public void establishLettersInWord(){
        string secretWord = establishSecretWord();
        foreach(char letter in secretWord){
            lettersOfWord.Add(letter);
        }
        
    }
    public void spaceMaker(){
        foreach(char letter in lettersOfWord){
            revealWord.Add("_");
        }
    }
    public void RevealingLine(){
        foreach(string letter in revealWord){
            Console.Write(letter);
        }
        Console.WriteLine();

    }
}
//this is the class for the players data which inclues the players current and past guesses and players current parachute
//--------------------------------------------------------------------------------------------
public class Player{
    public List<string> playerParachute = new List<string> {" ___ ","/___\\","\\   /"," \\ /","  O  "," /|\\ "," / \\"," ","^^^^^",""};
    public char letterGeuss;
    public List<string> alreadyGuess = new List<string> {};

    public void playerInput(){
        Console.Write("Geuss a letter[]a-z]");
        string input = Console.ReadLine();
        input = input.ToLower();
        if(input.Length ==1){
            if(input.All( c => Char.IsLetter(c) )){
                if(alreadyGuess.Contains(input )){
                    Console.WriteLine($"You have already guessed {input}. Please try something else.");
                    playerInput();
                }
                else{
                    alreadyGuess.Add(input);
                    char[] cinput= input.ToCharArray();
                    letterGeuss = cinput[0];


                }
            }
            else{
                Console.WriteLine("Please enter a letter.");
                playerInput();
            }
        }
        else{
            Console.WriteLine("Please enter one letter;");
            playerInput();
        }

    }
    public void MakePlayerParachute(){
        if(! playerParachute.Contains(" \\ /")){
            Wrong();
            playerParachute.Insert(0,"  x  ");
        }
        foreach(string line in playerParachute){
            Console.WriteLine(line);
        }

    }
    public void Wrong(){
        playerParachute.RemoveAt(0);
    }
}