using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace KutuphaneUygulamasi.Models.ViewModels
{
    public class SetMemberRoleViewModel
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}
