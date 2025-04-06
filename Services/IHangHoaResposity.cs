using WebApi1.Models;

namespace WebApi1.Services
{
    public interface IHangHoaResposity
    {
        List<HangHoaModel> GetAll(string search , double? from , double? to, string? sortBy , int page =1);
        

    }
}
