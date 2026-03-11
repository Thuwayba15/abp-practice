using System.ComponentModel.DataAnnotations;

namespace AbpPractice.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}