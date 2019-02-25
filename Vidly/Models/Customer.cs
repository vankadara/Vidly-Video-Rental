using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name.")]//makes the name required from nullable
        [StringLength(255)]//change the datatype to string
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }
        
        //Datetime is nullable(?), by default string in nullable in c#
        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        //navigator property class(Which navigates from one class to other)
        public MembershipType MembershipType { get; set; }

        //the foreign key(Sometime we don't need everything),The EF(enrtity framework) understands this convection and treat this property as a foreign key 
        [Display(Name = "Membership Type")]
        public byte MembershipTypeid { get; set; }
    }
}