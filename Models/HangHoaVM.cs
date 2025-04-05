namespace WebApi1.Models
{
    public class HangHoaVM
    {
        public required string TenHangHoa { get; set; }
        
        public double DonGia { get; set; }
    }

    public class HangHoa : HangHoaVM {
    
        public Guid MaHangHoa { get; set; }
    }
}
