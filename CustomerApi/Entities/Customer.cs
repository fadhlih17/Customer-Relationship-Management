using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerApi.Entities;

[Table(name:"m_customer")]
public class Customer
{
    [Key, Column(name:"id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(name:"first_name", TypeName = "varchar(50)"), Required]
    public string FirstName { get; set; }
    [Column(name:"last_name", TypeName = "varchar(50)")]
    public string LastName { get; set; }
    [Column(name:"email"), Required, EmailAddress]
    public string Email { get; set; }
    [Column(name:"address", TypeName = "varchar(100)")]
    public string HomeAddress { get; set; }
}