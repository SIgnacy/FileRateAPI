namespace API.Extensions;

public static class FormFileExtensions
{
    public async static Task<byte[]> ToByteArrayAsync(this IFormFile file)
    {
        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
