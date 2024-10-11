// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Saydalia_Online.Areas.Identity.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Saydalia_Online.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Saydalia_Online_AuthUser> _userManager;
        private readonly SignInManager<Saydalia_Online_AuthUser> _signInManager;
        private IHostingEnvironment _host;


        public IndexModel(
            UserManager<Saydalia_Online_AuthUser> userManager,
            SignInManager<Saydalia_Online_AuthUser> signInManager,
            IHostingEnvironment host)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _host = host;

        }

        public string Username { get; set; }
        public string NameUser { get; set; }
        public string pathImg { get; set; }




        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            [EgyptianPhoneNumber(ErrorMessage = "Please enter a valid Egyptian phone number.")]
            public string PhoneNumber { get; set; }

            public string NewName { get; set; }

            [Phone]
            [Display(Name = "New phone number")]
            [EgyptianPhoneNumber(ErrorMessage = "Please enter a valid Egyptian phone number.")]
            public string NewPhone { get; set; }

            [NotMapped]
            public IFormFile clientfiles { get; set; }

        }

        private async Task LoadAsync(Saydalia_Online_AuthUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            Username = userName;
            NameUser = user.name;
            pathImg = user.ImagePath;



            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.clientfile = Input.clientfiles;

            string fileName = string.Empty;
            if (Input.clientfiles != null)
            {
                string myUpload = Path.Combine(_host.WebRootPath, "images");
                fileName = Input.clientfiles.FileName;
                string fullPath = Path.Combine(myUpload, fileName);

                // Save the file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await Input.clientfiles.CopyToAsync(fileStream);
                }

                // Save the image path to the database
                user.ImagePath = fileName;

                var setimg = await _userManager.UpdateAsync(user);
                if (!setimg.Succeeded)
                {
                    TempData["StatusMessage"] = "Unexpected error when trying to set new picture .";
                    return RedirectToPage();
                }
            }

            // Only update phone number if a new one is provided and it's not null or empty
            if (!string.IsNullOrWhiteSpace(Input.NewPhone) && Input.NewPhone != user.PhoneNumber)
            {
                var existingUser = await _userManager.Users
              .FirstOrDefaultAsync(u => u.PhoneNumber == Input.NewPhone);

                if (existingUser != null)
                {
                    // Phone number is already registered
                    //ModelState.AddModelError(string.Empty, "The phone number is already taken.");
                    TempData["StatusMessage"] = "The phone number is already taken.";
                    return RedirectToPage();
                }
                user.PhoneNumber = Input.NewPhone;
                var setPhoneResult = await _userManager.UpdateAsync(user);
                if (!setPhoneResult.Succeeded)
                {
                    TempData["StatusMessage"] = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            // Only update the name if a new one is provided
            if (!string.IsNullOrWhiteSpace(Input.NewName) && user.name != Input.NewName)
            {
                user.name = Input.NewName;
                var updateUserResult = await _userManager.UpdateAsync(user);
                if (!updateUserResult.Succeeded)
                {
                    TempData["StatusMessage"] = "Unexpected error when trying to update name.";
                    return RedirectToPage();
                }
            }

            // Refresh sign-in session
            await _signInManager.RefreshSignInAsync(user);
            TempData["successData"] = "Your profile has been updated";
            await LoadAsync(user);
            return RedirectToPage();
        }


    }
}
