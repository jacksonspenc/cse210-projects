using System;

class Globals 
{
  public static List<string> _prompts = new List <string> 
  {
    "How many cats did you see today",
    "Why are you the way that you are?",
    "What is the meaning of life, the universe, and everything?",
    "What is the natural logarithm of the integral of 8x^2 sin x from 1 to 2?",
    "Who made you fill out this list?",
    "When in unix time did you execute this program?",
    "How many nuns could a nunchuck chuck if a nunchuck could chuck nuns?",
    "Should I be scared of you?",
    "Do two wrongs make a right?",
    "In this world, is it kill or be killed?",
    "Where is the worst place to try to eat a pizza?",
  };    
    
    public static Random _rng = new Random();

    public static string GetValidFilepath() 
  {
    while (true) 
    {
      Console.WriteLine("Please provide a valid filepath:");
      string path = Console.ReadLine();
      if (IsValidFilepath(path)) {
        return path;
      } else 
      {
        Console.WriteLine("Provided path was not valid, please try again:");
      }
    }
  }

  public static bool IsValidFilepath(string path) 
  {
    try 
    {
      Path.GetFullPath(path);
      return true;
    } catch (Exception) 
    {
      return false;
    }
  }

  public static bool GetUserConfirmation(string message)
  {
    while (true)
    {
      Console.WriteLine(message);
      string response = Console.ReadLine().ToLowerInvariant();
      if (response == "y") return true;
      if (response == "n") return false;
      Console.WriteLine("Please enter y or n.");
    }
  }
}

class Program { 
    public static Journal _journal = new Journal();

  static void Main(string[] args) {
    string choice;
    while (true) {
      DisplayMainMenu();
      choice = Console.ReadLine().ToLowerInvariant();
      switch (choice) {
        case "new":
        case "n":
        case "0":
          _journal.CheckUnsavedData();
          _journal = new Journal();
          break;
        case "write": 
        case "w": 
        case "1":
          _journal.AddEntryRandom();
          break;
        case "display":
        case "d":
        case "2":
          if (_journal._contents.Count == 0) {
            Console.WriteLine("Current journal contains no entires. Please create or load a journal first.");
          } else {
            _journal.Display();
          }
          break;
        case "load":
        case "l":
        case "3":
          // load a journal from a file
          _journal.CheckUnsavedData();
          Console.WriteLine("Enter the path of the journal file you would like to load:");
          string filepath = Console.ReadLine();

          if (!File.Exists(filepath)){
            Console.WriteLine($"Failed to load file from {filepath}: file does not exist");
            continue;
          }

          string serialized_data;
          using (StreamReader reader = new StreamReader(filepath)) {
            serialized_data = reader.ReadLine();
          }

          _journal = new Journal();
          _journal.LoadFromSerializedString(serialized_data);
          _journal._filepath = filepath;

          break;
        case "save":
        case "s":
        case "4":
          // save journal to a file
          _journal.Save();
          break;
        case "quit":
        case "q":
        case "5":
          // exit the program
          _journal.CheckUnsavedData();
          System.Environment.Exit(1);
          break;
        default:
          Console.WriteLine($"No command matching {choice} was found");
          break;
      }
    }
    }

  static void DisplayMainMenu()
    {
        Console.Write("""
        What would you like to do?
        New (n, 0)
        Write (w, 1)
        Display (d, 2)
        Load (l, 3)
        Save (s, 4)
        Quit (q, 5)

        """);
    }
}

class Journal {
  public List<Entry> _contents = new List<Entry>{};
  public string _filepath = null;
  public bool _unsaved_data_exists = false;

  public void AddEntryRandom() {
    Entry new_entry = new Entry();
    new_entry.GetFromUser();
    _contents.Add(new_entry);
    _unsaved_data_exists = true;
  }

  public void Save() {
    if (_filepath == null) {
      Console.WriteLine("Where would you like to save your journal?");
      string new_filepath = Globals.GetValidFilepath();

      if (File.Exists(new_filepath)) {
        Console.WriteLine("There is already a file at the specified path. Would you like to overwrite it? (y/n)");
        string response = Console.ReadLine().ToLowerInvariant();
        if (response == "y") {
          _filepath = new_filepath;
        } else {
          Console.WriteLine("Operation aborted");
          return;
        }
      } else {
        _filepath = new_filepath;
      }
    } else {
      if (!Globals.IsValidFilepath(_filepath)) {
        Console.WriteLine("Error: filepath associated with this journal is invalid, cannot save");
      }
    }
    
    using (StreamWriter outputFile = new StreamWriter(_filepath)) {
      outputFile.WriteLine(Serialize());
    }
    _unsaved_data_exists = false;
  }
  
  public void CheckUnsavedData() {
    if (_unsaved_data_exists) {
      if (Globals.GetUserConfirmation(
          "Current journal contains unsaved data, would you like to save? (y/n)")){
        Save();
      }
    }

  }
  public void Display() {
    foreach (Entry entry in _contents){
      entry.Display();
    }
  }

  public string Serialize() {
    var serialized_data = "";
    foreach (Entry entry in _contents){
      serialized_data += entry.Serialize();
      serialized_data += "~"; 
    }
    serialized_data.Remove(serialized_data.Length - 1);
    return serialized_data;
  }

  public void LoadFromSerializedString(string data) {
    string[] substrs = data.Split("~");
    foreach (string substr in substrs) {
      if (substr == "") {
        continue;
      }
      Entry entry = new Entry(false);
      if (entry.LoadFromSerializedString(substr)) {
        _contents.Add(entry);
      }
    }
  }
}

class Entry {
  public string _entry = "";
  public string _date = "";
  public string _prompt = "";

  public Entry(bool random = true) {
    if (random) {
      _prompt = Globals._prompts[Globals._rng.Next(0, Globals._prompts.Count)];
    }
  }

  public void GetFromUser() {
    Console.WriteLine(_prompt);
    _entry = Console.ReadLine().Replace("|", "");
    _entry = _entry.Replace("~", "");
    _date = DateTime.Now.ToShortDateString();
  }

  public void Display() {
    Console.WriteLine(_date);
    Console.WriteLine(_prompt);
    Console.WriteLine(_entry);
  }

  public string Serialize() {
    return $"{_date}|{_prompt}|{_entry}";
  }

  public bool LoadFromSerializedString(string data) {
    string[] substrs = data.Split("|");
    try {
      _date = substrs[0];
      _prompt = substrs[1];
      _entry = substrs[2];
      return true;
    } catch (Exception) {
      Console.WriteLine($"Error: Invalid journal data detected: {data}");
      Console.WriteLine("Parsing for entry failed, skipping...");
      return false;
    }
  }
}