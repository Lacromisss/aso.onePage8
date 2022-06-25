using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspEv.Models
{
    public class User
    {
        public int Id { get; set; }
        public byte Raiting { get; set; }
        public string Name { get; set; }
        public string Degre { get; set; }
        public string SocialName { get; set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
