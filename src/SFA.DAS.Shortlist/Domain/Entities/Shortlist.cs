namespace SFA.DAS.Shortlist.Application.Domain.Entities
{
    public class Shortlist
    {
        public Guid Id { get; set; }
        public Guid ShortlistUserId { get; set; }
        public int Ukprn { get; set; }
        public int StandardId { get; set; }
        public string LocationDescription { get; set; }
        public float? Lat { get; set; }
        public float? Long { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
