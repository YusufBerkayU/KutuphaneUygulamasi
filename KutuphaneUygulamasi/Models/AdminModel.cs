// Models/AdminModel.cs
using KutuphaneUygulamasi.Models;
using KutuphaneUygulamasi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace KutuphaneUygulamasi.Models
{
    public class AdminModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public SetMemberRoleViewModel SetMemberRoleViewModel { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
