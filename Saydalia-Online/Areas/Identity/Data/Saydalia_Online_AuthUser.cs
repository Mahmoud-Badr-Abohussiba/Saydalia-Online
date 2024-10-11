using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Saydalia_Online.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Saydalia_Online_AuthUser class
public class Saydalia_Online_AuthUser : IdentityUser
{
    public string name { get; set; }
    public string? ImagePath { get; set; }


    [EgyptianPhoneNumber(ErrorMessage = "Please enter a valid Egyptian phone number.")]
    [ProtectedPersonalData]
    public override string? PhoneNumber { get; set; }

    [NotMapped]
    public IFormFile clientfile { get; set; }


}

