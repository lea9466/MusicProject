using MusicIinterfaces;
using MusicModels;
using Repository;
using System;
using Repository;
using MusicModels; 
using MusicDB;     



namespace Repository
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (Database db = new Database())
            //{
            //    db.categories.Add(new Category() { Name = "Food" });
            //    db.SaveChanges();
            //}
            
            

            Console.WriteLine("--- ברוכים הבאים למערכת ניהול המוזיקה ---");

            // 1. יצירת החיבור (הקונטקסט)
            using (var db = new DataBase())
            {
                // 2. יצירת הרפוסיטוריז
                var categoryRepo = new CategoryRepository(db);
                var songRepo = new SongRepository(db);
                var userRipo=new UserRepository(db);
                try
                {

                    User user = new User();
                    user.Name = "Test";
                    user.Email = "Test";
                    user.srcImage = "Test";
                    userRipo.AddItem(user);
                    // 4. הוספת שיר שקשור לקטגוריה הזו
                    //var mySong = new Song
                    //{
                    //    Name = "שיר לדוגמה",
                    //    Artist = "זמר מפורסם",
                    //    CategoryId = 1, // מחברים את השיר לקטגוריה שיצרנו הרגע
                    //    Date = new DateOnly(),
                    //    Language = "Hebrew"
                    //};
                    //songRepo.AddItem(mySong);
                    //Console.WriteLine($"השיר '{mySong.Name}' נוסף בהצלחה עם ID: {mySong.Id}!");

                    //Console.WriteLine("\n--- בדיקה מהירה: רשימת השירים במסד ---");
                    //var allSongs = songRepo.GetAll();
                    //foreach (var s in allSongs)
                    //{
                    //    Console.WriteLine($"שיר: {s.Name} | אמן: {s.Artist} | קטגוריה: {s.Category?.Name}");
                    //}
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"אופס! הייתה שגיאה: {ex.Message}");
                    if (ex.InnerException != null)
                        Console.WriteLine($"פרטים נוספים: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\nלחץ על מקש כלשהו ליציאה...");
            Console.ReadKey();


        }
    }
}
