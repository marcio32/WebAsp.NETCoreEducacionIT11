using Data.Dto;
using Data.Entities;

namespace Web.ViewModels
{
    public class ServiciosViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public static implicit operator ServiciosViewModel(ServiciosDto servicioDto)
        {
            var servicioViewModel = new ServiciosViewModel();
            servicioViewModel.Id = servicioDto.Id;
            servicioViewModel.Nombre = servicioDto.Nombre;
            servicioViewModel.Activo = servicioDto.Activo;
            return servicioViewModel;

        }
    }
}
