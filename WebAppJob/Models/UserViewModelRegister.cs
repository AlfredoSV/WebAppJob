using Domain.Entities;
using Framework.Security2023.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models;

public class UserViewModelRegister
{
    [Required(ErrorMessage = "El nombre de usuario es requerido.")]
    [DisplayName("User Name")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "La contraeña es requerida.")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "La contraseña es distinta.")]
    [DisplayName("Confirm Password")]
    [Required(ErrorMessage = "La contraeña a comparar es requerida.")]
    public string ConfirmPassword { get; set; }

    public DateTime DateCreated { get; set; }

    [Required(ErrorMessage = "El nombre es requerido.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Los apellidos son requeridos.")]
    public string LastName { get; set; }

    //[Required(ErrorMessage = "La edad es requerida.")]
    public int Age { get; set; }

    [DisplayName("Birth Date")]
    [Range(typeof(DateTime), "1/1/2004", "24/12/2025", ErrorMessage = "La fecha no es valida.")]
    [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
    public DateTime Birthdate { get; set; }

    [EmailAddress(ErrorMessage = "Ingresa un correo valido.")]
    [Required(ErrorMessage = "El correo es requerido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El Rfc es requerido.")]
    public string Rfc { get; set; }

    [EmailAddress(ErrorMessage = "Sekecciona una compañia.")]
    public string Companie { get; set; }

    public List<Company> Companies { get; set; }

    public List<SelectListItem> CompaniesItems
    {
        get
        {
            return this.Companies.Select(data => new SelectListItem() { Text = data.NameCompany, Value = data.Id.ToString() }).ToList();
        }
    }
}
