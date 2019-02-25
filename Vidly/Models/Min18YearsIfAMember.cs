using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember: ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //ValidationContext is used to get the value of the customer form field
            var customer = (Customer)validationContext.ObjectInstance;

            //if the 'select the membership type' or 'pay as you go' than it should show error
            //Business Rule: the payasyougo can be selected by the person leess than 18 years old 
            if (customer.MembershipTypeid == MembershipType.Unknown 
                || customer.MembershipTypeid == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            //if no birth date is entered than show error
            if (customer.BirthDate == null)
            {
                return new ValidationResult("Birthdate is required.");
            }

            //the calucalation of age is more complex but for simplicity we are only taking the year(which is not feasible in realtime application)
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;

            return (age > 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer should be atleast 18 years old to go on a membership.");
        }
    }
}