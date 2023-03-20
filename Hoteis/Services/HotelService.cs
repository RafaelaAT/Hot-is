using System.Text.Json;
using Hoteis.Models;

namespace Hoteis.Services;

public class HotelService : IHotelService
{
    private readonly IHttpContextAccessor _session;
    private readonly string hotelFile = @"Data\Hotel.json";
    private readonly string estadoFile = @"Data\Estado.json";

    public HotelService(IHttpContextAccessor session)
    {
        _session = session;
        PopularSessao();
    }

    public List<Hotel> GetHotels()
    {
        PopularSessao();
        var Hoteis = JsonSerializer.Deserialize<List<Hotel>>
            (_session.HttpContext.Session.GetString("Hoteis"));
        return Hoteis;
    }

    public List<Estado> GetEstados()
    {
        PopularSessao();
        var Estados = JsonSerializer.Deserialize<List<Estado>>
            (_session.HttpContext.Session.GetString("Estados"));
        return Estados;
    }

    public Hotel GetHotel(string Nome)
    {
        var Hoteis = GetHotels();
        return Hoteis.Where(p => p.Nome == Nome).FirstOrDefault();
    }

    public HoteisDto GetHoteisDto();
    {
        var Hote = new HoteisDto()
        {
            Hoteis = GetHotels(),
            Estados = GetEstados()

        };
        return Hote;
    }

    public DetailsDto GetDetailedHotel(string Nome)
    {
        var Hoteis = GetHotels();
        var Hote = new DetailsDto()
        {
            Current = Hoteis.Where(p => p.Nome == Nome)
                .FirstOrDefault(),
            Prior = Hoteis.OrderByDescending(p => p.Nome)
                .FirstOrDefault(p => p.Numero < Numero),
            Next = Hoteis.OrderBy(p => p.Nome)
                .FirstOrDefault(p => p.Nome > Nome),
        };
        return Hote;
    }

    public Estado GetEstado(string Nome)
    {
        var Estados = GetEstados();
        return Estados.Where(t => t.Nome == Nome).FirstOrDefault();
    }

    private void PopularSessao()
    {
        if (string.IsNullOrEmpty(_Session.HttpContext.Session.GetString("Estados")))
        {
            _session.HttpContext.Session
                .SetString("Hoteis", LerArquivo(hotelFile));
            _session.HttpContext.Session
                .SetString("Estados", LerArquivo(estadosFile));
        }
    }
}