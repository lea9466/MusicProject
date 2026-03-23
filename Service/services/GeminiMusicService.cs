using GenerativeAI;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicDTO;
using MusicInterfaces.ServiceInterfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace Service.services
{
    public class GeminiMusicService : IGemini
    {
        private readonly HttpClient _httpClient;
        // הערה: מומלץ בסיום הפיתוח להעביר את המפתח ל-appsettings.json
        public GeminiMusicService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetEnhancedChordsJsonAsync(FullSongDto request)
        {
            string jsonInput = JsonSerializer.Serialize(request);

            string prompt = $@"
        אתה מומחה לתיאוריה מוזיקלית. קיבלת שיר בפורמט JSON הכולל מילים (wordLines) ואקורדים (chords).

        המשימה: 
        1.הצע שיפורים לאקורדים הקיימים כדי להוסיף עומק ורגש (למשל: הפיכת Am ל-Am9, הוספת אקורדים עוברים וכו').
        2. כתוב פסקה של המלצות מוזיקליות חופשיות (למשל: 'במעבר לפזמון מומלץ להוסיף אקורד מסוים או בין אקורד זה וזה מומלץ להוסיף אקורד מסוים X כדי ליצור מתח', או 'בשורה השנייה כדאי לנגן חלש יותר').

        חוקים נוקשים:
        1. החזר אך ורק מערך JSON של אקורדים במבנה המדויק שקיבלת (Name, LineNumber, Spaces, Adding,Reason).
        2. שמור על ה-LineNumber המקורי של כל אקורד כדי שיישאר מעל השורה הנכונה.
        3. שמור על ה-Spaces כדי שהאקורד יישאר במיקום האופקי הנכון.
        4. אל תוסיף טקסט חופשי לפני או אחרי ה-JSON!!!!!.
        5. המבנה חייב להיות כ [{{""Name"": ""..."", ""LineNumber"": 0, ...}}]
        6. לכל אקורד הוסף שדה Reason ובו משפט בעברית למה בחרת להוסיף אותו ומה הוא מוסיף לשיר
        - החזר אך ורק JSON.
        - המבנה חייב להיות:
          {{
            ""Chords"": [{{ ""Name"": ""..."", ""LineNumber"": 0, ... }}],
            ""MusicalRecommendations"": ""כאן יבוא הטקסט החופשי עם ההמלצות שלך בעברית""
          }}
        הנתונים:
        {jsonInput}";

            return await CallGeminiApiAsync(prompt);
        }

        private async Task<string> CallGeminiApiAsync(string prompt)
        {
            // 1. הגדרת ה-API Key והכתובת (החליפי ב-KEY שלך)
            string apiKey = "AIzaSyCnmpivfTLs9AcXBEz4J2GGF0OTPUZXv9s";
            string model = "gemini-2.5-flash";
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = prompt } } }
                }
            };

            // תיקון: שליחת ה-URL במקום ה-client
            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Google Error: {response.StatusCode}. Details: {error}");
            }

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            string rawJson = result.GetProperty("candidates")[0]
                                   .GetProperty("content")
                                   .GetProperty("parts")[0]
                                   .GetProperty("text")
                                   .GetString();

            // טיפול במקרה ש-Gemini מחזיר את ה-JSON בתוך בלוק של קוד (Markdown)
            return rawJson;
        }

        // פונקציית עזר לניקוי ה-JSON
        private string CleanJson(string text)
        {
            if (text.StartsWith("```json"))
            {
                text = text.Replace("```json", "").Replace("```", "").Trim();
            }
            else if (text.StartsWith("```"))
            {
                text = text.Replace("```", "").Trim();
            }
            return text;
        }
    }
}
