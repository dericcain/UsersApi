using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(30)]
        public string LastName { get; set; }
        
        [Required]
        [StringLength(60)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(10)]
        public string Phone { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}