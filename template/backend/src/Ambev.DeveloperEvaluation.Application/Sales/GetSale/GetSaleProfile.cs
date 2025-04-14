using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// AutoMapper profile for mapping between Sale and GetSaleResult.
/// </summary>
public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<SaleItem, GetSaleItemResult>();
        CreateMap<Sale, GetSaleResult>();
    }
}