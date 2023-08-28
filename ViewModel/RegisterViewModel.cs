﻿using System.ComponentModel.DataAnnotations;

namespace ReadAddict.ViewModel
{
    public class RegisterViewModel
    {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress(ErrorMessage ="Invalid Email Address")]
            public string Email { get; set; }

            [Required]
            public string PassWord { get; set; }
          
            public string PhoneNumber { get; set; }

    }
}
