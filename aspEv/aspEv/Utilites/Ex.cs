using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace aspEv.Utilites
{
    public static class Ex
    {
        public static bool CheckSize( this IFormFile formFile,int kb)
        {
            if (formFile.Length/1024*1024>kb)
            {
                return true;



            }
            return false;
        }
        public static bool CheckType( this IFormFile formFile,string typee)
        {
            if (formFile.ContentType.Contains(typee))
            {
                return true;

            }
            return false;
        }
        public async static Task<string> SavaChangeAsync( this IFormFile formFile,string pathh)
        {
            string Musi = Guid.NewGuid().ToString() + formFile.FileName;
            string path=Path.Combine(pathh, Musi);
            using (FileStream file= new FileStream(path, FileMode.Create))
            {
               await formFile.CopyToAsync(file);
            }
            return Musi;

        }

    }
}
