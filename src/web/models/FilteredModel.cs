namespace web.models
{
    public class FilteredModel
    {
        public int BirthdayStart { get; set; }
        public int BirthdayEnd { get; set; }
        public Range? Range { get; set; }
        public string IsOptin { get; set; }
        public string IsMember { get; set; }

        // Helper for super view nancy
        public bool HasRange => Range.HasValue;
        public bool IsBeforeRange => HasRange && Range == models.Range.Before;
        public bool IsAfterRange => HasRange && Range == models.Range.After;
        public bool IsBetweenRange => HasRange && Range == models.Range.Between;
        public bool HasBirthdayStart => BirthdayStart > 0;
        public bool HasBirthdayEnd => BirthdayEnd > 0;
        public bool HasOptin => !string.IsNullOrEmpty(IsOptin);
        public bool IsYesOptin => HasOptin && bool.Parse(IsOptin);
        public bool IsNoOptin => HasOptin && !bool.Parse(IsOptin);
        public bool HasMember => !string.IsNullOrEmpty(IsMember);
        public bool IsYesMember => HasMember && bool.Parse(IsMember);
        public bool IsNoMember => HasMember && !bool.Parse(IsMember);
    }

    public enum Range
    {
        Before = 0,
        After = 1,
        Between = 2
    }
}
