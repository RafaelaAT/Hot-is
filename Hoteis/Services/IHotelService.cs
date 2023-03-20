using Hoteis.Models;
namespace Hoteis.Services;

public interface IHotelService
{
    List<Hotel> GetHotels();
    List<Estado> GetEstados();
    Hotel GetHotel(string Nome);
    HoteisDto GetHoteisDto();
    DetailsDto GetDetailedHotel(string Nome);
    Estado GetEstado(string Nome);
}