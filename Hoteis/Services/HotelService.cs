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

    public HoteisDto GetHoteisDto()
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
        var Hoteis = GetHotels().ToArray();
        var index = Array.IndexOf(Hoteis, Hoteis.Where(p => p.Nome.Equals(Nome)).FirstOrDefault());
        var Hotel = new DetailsDto()
        {
            Current = Hoteis[index],
            Prior = index - 1 < 0 ? null : Hoteis[index - 1],
            Next = index + 1 >= Hoteis.Count() ? null : Hoteis[index + 1]
        };
        return Hotel;
    }

    public Estado GetEstado(string Nome)
    {
        var Estados = GetEstados();
        return Estados.Where(t => t.Nome == Nome).FirstOrDefault();
    }

    private void PopularSessao()
    {
        if (string.IsNullOrEmpty(_session.HttpContext.Session.GetString("Estados")))
        {
            _session.HttpContext.Session.SetString("Hoteis", LerArquivo(hotelFile));
            _session.HttpContext.Session.SetString("Estados", LerArquivo(estadoFile));
        }
    }

    private string LerArquivo(string fileName)
    {
        using (StreamReader leitor = new StreamReader(fileName))
        {
            string dados = leitor.ReadToEnd();
            return dados;
        }
    }
}