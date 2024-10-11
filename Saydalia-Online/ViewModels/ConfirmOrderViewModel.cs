using System.ComponentModel.DataAnnotations;

namespace Saydalia_Online.ViewModels
{
    public class ConfirmOrderViewModel
    {
        [Required(ErrorMessage = "Address is required.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [EgyptianPhoneNumber(ErrorMessage = "Please enter a valid Egyptian phone number.")]
        public string? Phone { get; set; }
    }

}
