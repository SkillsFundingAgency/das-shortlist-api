namespace SFA.DAS.Shortlist.Application.Domain.Entities
{
    public class Shortlist
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int Ukprn { get; set; }
        public int Larscode { get; set; }
        public string LocationDescription { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
