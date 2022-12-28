using Data.Dto;
using Data.Entities;

namespace Web.ViewModels
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator RolesViewModel(RolesDto rolDto)
        {
            var rolViewModel = new RolesViewModel();
            rolViewModel.Id = rolDto.Id;
            rolViewModel.Nombre = rolDto.Nombre;
            rolViewModel.Activo = rolDto.Activo;
            return rolViewModel;

        }
    }
}
