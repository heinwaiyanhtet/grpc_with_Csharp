using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System;
namespace BlogGrpc.Models;

public class Blog
{
    [Key]
    public int Id {get;set;}

    [Required]
    [MaxLength(255)]
    [MinLength(4)]
    public string Title {get;set;}
    public string? ImageUrl {get;set;}

    [Required]
    [MinLength(200)]
    public string Description {get;set;}    
    public string Author {get;set;} 

}   