using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        //this is primary key
        public byte Id { get; set; }
        [Required]//makes the name required from nullable
        [StringLength(255)]//change the datatype to string
        public string Name { get; set; }
        //short because we don't need any values more than 32000
        public short SignUpFree { get; set; }
        //12 for 12 months so byte
        public byte DurationInMonths { get; set; }
        //0 to 100% so byte
        public byte DiscountRate { get; set; }

        //so if you change the values 0 or 1 somewhere else the compiler will show some errors
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}