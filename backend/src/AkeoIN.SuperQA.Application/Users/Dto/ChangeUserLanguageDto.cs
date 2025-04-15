using System.ComponentModel.DataAnnotations;

namespace AkeoIN.SuperQA.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}