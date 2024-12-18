using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using static SaveFiles;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;
using MySqlX.XDevAPI.Relational;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Exception for switch case shit

/*
1.    Method Overloading(graped)

2.    Method Overriding(on abstract method)

3.    Structure(graped)

4.    Encapsulation(graped)

5.    “this” keyword(graped)

6.    Constructors(graped)

7.    Inheritance(graped)

8.    Abstract Classes(graped)

9.    Abstract Methods(graped)

10. Polymorphism(graped)

11. Interfaces(graped)

12.Exception(graped)

madami nakong nilagay "exception handling" not sure if okay na siya completely


 */
public interface ISave
{
    void SaveFile(Character attributes);
    void SaveFile(Stats stats, Stats2 stats2);
    void EmptyFile(string path);
    void SaveFileQuery(Character attributes, Stats stats, Stats2 stats2, int num);//Changes
    void LoadDatabase(int num);//Changes
    void DropTable(int num);//Changes
}




public class SaveFiles : ISave
{
    private string filePath;


    public void EmptyFile(string path)
    {
        File.WriteAllText(path, string.Empty);
    }
    public SaveFiles(string filePath)
    {
        this.filePath = filePath;


    }
    public class FileHandler
    {
        private string filePath;

        public FileHandler(string filePath)
        {
            this.filePath = filePath;
        }

