namespace MusicDTO
{
    public class ChordDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int? LineNumber { get; set; }
        public int? IndexInLine { get; set; }
        public int? ScaleId { get; set; }
        public int? SongId { get; set; }
        public int? SequenceId { get; set; }
        public int? Spaces { get; set; }
        public string? Adding { get; set; }
        public string? Reason { get; set; }


    }
}
