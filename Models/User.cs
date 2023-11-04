using System.ComponentModel.DataAnnotations;
namespace DebuggingChallenge.Models;
#pragma warning disable CS8618
public class User
{
    [Required]
    public string Name {get;set;}

    public string? Location {get;set;}
}