        public void CheckAndReadFile()
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length == 0)
                {
                    Console.WriteLine("\nThe file is empty.");
                }
                else if (fileInfo.Length > 0)
                {
                    Console.WriteLine("\nthis file is not empty.");
                }
                else
                {
                    string fileContents = File.ReadAllText(filePath);
                    Console.WriteLine(fileContents);
                }
            }
            else
            {
                Console.WriteLine("\nThe file does not exist.");
            }
        }
        public void CheckAndReadFile_load()
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length == 0)
                {
                    Console.WriteLine("\nThe file is empty.");
                }
                else
                {
                    string fileContents = File.ReadAllText(filePath);
                    Console.WriteLine(fileContents);
                }
            }
            else
            {
                Console.WriteLine("\nThe file does not exist.");
            }
        }
    }
    private void CreatSaveFile()
    {
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath)) ;
        }
        else
        {
            Console.WriteLine("\nSave File found.");
        }
    }

    //Transferring of "Temp Save" to txt for local storage
    public void SaveFile(Character attributes)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"Username: {attributes.Username}");
            writer.WriteLine($"Gender: {attributes.Gender}");
            writer.WriteLine($"Hair: {attributes.Hair}");
            writer.WriteLine($"Hair Color: {attributes.HairColor}");
            writer.WriteLine($"Eye Color: {attributes.EyeColor}");
            writer.WriteLine($"Facial Hair: {attributes.FacialHair}");
            writer.WriteLine($"Facial Expression: {attributes.FacialExpression}");
            writer.WriteLine($"Top: {attributes.Top}");
            writer.WriteLine($"Bottoms: {attributes.Bottoms}");
            writer.WriteLine($"Footwear: {attributes.Footwear}");
            writer.WriteLine($"Headwear: {attributes.Headwear}");
            writer.WriteLine($"Accessories: {attributes.Accessories}");
            writer.WriteLine($"Melee Weapon: {attributes.MeleeWeapon}");
            writer.WriteLine($"Ranged Weapon: {attributes.RangedWeapon}");
            writer.WriteLine($"Throwables: {attributes.Throwables}");
            writer.WriteLine($"Special Weapon: {attributes.SpecialWeapon}");
            writer.WriteLine($"Boosts: {attributes.Boosts}");
            writer.WriteLine($"Healing Items: {attributes.HealingItems}");
            writer.WriteLine($"Pet Companion: {attributes.PetCompanion}");
            writer.WriteLine($"Vehicle: {attributes.Vehicle}");
            writer.WriteLine($"Nightvision: {attributes.Nightvision}");
            writer.WriteLine($"Noise Sensitivity: {attributes.NoiseSensitivity}");
            writer.WriteLine($"Skillpoints: {attributes.Skillpoints}");
        }

    }

    public void SaveFile(Stats stats, Stats2 stats2)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Strength: {stats.str}");
            writer.WriteLine($"Speed: {stats.spd}");
            writer.WriteLine($"Stealth: {stats2.stl}");
            writer.WriteLine($"Defense: {stats2.def}");
        }
    }
    public void SaveFileQuery(Character char1, Stats stats, Stats2 stats2, int num)

    {   //hindi babaguhin
        // string emptyrows = "\r\nINSERT INTO [dbo].[Stats] (\r\n , \r\n    [Username], \r\n    [Gender], \r\n    [Hair], \r\n    [HairColor], \r\n    [EyeColor], \r\n    [FacialHair], \r\n    [FacialExpression], \r\n    [TopClothing], \r\n    [Bottoms], \r\n    [Footwear], \r\n    [Headwear], \r\n    [Accessories], \r\n    [MeleeWeapon], \r\n    [RangedWeapon], \r\n    [Throwables], \r\n    [SpecialWeapon], \r\n    [Boosts], \r\n    [HealingItems], \r\n    [PetCompanion], \r\n    [Vehicle], \r\n    [Nightvision], \r\n    [NoiseSensitivity], \r\n    [Skillpoints], \r\n    [Strength], \r\n    [Speed], \r\n    [Stl], \r\n    [Defence]\r\n) VALUES\r\n(1, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, 0),\r\n(2, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, 0),\r\n(3, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, 0),\r\n(4, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, 0),\r\n(5, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0, 0, 0, 0, 0, 0);\r\n";
        string insertQueryString = "INSERT INTO dbo.Stats(Id,Username, Gender, Hair, HairColor, EyeColor, FacialHair, FacialExpression, TopClothing, Bottoms, Footwear, Headwear, Accessories, MeleeWeapon, RangedWeapon, Throwables, SpecialWeapon, Boosts, HealingItems, PetCompanion, Vehicle, Nightvision, NoiseSensitivity, Skillpoints, Strength, Speed, Stl, Defense) " +
                           "VALUES ('" + num + "', '" + char1.Username + "', '" + char1.Gender + "', '" + char1.Hair + "', '" + char1.HairColor + "', '" + char1.EyeColor + "', '" + char1.FacialHair + "', '" + char1.FacialExpression + "', '" + char1.Top + "', '" + char1.Bottoms + "', '" + char1.Footwear + "', '" + char1.Headwear + "', '" + char1.Accessories + "', '" + char1.MeleeWeapon + "', '" + char1.RangedWeapon + "', '" + char1.Throwables + "', '" + char1.SpecialWeapon + "', '" + char1.Boosts + "', '" + char1.HealingItems + "', '" + char1.PetCompanion + "', '" + char1.Vehicle + "', " + (char1.Nightvision ? "1" : "0") + ", " + (char1.NoiseSensitivity ? "1" : "0") + ", " + char1.Skillpoints + ", " + stats.str + ", " + stats.spd + ", " + stats2.stl + ", " + stats2.def + ")";
        string asa = "SET IDENTITY_INSERT [dbo].[Stats] ON;";
        //babaguhin kasi connection string eto nung database na ginawa mo sa lappy mo
        string databaseConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog =""C:\USERS\JARED\SOURCE\REPOS\BRAIN STEW\BRAIN STEW\DATABASE1.MDF""; Integrated Security = True ";


        SqlConnection connection = new SqlConnection(databaseConnectionString);
        connection.Open();


        SqlCommand command1 = new SqlCommand(asa, connection);
        SqlCommand RAW = new SqlCommand(insertQueryString, connection);

        command1.ExecuteNonQuery();
        RAW.ExecuteNonQuery();



    }
    public void DropTable(int num)
    {
        string DropTable = "DELETE FROM dbo.Stats WHERE Id =" + num;
        string databaseConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog =""C:\USERS\JARED\SOURCE\REPOS\BRAIN STEW\BRAIN STEW\DATABASE1.MDF""; Integrated Security = True ";


        SqlConnection connection = new SqlConnection(databaseConnectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(DropTable, connection);
        command.ExecuteNonQuery();
    }
    public void LoadDatabase(int num)
    {
        string query = "SELECT Username, Gender, Hair, HairColor, EyeColor, FacialHair, FacialExpression, TopClothing, Bottoms, Footwear, Headwear, Accessories, MeleeWeapon, RangedWeapon, Throwables, SpecialWeapon, Boosts, HealingItems, PetCompanion, Vehicle, Nightvision, NoiseSensitivity, Skillpoints, Strength, Speed, Stl, Defense FROM dbo.Stats WHERE Id =" + num;
        string databaseConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog =""C:\USERS\JARED\SOURCE\REPOS\BRAIN STEW\BRAIN STEW\DATABASE1.MDF""; Integrated Security = True ";

        using (SqlConnection connection = new SqlConnection(databaseConnectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader displayer = command.ExecuteReader();

            while (displayer.Read())
            {
                Console.WriteLine("Username: " + displayer["Username"]);
                Console.WriteLine("Gender: " + displayer["Gender"]);
                Console.WriteLine("Hair: " + displayer["Hair"]);
                Console.WriteLine("HairColor: " + displayer["HairColor"]);
                Console.WriteLine("EyeColor: " + displayer["EyeColor"]);
                Console.WriteLine("FacialHair: " + displayer["FacialHair"]);
                Console.WriteLine("FacialExpression: " + displayer["FacialExpression"]);
                Console.WriteLine("TopClothing: " + displayer["TopClothing"]);
                Console.WriteLine("Bottoms: " + displayer["Bottoms"]);
                Console.WriteLine("Footwear: " + displayer["Footwear"]);
                Console.WriteLine("Headwear: " + displayer["Headwear"]);
                Console.WriteLine("Accessories: " + displayer["Accessories"]);
                Console.WriteLine("MeleeWeapon: " + displayer["MeleeWeapon"]);
                Console.WriteLine("RangedWeapon: " + displayer["RangedWeapon"]);
                Console.WriteLine("Throwables: " + displayer["Throwables"]);
                Console.WriteLine("SpecialWeapon: " + displayer["SpecialWeapon"]);
                Console.WriteLine("Boosts: " + displayer["Boosts"]);
                Console.WriteLine("HealingItems: " + displayer["HealingItems"]);
                Console.WriteLine("PetCompanion: " + displayer["PetCompanion"]);
                Console.WriteLine("Vehicle: " + displayer["Vehicle"]);
                Console.WriteLine("Nightvision: " + displayer["Nightvision"]);
                Console.WriteLine("NoiseSensitivity: " + displayer["NoiseSensitivity"]);
                Console.WriteLine("Skillpoints: " + displayer["Skillpoints"]);
                Console.WriteLine("Strength: " + displayer["Strength"]);
                Console.WriteLine("Speed: " + displayer["Speed"]);
                Console.WriteLine("Stealth: " + displayer["Stl"]);
                Console.WriteLine("Defense: " + displayer["Defense"]);
                Console.WriteLine();
            }

            displayer.Close();
        }
    }
}


public class Character
{
    public bool CheckIfUsernameExists(string username)
    {
        string query = "SELECT COUNT(1) FROM [dbo].[Stats] WHERE [Username] = @Username";
        string databaseConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog =""C:\USERS\JARED\SOURCE\REPOS\BRAIN STEW\BRAIN STEW\DATABASE1.MDF""; Integrated Security = True ";

        using (SqlConnection connection = new SqlConnection(databaseConnectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Username", username);

            connection.Open();
            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }
    }//changes
    // Private fields
    private string username;
    private string gender;
    private string hair;
    private string hairColor;
    private string eyeColor;
    private string facialHair;
    private string facialExpression;
    private string top;
    private string bottoms;
    private string footwear;
    private string headwear;
    private string accessories;
    private string meleeWeapon;
    private string rangedWeapon;
    private string throwables;
    private string specialWeapon;
    private string boosts;
    private string healingItems;
    private string petCompanion;
    private string vehicle;
    private bool nightvision;
    private bool noiseSensitivity;
    private int skillpoints = 10;

    // Public properties with encapsulation using the 'this' keyword
    public string Username
    {
        get { return this.username; }
        set { this.username = value; }
    }

    public string Gender
    {
        get { return this.gender; }
        set { this.gender = value; }
    }

    public string Hair
    {
        get { return this.hair; }
        set { this.hair = value; }
    }

    public string HairColor
    {
        get { return this.hairColor; }
        set { this.hairColor = value; }
    }

    public string EyeColor
    {
        get { return this.eyeColor; }
        set { this.eyeColor = value; }
    }

    public string FacialHair
    {
        get { return this.facialHair; }
        set { this.facialHair = value; }
    }

    public string FacialExpression
    {
        get { return this.facialExpression; }
        set { this.facialExpression = value; }
    }

    public string Top
    {
        get { return this.top; }
        set { this.top = value; }
    }

    public string Bottoms
    {
        get { return this.bottoms; }
        set { this.bottoms = value; }
    }

    public string Footwear
    {
        get { return this.footwear; }
        set { this.footwear = value; }
    }

    public string Headwear
    {
        get { return this.headwear; }
        set { this.headwear = value; }
    }

    public string Accessories
    {
        get { return this.accessories; }
        set { this.accessories = value; }
    }

    public string MeleeWeapon
    {
        get { return this.meleeWeapon; }
        set { this.meleeWeapon = value; }
    }

    public string RangedWeapon
    {
        get { return this.rangedWeapon; }
        set { this.rangedWeapon = value; }
    }

    public string Throwables
    {
        get { return this.throwables; }
        set { this.throwables = value; }
    }

    public string SpecialWeapon
    {
        get { return this.specialWeapon; }
        set { this.specialWeapon = value; }
    }

    public string Boosts
    {
        get { return this.boosts; }
        set { this.boosts = value; }
    }

    public string HealingItems
    {
        get { return this.healingItems; }
        set { this.healingItems = value; }
    }

    public string PetCompanion
    {
        get { return this.petCompanion; }
        set { this.petCompanion = value; }
    }

    public string Vehicle
    {
        get { return this.vehicle; }
        set { this.vehicle = value; }
    }

    public bool Nightvision
    {
        get { return this.nightvision; }
        set { this.nightvision = value; }
    }

    public bool NoiseSensitivity
    {
        get { return this.noiseSensitivity; }
        set { this.noiseSensitivity = value; }
    }

    public int Skillpoints
    {
        get { return this.skillpoints; }
        set { this.skillpoints = value; }
    }
}


abstract class Power
{
    public abstract void Supe(bool nightvision, bool noiseSensitivity);

    public void Attributes()
    {
        Console.WriteLine("\nSpecial Abilities(s)");
    }
}
class Special_power : Power
{
    public override void Supe(bool nightvision, bool noiseSensitivity)
    {

        if (nightvision == true)
        {
            Console.WriteLine("\nNight Vision.");
        }

        if (noiseSensitivity == true)
        {
            Console.WriteLine("\nNoiseSensitivity.");
        }

        if (nightvision == false && noiseSensitivity == false)
        {
            Console.WriteLine("\nnone.");

        }
    }
}
public interface IStats
{

    int str { get; set; }
    int spd { get; set; }

}
public interface IStats2
{

    int stl { get; set; }
    int def { get; set; }
}
public struct Stats : IStats
{

    public int str { get; set; }
    public int spd { get; set; }

}


public struct Stats2 : IStats2
{


    public int stl { get; set; }
    public int def { get; set; }
}






class BrainStew
{
    /*
    class invalidinputException : Exception //i omitted this but just in case you wanna  use it its for special characteer
    {
        public invalidinputException(string message) : base(message) { }
    }
    public static void username_validate(string username)
    {
        string pattern = @"[!@#\$%\^\&\*\)\(+=._-]";
        Regex regex = new Regex(pattern);
        if (!regex.IsMatch(username))
        {
            throw new invalidinputException("invalid input");
        }

    }
    */


    public static void Main(string[] args)
    {
        Stats stat = new Stats();
        Stats2 stat2 = new Stats2();

        int str = stat.str;
        int spd = stat.spd;
        int stl = stat2.stl;
        int def = stat2.def;
        Special_power sp = new Special_power();

        SqlConnection DATABASE1;
        //string databaseConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Admin\source\repos\Brain Stew\Database1.mdf"";Integrated Security=True";

        string databaseConnectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog =""C:\USERS\JARED\SOURCE\REPOS\BRAIN STEW\BRAIN STEW\DATABASE1.MDF""; Integrated Security = True ";
        /*try
        {
            DATABASE1 = new SqlConnection(databaseConnectionString);
            DATABASE1.Open();
            Console.WriteLine("work cmon");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }*/
        while (true)
        {
            Console.Clear();
            Console.WriteLine("____________  ___  _____ _   _   _____ _____ _____ _    _ \r\n| ___ \\ ___ \\/ _ \\|_   _| \\ | | /  ___|_   _|  ___| |  | |\r\n| |_/ / |_/ / /_\\ \\ | | |  \\| | \\ `--.  | | | |__ | |  | |\r\n| ___ \\    /|  _  | | | | . ` |  `--. \\ | | |  __|| |/\\| |\r\n| |_/ / |\\ \\| | | |_| |_| |\\  | /\\__/ / | | | |___\\  /\\  /\r\n\\____/\\_| \\_\\_| |_/\\___/\\_| \\_/ \\____/  \\_/ \\____/ \\/  \\/ \r\n                                                          ");

            Console.WriteLine("\n[1] NEW GAME");
            Console.WriteLine("[2] LOAD GAME");
            Console.WriteLine("[3] CAMPAIGN MODE");
            Console.WriteLine("[4] CREDITS ");
            Console.WriteLine("[5] EXIT");
            try
            {
                int menu = Convert.ToInt32(Console.ReadLine());

                if (menu == 1) // Character Creation
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("-----Character creation-----");


                        // Variables to store choices
                        Character char1 = new Character();

                        string username = "";
                        string gender = "";
                        string hair = "";
                        string hairColor = "";
                        string eyeColor = "";
                        string facialHair = "";
                        string facialExpression = "";
                        string top = "";
                        string bottoms = "";
                        string footwear = "";
                        string headwear = "";
                        string accessories = "";
                        string meleeWeapon = "";
                        string rangedWeapon = "";
                        string throwables = "";
                        string specialWeapon = "";
                        string boosts = "";
                        string healingItems = "";
                        string petCompanion = "";
                        string vehicle = "";
                        bool nightvision = true;
                        bool noiseSensitivity = true;
                        int skillpoints = 10;
                        bool valid = true;







                        //Username
                        while (true)
                        {
                            Console.WriteLine("\nEnter Character Name: ");


                            username = Console.ReadLine();




                            if (username.Length == 0)
                            {
                                Console.WriteLine("Please enter a username.");
                                continue;
                            }

                            else if (username.Contains(" "))
                            {
                                Console.WriteLine("\nUsername cannot contain blank spaces.");
                                continue;
                            }

                            else if (username.Length > 12 || username.Length < 3)
                            {
                                Console.WriteLine("\nUsername must be 3-12 characters long.");
                                continue;
                            }
                            else if (char1.CheckIfUsernameExists(username))
                            {
                                Console.WriteLine("Taken na po siya...");
                                continue;
                            }//changes

                            break;


                        }

                        // Gender
                        while (true)
                        {
                            Console.WriteLine("\nChoose Gender");
                            Console.WriteLine("[1] Chill guy");
                            Console.WriteLine("[2] Girly pop");
                            try
                            {
                                int genderChoice = int.Parse(Console.ReadLine());
                                switch (genderChoice)
                                {
                                    case 1: gender = "Chill guy"; break;
                                    case 2: gender = "Girly pop"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;

                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Hair
                        while (true)
                        {
                            Console.WriteLine("\nChoose Hair");
                            Console.WriteLine("[1] Bald");
                            Console.WriteLine("[2] Mullet");
                            Console.WriteLine("[3] Redneck Mullet");
                            Console.WriteLine("[4] Man bun");
                            Console.WriteLine("[5] Lebowski hair");
                            Console.WriteLine("[6] Pineapple burst fade");
                            Console.WriteLine("[7] Afro");
                            Console.WriteLine("[8] Mohawk");
                            Console.WriteLine("[9] Pixie cut");
                            Console.WriteLine("[10] Wolf cut");
                            Console.WriteLine("[11] Braids");
                            Console.WriteLine("[12] Pony tail");
                            try
                            {
                                int hairChoice = int.Parse(Console.ReadLine());
                                switch (hairChoice)
                                {
                                    case 1: hair = "Bald"; break;
                                    case 2: hair = "Mullet"; break;
                                    case 3: hair = "Redneck Mullet"; break;
                                    case 4: hair = "Man bun"; break;
                                    case 5: hair = "Lebowski hair"; break;
                                    case 6: hair = "Pineapple burst fade"; break;
                                    case 7: hair = "Afro"; break;
                                    case 8: hair = "Mohawk"; break;
                                    case 9: hair = "Pixie cut"; break;
                                    case 10: hair = "Wolf cut"; break;
                                    case 11: hair = "Braids"; break;
                                    case 12: hair = "Pony tail"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Hair Color
                        while (true)
                        {
                            Console.WriteLine("\nChoose Hair Color");
                            Console.WriteLine("[1] Black");
                            Console.WriteLine("[2] Brown");
                            Console.WriteLine("[3] Blue");
                            Console.WriteLine("[4] Green");
                            Console.WriteLine("[5] Gray");
                            Console.WriteLine("[6] Red");
                            Console.WriteLine("[7] Purple");
                            Console.WriteLine("[8] Pink");
                            Console.WriteLine("[9] Yellow");
                            Console.WriteLine("[10] Orange");
                            Console.WriteLine("[11] White");
                            try
                            {
                                int hairColorChoice = int.Parse(Console.ReadLine());
                                switch (hairColorChoice)
                                {
                                    case 1: hairColor = "Black"; break;
                                    case 2: hairColor = "Brown"; break;
                                    case 3: hairColor = "Blue"; break;
                                    case 4: hairColor = "Green"; break;
                                    case 5: hairColor = "Gray"; break;
                                    case 6: hairColor = "Red"; break;
                                    case 7: hairColor = "Purple"; break;
                                    case 8: hairColor = "Pink"; break;
                                    case 9: hairColor = "Yellow"; break;
                                    case 10: hairColor = "Orange"; break;
                                    case 11: hairColor = "White"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Eye Color
                        while (true)
                        {
                            Console.WriteLine("\nChoose Eye Color");
                            Console.WriteLine("[1] Black");
                            Console.WriteLine("[2] Brown");
                            Console.WriteLine("[3] Blue");
                            Console.WriteLine("[4] Green");
                            Console.WriteLine("[5] Gray");
                            Console.WriteLine("[6] Red");
                            Console.WriteLine("[7] Purple");
                            Console.WriteLine("[8] Pink");
                            Console.WriteLine("[9] Yellow");
                            Console.WriteLine("[10] Orange");
                            Console.WriteLine("[11] White");
                            try
                            {
                                int eyeColorChoice = int.Parse(Console.ReadLine());
                                switch (eyeColorChoice)
                                {
                                    case 1: eyeColor = "Black"; break;
                                    case 2: eyeColor = "Brown"; break;
                                    case 3: eyeColor = "Blue"; break;
                                    case 4: eyeColor = "Green"; break;
                                    case 5: eyeColor = "Gray"; break;
                                    case 6: eyeColor = "Red"; break;
                                    case 7: eyeColor = "Purple"; break;
                                    case 8: eyeColor = "Pink"; break;
                                    case 9: eyeColor = "Yellow"; break;
                                    case 10: eyeColor = "Orange"; break;
                                    case 11: eyeColor = "White"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Facial Hair
                        while (true)
                        {
                            Console.WriteLine("\nChoose Facial Hair");
                            Console.WriteLine("[1] None");
                            Console.WriteLine("[2] Moustache");
                            Console.WriteLine("[3] Stubble");
                            Console.WriteLine("[4] Full Beard");
                            Console.WriteLine("[5] Wizard Beard");
                            Console.WriteLine("[6] Goatee");
                            Console.WriteLine("[7] Side Burns");
                            try
                            {
                                int facialHairChoice = int.Parse(Console.ReadLine());
                                switch (facialHairChoice)
                                {
                                    case 1: facialHair = "None"; break;
                                    case 2: facialHair = "Moustache"; break;
                                    case 3: facialHair = "Stubble"; break;
                                    case 4: facialHair = "Full Beard"; break;
                                    case 5: facialHair = "Wizard Beard"; break;
                                    case 6: facialHair = "Goatee"; break;
                                    case 7: facialHair = "Side Burns"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Facial Expression
                        while (true)
                        {
                            Console.WriteLine("\nChoose Facial Expression");
                            Console.WriteLine("[1] Nonchalant");
                            Console.WriteLine("[2] Smiling");
                            Console.WriteLine("[3] Smirking");
                            Console.WriteLine("[4] Angry");
                            Console.WriteLine("[5] Sad");
                            Console.WriteLine("[6] Roblox Man Face");
                            try
                            {
                                int facialExpressionChoice = int.Parse(Console.ReadLine());
                                switch (facialExpressionChoice)
                                {
                                    case 1: facialExpression = "Nonchalant"; break;
                                    case 2: facialExpression = "Smiling"; break;
                                    case 3: facialExpression = "Smirking"; break;
                                    case 4: facialExpression = "Angry"; break;
                                    case 5: facialExpression = "Sad"; break;
                                    case 6: facialExpression = "Roblox Man Face"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Top
                        while (true)
                        {
                            Console.WriteLine("\nChoose Top");
                            Console.WriteLine("[1] STI org shirt");
                            Console.WriteLine("[2] Wife beater shirt");
                            Console.WriteLine("[3] Sweaty Jacket");
                            Console.WriteLine("[4] Michael Jackson Thriller Jacket");
                            Console.WriteLine("[5] Tommy Shelby Overcoat");
                            Console.WriteLine("[6] Shirt found on the ground");
                            Console.WriteLine("[7] Barong tagalog");
                            Console.WriteLine("[8] Old Wedding Dress");
                            Console.WriteLine("[9] Lady Gaga Merch");
                            Console.WriteLine("[10] Apron");
                            Console.WriteLine("[11] 7/11 Uniform Top");
                            Console.WriteLine("[12] Nirvana T-shirt");
                            Console.WriteLine("[13] Sticky Sweater");
                            try
                            {
                                int topChoice = int.Parse(Console.ReadLine());
                                switch (topChoice)
                                {
                                    case 1: top = "STI org shirt"; break;
                                    case 2: top = "Wife beater shirt"; break;
                                    case 3: top = "Sweaty Jacket"; break;
                                    case 4: top = "Michael Jackson Thriller Jacket"; break;
                                    case 5: top = "Tommy Shelby Overcoat"; break;
                                    case 6: top = "Shirt found on the ground"; break;
                                    case 7: top = "Barong tagalog"; break;
                                    case 8: top = "Old Wedding Dress"; break;
                                    case 9: top = "Lady Gaga Merch"; break;
                                    case 10: top = "Apron"; break;
                                    case 11: top = "7/11 Uniform Top"; break;
                                    case 12: top = "Nirvana T-shirt"; break;
                                    case 13: top = "Sticky Sweater"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Bottoms
                        while (true)
                        {
                            Console.WriteLine("\nChoose Bottoms");
                            Console.WriteLine("[1] Pajamas");
                            Console.WriteLine("[2] Old Boxers");
                            Console.WriteLine("[3] Yoga Pants");
                            Console.WriteLine("[4] Ripped Jeans");
                            Console.WriteLine("[5] Elvis Presley Tassel Pants");
                            Console.WriteLine("[6] School Girl Skirt");
                            Console.WriteLine("[7] STI IT Pants 2xl");
                            Console.WriteLine("[8] Cargo Pants");
                            Console.WriteLine("[9] John Cena Jorts");
                            try
                            {
                                int bottomsChoice = int.Parse(Console.ReadLine());
                                switch (bottomsChoice)
                                {
                                    case 1: bottoms = "Pajamas"; break;
                                    case 2: bottoms = "Old Boxers"; break;
                                    case 3: bottoms = "Yoga Pants"; break;
                                    case 4: bottoms = "Ripped Jeans"; break;
                                    case 5: bottoms = "Elvis Presley Tassel Pants"; break;
                                    case 6: bottoms = "School Girl Skirt"; break;
                                    case 7: bottoms = "STI IT Pants 2xl"; break;
                                    case 8: bottoms = "Cargo Pants"; break;
                                    case 9: bottoms = "John Cena Jorts"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Footwear
                        while (true)
                        {
                            Console.WriteLine("\nChoose Footwear");
                            Console.WriteLine("[1] Black Shoes ni Guab");
                            Console.WriteLine("[2] Biker Boots");
                            Console.WriteLine("[3] Loafers");
                            Console.WriteLine("[4] Roller Skates");
                            Console.WriteLine("[5] Tsinelas");
                            Console.WriteLine("[6] Crocs");
                            Console.WriteLine("[7] Converse (emo)");
                            Console.WriteLine("[8] Unicorn Slippers");
                            try
                            {
                                int footwearChoice = int.Parse(Console.ReadLine());
                                switch (footwearChoice)
                                {
                                    case 1: footwear = "Black Shoes ni Guab"; break;
                                    case 2: footwear = "Biker Boots"; break;
                                    case 3: footwear = "Loafers"; break;
                                    case 4: footwear = "Roller Skates"; break;
                                    case 5: footwear = "Tsinelas"; break;
                                    case 6: footwear = "Crocs"; break;
                                    case 7: footwear = "Converse (emo)"; break;
                                    case 8: footwear = "Unicorn Slippers"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Headwear
                        while (true)
                        {
                            Console.WriteLine("\nChoose Headwear");
                            Console.WriteLine("[1] None");
                            Console.WriteLine("[2] Fedora");
                            Console.WriteLine("[3] Balaclava");
                            Console.WriteLine("[4] Cowboy Hat");
                            Console.WriteLine("[5] Dirty Beanie");
                            Console.WriteLine("[6] Football Helmet");
                            Console.WriteLine("[7] Fez");
                            Console.WriteLine("[8] McDonalds Paper Bag");
                            Console.WriteLine("[9] Hard Hat");
                            Console.WriteLine("[10] Santa Hat");
                            try
                            {
                                int headwearChoice = int.Parse(Console.ReadLine());
                                switch (headwearChoice)
                                {
                                    case 1: headwear = "None"; break;
                                    case 2: headwear = "Fedora"; break;
                                    case 3: headwear = "Balaclava"; break;
                                    case 4: headwear = "Cowboy Hat"; break;
                                    case 5: headwear = "Dirty Beanie"; break;
                                    case 6: headwear = "Football Helmet"; break;
                                    case 7: headwear = "Fez"; break;
                                    case 8: headwear = "McDonalds Paper Bag"; break;
                                    case 9: headwear = "Hard Hat"; break;
                                    case 10: headwear = "Santa Hat"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Accessories
                        while (true)
                        {
                            Console.WriteLine("\nChoose Accessories");
                            Console.WriteLine("[1] None");
                            Console.WriteLine("[2] Scarf");
                            Console.WriteLine("[3] Gold Chain");
                            Console.WriteLine("[4] Headphones");
                            Console.WriteLine("[5] Sunglasses");
                            try
                            {
                                int accessoriesChoice = int.Parse(Console.ReadLine());
                                switch (accessoriesChoice)
                                {
                                    case 1: accessories = "None"; break;
                                    case 2: accessories = "Scarf"; break;
                                    case 3: accessories = "Gold Chain"; break;
                                    case 4: accessories = "Headphones"; break;
                                    case 5: accessories = "Sunglasses"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Melee Weapon
                        while (true)
                        {
                            Console.WriteLine("\nChoose Melee Weapon");
                            Console.WriteLine("[1] Old Frying Pan");
                            Console.WriteLine("[2] 10kg Dumbbells");
                            Console.WriteLine("[3] Fire Extinguisher (Empty)");
                            Console.WriteLine("[4] Baseball Bat with Nails");
                            Console.WriteLine("[5] Stop Sign");
                            Console.WriteLine("[6] Dead Person’s Spine");
                            Console.WriteLine("[7] A4tech Keyboard");
                            Console.WriteLine("[8] Steel Chair");
                            Console.WriteLine("[9] Rusty Katana");
                            Console.WriteLine("[10] Whiskey Bottle (Empty)");
                            Console.WriteLine("[11] Plunger");
                            try
                            {
                                int meleeWeaponChoice = int.Parse(Console.ReadLine());
                                switch (meleeWeaponChoice)
                                {
                                    case 1: meleeWeapon = "Old Frying Pan"; break;
                                    case 2: meleeWeapon = "10kg Dumbbells"; break;
                                    case 3: meleeWeapon = "Fire Extinguisher (Empty)"; break;
                                    case 4: meleeWeapon = "Baseball Bat with Nails"; break;
                                    case 5: meleeWeapon = "Stop Sign"; break;
                                    case 6: meleeWeapon = "Dead Person’s Spine"; break;
                                    case 7: meleeWeapon = "A4tech Keyboard"; break;
                                    case 8: meleeWeapon = "Steel Chair"; break;
                                    case 9: meleeWeapon = "Rusty Katana"; break;
                                    case 10: meleeWeapon = "Whiskey Bottle (Empty)"; break;
                                    case 11: meleeWeapon = "Plunger"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;

                            }
                            break;
                        }

                        // Ranged Weapon
                        while (true)
                        {
                            Console.WriteLine("\nChoose Ranged Weapon");
                            Console.WriteLine("[1] Nail Gun");
                            Console.WriteLine("[2] Paintball Gun");
                            Console.WriteLine("[3] Tennis Ball Launcher");
                            Console.WriteLine("[4] Smelly Harpoon");
                            Console.WriteLine("[5] Slingshot");
                            Console.WriteLine("[6] Bow and Arrows");
                            Console.WriteLine("[7] Fireworks");
                            try
                            {
                                int rangedWeaponChoice = int.Parse(Console.ReadLine());
                                switch (rangedWeaponChoice)
                                {
                                    case 1: rangedWeapon = "Nail Gun"; break;
                                    case 2: rangedWeapon = "Paintball Gun"; break;
                                    case 3: rangedWeapon = "Tennis Ball Launcher"; break;
                                    case 4: rangedWeapon = "Smelly Harpoon"; break;
                                    case 5: rangedWeapon = "Slingshot"; break;
                                    case 6: rangedWeapon = "Bow and Arrows"; break;
                                    case 7: rangedWeapon = "Fireworks"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!"); continue;
                            }
                            break;
                        }

                        // Throwables
                        while (true)
                        {
                            Console.WriteLine("\nChoose Throwables");
                            Console.WriteLine("[1] Bricks");
                            Console.WriteLine("[2] Nokia Cellphone");
                            Console.WriteLine("[3] Molotov");
                            Console.WriteLine("[4] Cheese");
                            Console.WriteLine("[5] Cat");
                            Console.WriteLine("[6] Plant Pot");
                            Console.WriteLine("[7] Plates");
                            try
                            {
                                int throwablesChoice = int.Parse(Console.ReadLine());
                                switch (throwablesChoice)
                                {
                                    case 1: throwables = "Bricks"; break;
                                    case 2: throwables = "Nokia Cellphone"; break;
                                    case 3: throwables = "Molotov"; break;
                                    case 4: throwables = "Cheese"; break;
                                    case 5: throwables = "Cat"; break;
                                    case 6: throwables = "Plant Pot"; break;
                                    case 7: throwables = "Plates"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex) { Console.WriteLine("\nInvalid Input!"); continue; }
                            break;
                        }

                        // Special Weapons
                        while (true)
                        {
                            Console.WriteLine("\nChoose Special Weapons");
                            Console.WriteLine("[1] Chainsaw from supermarket");
                            Console.WriteLine("[2] Flamethrower");
                            Console.WriteLine("[3] Firetruck Water Hose");
                            Console.WriteLine("[4] The Last Working Pistol");
                            try
                            {
                                int specialWeaponChoice = int.Parse(Console.ReadLine());
                                switch (specialWeaponChoice)
                                {
                                    case 1: specialWeapon = "Chainsaw from supermarket"; break;
                                    case 2: specialWeapon = "Flamethrower"; break;
                                    case 3: specialWeapon = "Firetruck Water Hose"; break;
                                    case 4: specialWeapon = "The Last Working Pistol"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!"); continue;
                            }
                            break;
                        }

                        // Extra Items
                        while (true)
                        {
                            Console.WriteLine("\nDo you want to have night vision? (Y/N)");
                            Console.WriteLine("Pro: Can easily navigate maps with low light environment");
                            Console.WriteLine("Con: Sensitive on daylight environment maps");
                            Console.WriteLine("[1] Yes");
                            Console.WriteLine("[2] No");

                            try
                            {
                                int YN = Convert.ToInt32(Console.ReadLine());
                                switch (YN)
                                {
                                    case 1: nightvision = true; break;
                                    case 2: nightvision = false; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;

                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!"); continue;
                            }
                            Console.WriteLine("\nDo you want to have Noise Sensitivity? (Y/N)");
                            Console.WriteLine("Pro: Heightened stealth helps in ambush attacks or setting up traps");
                            Console.WriteLine("Con: Mob attacks may cause panic during battle");
                            Console.WriteLine("[1] Yes");
                            Console.WriteLine("[2] No");

                            try
                            {
                                int Yn = Convert.ToInt32(Console.ReadLine());
                                switch (Yn)
                                {
                                    case 1: noiseSensitivity = true; break;
                                    case 2: noiseSensitivity = false; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex) { Console.WriteLine("\nInvalid Input!"); continue; }
                            break;
                        }

                        // Boosts
                        while (true)
                        {
                            Console.WriteLine("\nChoose Boosts");
                            Console.WriteLine("[1] Monster Energy (Gives character an instant energy surge)");
                            Console.WriteLine("[2] Old Love Letter from Crush (Increasing courage and charisma)");
                            Console.WriteLine("[3] Old Family Picture (Providing mental resilience)");
                            Console.WriteLine("[4] Sunscreen (Increase resistance to extreme heat)");
                            Console.WriteLine("[5] Bulletproof Vest (Extra defense)");
                            Console.WriteLine("[6] Knight Armor from Museum (Immune to zombie bites)");

                            try
                            {
                                int boostsChoice = int.Parse(Console.ReadLine());
                                switch (boostsChoice)
                                {
                                    case 1: boosts = "Monster Energy"; break;
                                    case 2: boosts = "Old Love Letter from Crush"; break;
                                    case 3: boosts = "Old Family Picture"; break;
                                    case 4: boosts = "Sunscreen"; break;
                                    case 5: boosts = "Bulletproof Vest"; break;
                                    case 6: boosts = "Knight Armor from Museum"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!"); continue;
                            }
                            break;
                        }

                        // Healing Items
                        while (true)
                        {
                            Console.WriteLine("\nChoose Healing Items");
                            Console.WriteLine("[1] Med kit (quickly stabilize your health)");
                            Console.WriteLine("[2] Sleep it off (Restores mental clarity and regenerates health at slower pace)");
                            Console.WriteLine("[3] Vicks Vapor rub (soothe muscle soreness)");
                            Console.WriteLine("[4] Quack Doctor Herbs (random herbs from the wild)");
                            Console.WriteLine("[5] Dirty Band Aid (stops bleeding and cover up cuts)");
                            Console.WriteLine("[6] Ramen Noodles (provides nutrition and energy)");
                            try
                            {
                                int healingItemsChoice = int.Parse(Console.ReadLine());
                                switch (healingItemsChoice)
                                {
                                    case 1: healingItems = "Med kit"; break;
                                    case 2: healingItems = "Sleep it off"; break;
                                    case 3: healingItems = "Vicks Vapor rub"; break;
                                    case 4: healingItems = "Quack Doctor Herbs"; break;
                                    case 5: healingItems = "Dirty Band Aid"; break;
                                    case 6: healingItems = "Ramen Noodles"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Pet Companion
                        while (true)
                        {
                            Console.WriteLine("\nChoose Pet Companion");
                            Console.WriteLine("[1] Cat");
                            Console.WriteLine("[2] Dog");
                            Console.WriteLine("[3] Cursing parrot");
                            Console.WriteLine("[4] Goldfish in a paper cup");
                            try
                            {
                                int petCompanionChoice = int.Parse(Console.ReadLine());
                                switch (petCompanionChoice)
                                {
                                    case 1: petCompanion = "Cat"; break;
                                    case 2: petCompanion = "Dog"; break;
                                    case 3: petCompanion = "Cursing parrot"; break;
                                    case 4: petCompanion = "Goldfish in a paper cup"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        // Vehicle
                        while (true)
                        {
                            Console.WriteLine("\nChoose Vehicle");
                            Console.WriteLine("[1] School Bus (It can carry lots of passengers)");
                            Console.WriteLine("[2] Old Truck (It can haul heavy loads)");
                            Console.WriteLine("[3] Armored Car from Bank (it offers top tier protection from zombies)");
                            Console.WriteLine("[4] Pedicab (Not the best vehicle but it can be used when you need to be stealthy)");
                            Console.WriteLine("[5] Horse (Doesn’t rely on fuel or batteries and it can travel across various terrains)");
                            Console.WriteLine("[6] Wheelchair with rocket boosters");
                            try
                            {
                                int vehicleChoice = int.Parse(Console.ReadLine());
                                switch (vehicleChoice)
                                {
                                    case 1: vehicle = "School Bus"; break;
                                    case 2: vehicle = "Old Truck"; break;
                                    case 3: vehicle = "Armored Car from Bank"; break;
                                    case 4: vehicle = "Pedicab"; break;
                                    case 5: vehicle = "Horse"; break;
                                    case 6: vehicle = "Wheelchair with rocket boosters"; break;
                                    default: Console.WriteLine("\nInvalid Input!"); continue;
                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine("\nInvalid Input!");
                                continue;
                            }
                            break;
                        }

                        while (true)
                        {
                            while (valid == true)
                            {
                                str = 0;
                                spd = 0;
                                stl = 0;
                                def = 0;
                                Console.WriteLine("\n-----Character Attributes-----");
                                Console.WriteLine("\nStrength: " + str);
                                Console.WriteLine("Speed: " + spd);
                                Console.WriteLine("Stealth: " + stl);
                                Console.WriteLine("Defense: " + def);

                                Console.WriteLine("\nSkillpoints (" + skillpoints + ")");
                                Console.WriteLine("Enter Strength(0-3): ");
                                try
                                {
                                    str = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                    break;
                                }

                                if (str > skillpoints)
                                {
                                    Console.WriteLine("\nInsufficient skillpoints");
                                    str = 0;
                                    break;
                                }

                                else if (str > 3 || str < 0)
                                {
                                    Console.WriteLine("\nInvalid input! Please input 0-3 only!");
                                    str = 0;
                                    break;
                                }

                                else
                                {
                                    skillpoints = skillpoints - str;
                                }
                                try
                                {
                                    Console.WriteLine("\nSkillpoints (" + skillpoints + ")");
                                    Console.WriteLine("Enter Speed(0-3): ");
                                    spd = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                    break;
                                }
                                if (spd > skillpoints)
                                {
                                    Console.WriteLine("\nInsufficient skillpoints");
                                    spd = 0;
                                    break;
                                }

                                else if (spd > 3 || spd < 0)
                                {
                                    Console.WriteLine("\nInvalid input! Please input 0-3 only!");
                                    spd = 0;
                                    break;
                                }

                                else
                                {
                                    skillpoints = skillpoints - spd;
                                }
                                try
                                {
                                    Console.WriteLine("\nSkillpoints (" + skillpoints + ")");
                                    Console.WriteLine("Enter Stealth(0-3): ");
                                    stl = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                    break;
                                }
                                if (stl > skillpoints)
                                {
                                    Console.WriteLine("\nInsufficient skillpoints");
                                    stl = 0;
                                    break;
                                }

                                else if (stl > 3 || stl < 0)
                                {
                                    Console.WriteLine("\nInvalid input! Please input 0-3 only!");
                                    stl = 0;
                                    break;
                                }

                                else
                                {
                                    skillpoints = skillpoints - stl;
                                }
                                try
                                {
                                    Console.WriteLine("\nSkillpoints (" + skillpoints + ")");
                                    Console.WriteLine("Enter Defence(0-3): ");
                                    def = Convert.ToInt32(Console.ReadLine());
                                }
                                catch (FormatException ex)
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                    break;
                                }
                                if (def > skillpoints)
                                {
                                    Console.WriteLine("\nInsufficient skillpoints");
                                    def = 0;
                                    break;
                                }

                                else if (def > 3 || def < 0)
                                {
                                    Console.WriteLine("\nInvalid input! Please input 0-3 only!");
                                    def = 0;
                                    break;
                                }

                                else
                                {
                                    skillpoints = skillpoints - def;
                                }

                                break;
                            }
                            if (skillpoints != 0)
                            {

                                Console.WriteLine("\nDo you wish to reset skillpoints and restart? (Y/N)");
                                Console.WriteLine("You have " + skillpoints + " skillpoints left. (Unused skillpoints can still be be used at another time.)");
                                string yn = Console.ReadLine();

                                if (yn.ToUpper() == "Y")
                                {
                                    skillpoints = 10;
                                    valid = true;
                                    continue;
                                }

                                else if (yn.ToUpper() == "N")
                                {
                                    break;
                                }

                                else
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                    valid = false;
                                }

                            }

                            else
                            {
                                Console.WriteLine("\nDo you wish to reset skillpoints and restart? (Y/N)");
                                string yn = Console.ReadLine();

                                if (yn.ToUpper() == "Y")
                                {
                                    skillpoints = 10;
                                    valid = true;
                                    continue;
                                }

                                else if (yn.ToUpper() == "N")
                                {
                                    break;
                                }

                                else
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                    valid = false;
                                }
                            }
                        }
                        // Set the values into the char1 attributes
                        char1.Username = username;
                        char1.Gender = gender;
                        char1.Hair = hair;
                        char1.HairColor = hairColor;
                        char1.EyeColor = eyeColor;
                        char1.FacialHair = facialHair;
                        char1.FacialExpression = facialExpression;
                        char1.Top = top;
                        char1.Bottoms = bottoms;
                        char1.Footwear = footwear;
                        char1.Headwear = headwear;
                        char1.Accessories = accessories;
                        char1.MeleeWeapon = meleeWeapon;
                        char1.RangedWeapon = rangedWeapon;
                        char1.Throwables = throwables;
                        char1.SpecialWeapon = specialWeapon;
                        char1.Boosts = boosts;
                        char1.HealingItems = healingItems;
                        char1.PetCompanion = petCompanion;
                        char1.Vehicle = vehicle;
                        char1.Nightvision = nightvision;
                        char1.NoiseSensitivity = noiseSensitivity;
                        char1.Skillpoints = skillpoints;


                        stat.str = str;
                        stat.spd = spd;
                        stat2.stl = stl;
                        stat2.def = def;

                        Console.WriteLine("\nCharacter creation complete.");
                        Console.WriteLine(" ");

                        // Print the selected choices

                        Console.WriteLine("\n-----Character Details-----");
                        Console.WriteLine($"Username: {char1.Username} ");
                        Console.WriteLine($"Gender: {gender}");
                        Console.WriteLine($"Hair: {hair}");
                        Console.WriteLine($"Hair Color: {hairColor}");
                        Console.WriteLine($"Eye Color: {eyeColor}");
                        Console.WriteLine($"Facial Hair: {facialHair}");
                        Console.WriteLine($"Facial Expression: {facialExpression}");
                        Console.WriteLine($"Top: {top}");
                        Console.WriteLine($"Bottoms: {bottoms}");
                        Console.WriteLine($"Footwear: {footwear}");
                        Console.WriteLine($"Headwear: {headwear}");
                        Console.WriteLine($"Accessories: {accessories}");

                        Console.WriteLine("\n---Weapons---");
                        Console.WriteLine($"Melee Weapon: {meleeWeapon}");
                        Console.WriteLine($"Ranged Weapon: {rangedWeapon}");
                        Console.WriteLine($"Throwables: {throwables}");
                        Console.WriteLine($"Special Weapon: {specialWeapon}");

                        Console.WriteLine("\n---Character Utilities---");
                        Console.WriteLine($"Boosts: {boosts}");
                        Console.WriteLine($"Healing Items: {healingItems}");
                        Console.WriteLine($"Pet Companion: {petCompanion}");
                        Console.WriteLine($"Vehicle: {vehicle}");

                        sp.Attributes();
                        sp.Supe(nightvision, noiseSensitivity);

                        Console.WriteLine("\n-----Character Stats-----");
                        Console.WriteLine("Strength: " + str);
                        Console.WriteLine("Speed: " + spd);
                        Console.WriteLine("Stealth: " + stl);
                        Console.WriteLine("Defense: " + def);
                        // save character

                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("\nDo you want to save this character? (Y/N)");
                                string save = Console.ReadLine();


                                if (save.ToUpper() == "Y")
                                {
                                    while (true)
                                    {
                                        Console.WriteLine("\nWhich save file do you want it to save in?");
                                        Console.WriteLine("[1] SAVE FILE");
                                        Console.WriteLine("[2] SAVE FILE");
                                        Console.WriteLine("[3] SAVE FILE");
                                        Console.WriteLine("[4] SAVE FILE");
                                        Console.WriteLine("[5] SAVE FILE");
                                        try
                                        {
                                            int saveFile = int.Parse(Console.ReadLine());




                                            switch (saveFile)
                                            {
                                                case 1:
                                                    ISave fileStorage = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt");
                                                    //checks file if empty
                                                    string filePath1 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt";
                                                    FileInfo fileInfo = new FileInfo(filePath1);
                                                    int num = 1;
                                                    if (fileInfo.Length > 0)
                                                    {
                                                        Console.WriteLine("\nthis file is not empty. \n do you wish to overwrite this file? Y|N ");
                                                        string s = Console.ReadLine();
                                                        if (s.ToUpper() == "Y")
                                                        {
                                                            fileStorage.DropTable(num);//Changes
                                                            fileStorage.SaveFile(char1);
                                                            fileStorage.SaveFile(stat, stat2);
                                                            fileStorage.SaveFileQuery(char1, stat, stat2, num);//Changes instead of table u only use num now
                                                            Console.WriteLine("\nCharacter Overwritten");
                                                        }
                                                        else if (s.ToUpper() == "N")
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Save using file storage
                                                        fileStorage.SaveFile(char1);
                                                        fileStorage.SaveFile(stat, stat2);
                                                        fileStorage.SaveFileQuery(char1, stat, stat2, num);
                                                        Console.WriteLine("\nCharacter Saved");

                                                    }
                                                    break;
                                                case 2:
                                                    ISave fileStorage2 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile2.txt");
                                                    //checks file if empty
                                                    string table2 = "Stats2";
                                                    string filePath2 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile2.txt";
                                                    FileInfo fileInfo2 = new FileInfo(filePath2);
                                                    int num2 = 2;
                                                    if (fileInfo2.Length > 0)
                                                    {

                                                        Console.WriteLine("\nthis file is not empty. \n do you wish to overwrite this file? Y|N ");
                                                        string s = Console.ReadLine();
                                                        if (s.ToUpper() == "Y")
                                                        {
                                                            fileStorage2.DropTable(num2);//Changes
                                                            fileStorage2.SaveFile(char1);
                                                            fileStorage2.SaveFile(stat, stat2);
                                                            fileStorage2.SaveFileQuery(char1, stat, stat2, num2);//Changes instead of table u only use num now
                                                            Console.WriteLine("\nCharacter Overwritten");
                                                        }
                                                        else if (s.ToUpper() == "N")
                                                        {
                                                            continue;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        // Save using file storage
                                                        fileStorage2.SaveFile(char1);
                                                        fileStorage2.SaveFile(stat, stat2);
                                                        fileStorage2.SaveFileQuery(char1, stat, stat2, num2);
                                                        Console.WriteLine("\nCharacter Saved");

                                                    }
                                                    break;
                                                case 3:
                                                    ISave fileStorage3 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile3.txt");
                                                    //checks file if empty
                                                    string filePath3 = "C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveSaveFile3.txt";
                                                    FileInfo fileInfo3 = new FileInfo(filePath3);
                                                    int num3 = 3;
                                                    if (fileInfo3.Length > 0)
                                                    {

                                                        Console.WriteLine("\nthis file is not empty. \n do you wish to overwrite this file? Y|N ");
                                                        string s = Console.ReadLine();
                                                        if (s.ToUpper() == "Y")
                                                        {
                                                            fileStorage3.SaveFile(char1);
                                                            fileStorage3.SaveFile(stat, stat2);
                                                            fileStorage3.SaveFileQuery(char1, stat, stat2, num3);
                                                            Console.WriteLine("\nCharacter Overwritten");
                                                        }
                                                        else if (s.ToUpper() == "N")
                                                        {
                                                            continue;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        // Save using file storage
                                                        fileStorage3.SaveFile(char1);
                                                        fileStorage3.SaveFile(stat, stat2);
                                                        fileStorage3.SaveFileQuery(char1, stat, stat2, num3);
                                                        Console.WriteLine("\nCharacter Saved");

                                                    }
                                                    break;
                                                case 4:
                                                    ISave fileStorage4 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile4.txt");
                                                    //checks file if empty
                                                    string filePath4 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile4.txt";
                                                    FileInfo fileInfo4 = new FileInfo(filePath4);
                                                    int num4 = 4;
                                                    if (fileInfo4.Length > 0)
                                                    {

                                                        Console.WriteLine("\nthis file is not empty. \n do you wish to overwrite this file? Y|N ");
                                                        string s = Console.ReadLine();
                                                        if (s.ToUpper() == "Y")
                                                        {
                                                            fileStorage4.DropTable(num4);
                                                            fileStorage4.SaveFile(char1);
                                                            fileStorage4.SaveFile(stat, stat2);
                                                            fileStorage4.SaveFileQuery(char1, stat, stat2, num4);
                                                            Console.WriteLine("\nCharacter Overwritten");
                                                        }
                                                        else if (s.ToUpper() == "N")
                                                        {
                                                            continue;
                                                        }

                                                    }
                                                    else
                                                    {
                                                        // Save using file storage
                                                        fileStorage4.SaveFile(char1);
                                                        fileStorage4.SaveFile(stat, stat2);
                                                        fileStorage4.SaveFileQuery(char1, stat, stat2, num4);
                                                        Console.WriteLine("\nCharacter Saved");

                                                    }
                                                    break;
                                                case 5:
                                                    ISave fileStorage5 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile5.txt");
                                                    //checks file if empty
                                                    string filePath5 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile5.txt";
                                                    FileInfo fileInfo5 = new FileInfo(filePath5);
                                                    int num5 = 5;
                                                    if (fileInfo5.Length > 0)
                                                    {

                                                        Console.WriteLine("\nthis file is not empty. \n do you wish to overwrite this file? Y|N ");
                                                        string s = Console.ReadLine();
                                                        if (s.ToUpper() == "Y")
                                                        {
                                                            fileStorage5.DropTable(num5);
                                                            fileStorage5.SaveFile(char1);
                                                            fileStorage5.SaveFile(stat, stat2);
                                                            fileStorage5.SaveFileQuery(char1, stat, stat2, num5);
                                                            Console.WriteLine("\nCharacter Overwritten");
                                                        }
                                                        else if (s.ToUpper() == "N")
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // Save using file storage
                                                        fileStorage5.SaveFile(char1);
                                                        fileStorage5.SaveFile(stat, stat2);
                                                        fileStorage5.SaveFileQuery(char1, stat, stat2, num5);
                                                        Console.WriteLine("\nCharacter Saved");

                                                    }
                                                    break;


                                                default: Console.WriteLine("\nInvalid Input!"); continue;
                                            }
                                        }
                                        catch (FormatException ex)
                                        {
                                            Console.WriteLine("\nInvalid Input!");
                                            continue;
                                        }
                                        break;
                                    }
                                    break;
                                }

                                else if (save.ToUpper() == "N")
                                {
                                    break;
                                }

                                else
                                {
                                    Console.WriteLine("\nInvalid Input!");
                                }
                            }

                            catch (FormatException ex)
                            {
                                Console.Clear();
                                Console.WriteLine("\nInvalid Input!");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("\nEnter any key to return to Menu");
                                Console.WriteLine("Enter X to exit");
                                string a = Console.ReadLine();
                                if (a.ToUpper() == "X")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Bye Bye ~ ");
                                    break;
                                }

                                else
                                {
                                    Console.Clear();
                                    continue;
                                }
                            }

                        }
                        break;
                    }

                }




                else if (menu == 2) // Load game
                {
                    Console.Clear();
                    Console.WriteLine("[1] VIEW ALL CHARACTERS");
                    Console.WriteLine("[2] VIEW A SPECIFIC FILE");
                    Console.WriteLine("[3] DELETE A FILE");
                    Console.WriteLine("[4] GO BACK TO THE MAIN MENU");
                    int load = Convert.ToInt32(Console.ReadLine());
                    switch (load)
                    {
                        case 1:

                            ISave FileStorage1 = new SaveFiles("C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveFile1.txt");
                            for (int i = 1; i <= 5; i++)
                            {
                                FileStorage1.LoadDatabase(i);
                                continue;
                            }
                            break;


                        case 2:
                            Console.Clear();
                            Console.WriteLine("[1] SAVE FILE");
                            Console.WriteLine("[2] SAVE FILE");
                            Console.WriteLine("[3] SAVE FILE");
                            Console.WriteLine("[4] SAVE FILE");
                            Console.WriteLine("[5] SAVE FILE");
                            int save = Convert.ToInt32(Console.ReadLine());

                            try
                            {
                                switch (save)
                                {
                                    case 1:
                                        Console.Clear();
                                        // Specify the path to your text file
                                        string filePath1 = "C:\\Users\\Admin\\source\\repos\\Brain Stew\\Save\\SaveFile1.txt";

                                        ISave FileStorage = new SaveFiles("C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveFile1.txt");
                                        int num = 1;
                                        FileStorage.LoadDatabase(num);//Changes

                                        // Create an instance of the FileHandler class
                                        // FileHandler fileHandler1 = new FileHandler(filePath1);

                                        // Check if the file is empty and read its contents if it's not
                                        //fileHandler1.CheckAndReadFile_load();
                                        break;

                                    case 2:
                                        Console.Clear();
                                        // Specify the path to your text file
                                        string filePath2 = "C:\\Users\\Admin\\source\\repos\\Brain Stew\\Save\\SaveFile2.txt";

                                        ISave FileStorage2 = new SaveFiles("C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveFile1.txt");
                                        int num2 = 2;
                                        FileStorage2.LoadDatabase(num2);//Changes


                                        break;

                                    case 3:
                                        Console.Clear();
                                        // Specify the path to your text file
                                        string filePath3 = "C:\\Users\\Admin\\source\\repos\\Brain Stew\\Save\\SaveFile3.txt";

                                        ISave FileStorage3 = new SaveFiles("C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveFile1.txt");
                                        int num3 = 3;
                                        FileStorage3.LoadDatabase(num3);//Changes

                                        break;

                                    case 4:
                                        Console.Clear();
                                        // Specify the path to your text file
                                        string filePath4 = "C:\\Users\\Admin\\source\\repos\\Brain Stew\\Save\\SaveFile4.txt";

                                        ISave FileStorage4 = new SaveFiles("C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveFile1.txt");
                                        int num4 = 4;
                                        FileStorage4.LoadDatabase(num4);//Changes

                                        break;

                                    case 5:
                                        Console.Clear();
                                        // Specify the path to your text file
                                        string filePath5 = "C:\\Users\\Admin\\source\\repos\\Brain Stew\\Save\\SaveFile5.txt";

                                        ISave FileStorage5 = new SaveFiles("C:\\Users\\Admin\\source\\repos\\Brain Stew\\SaveFile1.txt");
                                        int num5 = 5;
                                        FileStorage5.LoadDatabase(num5);


                                        break;
                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input!");
                                        break;
                                }
                            }


                            catch (FormatException ex)
                            {
                                Console.Clear();
                                Console.WriteLine("\nInvalid Input!");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("\nEnter any key to return to Menu");
                                Console.WriteLine("Enter X to exit");
                                string a = Console.ReadLine();
                                if (a.ToUpper() == "X")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Bye Bye ~ ");
                                    break;
                                }

                                else
                                {
                                    Console.Clear();
                                    continue;
                                }
                            }
                            break;
                        case 3:

                            Console.Clear();
                            Console.WriteLine("Select which file do you want to DELETE");
                            Console.WriteLine("[1] SAVE FILE");
                            Console.WriteLine("[2] SAVE FILE");
                            Console.WriteLine("[3] SAVE FILE");
                            Console.WriteLine("[4] SAVE FILE");
                            Console.WriteLine("[5] SAVE FILE");
                            int del = int.Parse(Console.ReadLine());
                            Console.WriteLine("Are you sure?");
                            Console.WriteLine("[1] YES");
                            Console.WriteLine("[2] NO");
                            int u = int.Parse(Console.ReadLine());
                            try
                            {
                                switch (del)
                                {
                                    case 1:

                                        if (u == 1)
                                        {
                                            ISave fileStorage = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt");
                                            int num11 = 1;
                                            string fileKILL = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt";
                                            fileStorage.EmptyFile(fileKILL);
                                            fileStorage.DropTable(num11);

                                            Console.WriteLine("\n SaveFile 1 deleted!");
                                        }
                                        else if (u == 2)
                                        {
                                            break;
                                        }

                                        break;
                                    case 2:


                                        if (u == 1)
                                        {
                                            ISave fileStorage2 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt");
                                            //checks file if empty
                                            string fileKILL2 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile2.txt";
                                            int num22 = 2;
                                            fileStorage2.EmptyFile(fileKILL2);
                                            fileStorage2.DropTable(num22);

                                            Console.WriteLine("\n SaveFile 2 deleted!");
                                        }
                                        else if (u == 2)
                                        {
                                            break;
                                        }

                                        break;
                                    case 3:

                                        ISave fileStorage3 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt");

                                        if (u == 1)
                                        {
                                            int num33 = 3;
                                            string fileKILL3 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile3.txt";
                                            fileStorage3.EmptyFile(fileKILL3);
                                            fileStorage3.DropTable(num33);

                                            Console.WriteLine("\n SaveFile 3 deleted!");
                                        }
                                        else if (u == 2)
                                        {
                                            break;
                                        }

                                        break;
                                    case 4:

                                        ISave fileStorage4 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt");

                                        if (u == 1)
                                        {
                                            int num44 = 4;

                                            string fileKILL4 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile4.txt";
                                            fileStorage4.EmptyFile(fileKILL4);
                                            fileStorage4.DropTable(num44);

                                            Console.WriteLine("\n SaveFile 4 deleted!");
                                        }
                                        else if (u == 2)
                                        {
                                            break;
                                        }

                                        break;
                                    case 5:

                                        ISave fileStorage5 = new SaveFiles("C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile1.txt");
                                        if (u == 1)
                                        {
                                            int num55 = 5;
                                            string fileKILL5 = "C:\\Users\\Jared\\source\\repos\\Brain Stew\\Brain Stew\\Save\\SaveFile5.txt";
                                            fileStorage5.EmptyFile(fileKILL5);
                                            fileStorage5.DropTable(num55);

                                            Console.WriteLine("\n SaveFile 5 deleted!");
                                        }
                                        else if (u == 2)
                                        {
                                            break;
                                        }

                                        break;

                                    default:
                                        Console.Clear();
                                        Console.WriteLine("Invalid Input!");
                                        continue;

                                }
                            }
                            catch (FormatException ex)
                            {
                                Console.Clear();
                                Console.WriteLine("\nInvalid Input!");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("\nEnter any key to return to Menu");
                                Console.WriteLine("Enter X to exit");
                                string a = Console.ReadLine();
                                if (a.ToUpper() == "X")
                                {
                                    Console.Clear();
                                    Console.WriteLine("Bye Bye ~ ");
                                    break;
                                }

                                else
                                {
                                    Console.Clear();
                                    continue;
                                }
                            }
                            break;
                        case 4:

                            Console.Clear();
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("\nEnter any key to return to Menu");
                            Console.WriteLine("Enter X to exit");
                            string aa = Console.ReadLine();
                            if (aa.ToUpper() == "X")
                            {
                                Console.Clear();
                                Console.WriteLine("Bye Bye ~ ");
                                break;
                            }

                            else
                            {
                                Console.Clear();
                                continue;
                            }
                            break;

                    }
                }

                else if (menu == 3) // Story
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("\nIn the year 2024 there was a Computer Science student struggling to create a game for an upcoming contest he has entered. He was having many sleepless nights, and his selfcare habits and diet wasn’t making things better for him. The student was borderline overdosing on energy drinks and ramen noodles.  \r\n\r\nThe contest has finally come, and the student has... lost?!?! The student couldn’t make an award-winning game despite sacrificing his sleep, comfort, and gaming hours! The student couldn’t bear the defeat. His sleepless nights and endless consumption of junk food has finally got to him, the chemicals he was stuffing himself with has fried his brain and has created an infection. The student went berserk and started attacking everyone. The student was detained. One of his victims were bitten and the student's saliva has also infected the bitten person starting another violent rampage. This Game Developing contest didn’t only produce new and fun games, it has also produced a dangerous infection that would change the world forever. \r\n\r\nMany years into the future the world has adjusted to the apocalypse, and things aren’t so dire anymore. You and your brother born into this apocalyptic world are just trying to have fun and have taken an interest on old video games from the 20s (2020s). Unfortunately, during one of your expeditions to find games and consoles your brother was attacked and bitten by a zombie. Your brother is infected and you just can’t bring yourself to kill him. You keep your brother in your basement and you notice that your brother is drawn to old energy drinks, you realize that zombies only attack humans but never eat them. You tell yourself that this has to mean something, why would zombies be interested in consuming energy drinks. You now embark on a journey to find wherever or whoever manufactured these energy drinks and uncover the truth. In a world where every option has been exhausted and resources have been depleted, you must find new and creative ways to kick zombie ass and save your brother. ");
                }

                else if (menu == 4) // Credits
                {
                    Console.Clear();
                    Console.WriteLine("-----CREDITS-----");
                    Console.WriteLine("\nAgustin, Sheen Reinnier ");
                    Console.WriteLine("Documentarist (ayaw gumawa nung iba ng flowchart)");
                    Console.WriteLine("'Mataas kung tumalon, malalim kung magmahal sa mga nanay na may anak na walong taon'");
                    Console.WriteLine("- Hari ng tugma");

                    Console.WriteLine("\nPerez, Kyler Lee M.");
                    Console.WriteLine("Programmerist (Pero kulang ung ginawang code)");
                    Console.WriteLine("Shout out parin kay Mr.Pares andami nyang tinype.");
                    Console.WriteLine("'Sana Bra nalang ako, para malapit ako sa puso mo' - kyler pares 2024");

                    Console.WriteLine("\nCaparuzo, Jared Asher C.");
                    Console.WriteLine("Dakilang Leader");
                    Console.WriteLine("!!!Pancit Canton!!!");
                    Console.WriteLine("Taga dala ng Laptop");
                    Console.WriteLine("Nagtapos ng gawa ni Kyler ;)");
                    Console.WriteLine("'If you do everything on the last minute, it only takes a minute to do' -Jared 2024");
                }

                else if (menu == 5) // ByeBye
                {
                    Console.Clear();
                    Console.WriteLine("Bye Bye ~ ");
                    break;
                }


                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input!");

                }

                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("\nEnter any key to return to Menu");
                Console.WriteLine("Enter X to exit");
                string x = Console.ReadLine();
                if (x.ToUpper() == "X")
                {
                    Console.Clear();
                    Console.WriteLine("Bye Bye ~ ");
                    break;
                }

                else
                {
                    Console.Clear();
                    continue;
                }
            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.WriteLine("\nInvalid Input!");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("\nEnter any key to return to Menu");
                Console.WriteLine("Enter X to exit");
                string x = Console.ReadLine();
                if (x.ToUpper() == "X")
                {
                    Console.Clear();
                    Console.WriteLine("Bye Bye ~ ");
                    break;
                }

                else
                {
                    Console.Clear();
                    continue;
                }
            }
        }
    }
}
