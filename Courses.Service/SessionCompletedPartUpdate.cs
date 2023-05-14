global using global::Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Courses.Service
{
    public class SessionCompletedPartUpdate
    {

        public static void SessionCompletedPartUpdateMethod(string partId) 
        {
            //var completedPartsBytes = HttpContext.Session.Get("completedParts");
            //SortedSet<int> completedParts = null;
            //if (completedPartsBytes != null)
            //{
            //    completedParts = JsonSerializer.Deserialize<SortedSet<int>>(completedPartsBytes);
            //}
            //else
            //{
            //    completedParts = new SortedSet<int>();
            //}
            //completedParts.Add(int.Parse(partId));
            //HttpContext.Session.Set("completedParts", JsonSerializer.SerializeToUtf8Bytes(completedParts));
        }
    }
}
