using AutoMapper;
using PreSaleForm.Application.PreSaleForms.Commands.Create;
using PreSaleForm.Application.PreSaleForms.Commands.Update;
using PreSaleForm.Domain.Entities;

namespace PreSaleForm.Application.PreSaleForms;

public class PreSaleFormProfile : Profile
{
    public PreSaleFormProfile()
    {
        CreateMap<CreatePreSaleFormRequest, PreSaleFormEntity>()
            .ForMember(dest => dest.Products, opt => opt.Ignore()); // Products manuel eklenecek
        CreateMap<PreSaleFormProductDto, PreSaleFormProduct>();
        CreateMap<PreSaleFormEntity, CreatePreSaleFormResponse>();
        CreateMap<PreSaleFormEntity, UpdatePreSaleFormResponse>();
    }
}