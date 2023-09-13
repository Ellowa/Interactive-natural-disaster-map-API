namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs
{
    public class GetAllUnconfirmedEventDto
    {
        public bool? AddIsChecked { get; set; } = false;

        public string? UserLogin { get; set; }

        public DateTime? AddedAt { get; set; }

        public string? SortColumn { get; set; }

        public string? SortOrder { get; set; }
    }
}
