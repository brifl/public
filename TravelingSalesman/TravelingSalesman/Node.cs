namespace TravelingSalesman
{
    public class Node
    {
        public string Name;
        public int X;
        public int Y;

        public bool Equals(Node other)
        {
            return string.Equals(Name, other.Name) && X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is Node && Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode*397) ^ X;
                hashCode = (hashCode*397) ^ Y;
                return hashCode;
            }
        }

        public Node Clone()
        {
            return new Node
            {
                Name = Name,
                X = X,
                Y = Y
            };
        }
    }
}