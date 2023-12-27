using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.IO;

namespace ProjectAPI.Models
{
    public class FileManager
    {
        public static async Task<byte[]?> FileToImage(IFormFile image)
        {
            string filetxt = Path.GetExtension(image.FileName);
            if (filetxt == ".jpeg" || filetxt == ".jpg" || filetxt == ".PNG")
            {
                await using var memoryStream = new MemoryStream();
                await image.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }
    }
}
