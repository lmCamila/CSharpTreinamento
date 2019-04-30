namespace Planner.Entity
{
    class TypePlan
    {
        public TypePlan() { }

        public TypePlan(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id  { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Id}- {Name} - {Description}";
        }
    }
}
