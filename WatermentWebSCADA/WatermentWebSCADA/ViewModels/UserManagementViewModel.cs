using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WatermentWebSCADA.ViewModels
{
    [Table("User")]
    public class ListAllUsers
    {
        [Key]
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }        


        }
  
}