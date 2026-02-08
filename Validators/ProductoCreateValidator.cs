using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Prueba_Completa_NET.Data;
using Prueba_Completa_NET.DTOs;


namespace Prueba_Completa_NET.Validators
{

    public class ProductoCreateValidator: AbstractValidator<ProductoCreateDTO>
    {
        public ProductoCreateValidator() { 
            
            RuleFor(p => p.ProductoId)
                .Equal(0).WithMessage("El ProductoId debe ser 0.");

            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es requerido.")
                .Length(3, 100).WithMessage("El nombre del producto debe tener entre 3 y 100 caracteres.");
            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio del producto debe ser mayor que cero.");
            RuleFor(p => p.Existencia)
                .GreaterThanOrEqualTo(0).WithMessage("La existencia del producto debe ser mayor o igual a cero");

        }
    }
}
