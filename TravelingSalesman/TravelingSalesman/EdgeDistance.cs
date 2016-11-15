namespace TravelingSalesman
{
    internal class EdgeDistance : Edge
    {
        private static readonly Calculator Calculator = new Calculator();
        public readonly double Distance;


        public EdgeDistance(Node a, Node b, double? distance = null)
        {
            NodeA = a;
            NodeB = b;
            Distance = distance ?? Calculator.Distance(a, b);
        }

        public bool Equals(EdgeDistance other)
        {
            return ((NodeA.Equals(other.NodeA) && NodeB.Equals(other.NodeB)) ||
                    (NodeA.Equals(other.NodeB) && NodeB.Equals(other.NodeA)))
                   && Distance.Equals(other.Distance);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            return obj is EdgeDistance && Equals((EdgeDistance) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = NodeA.GetHashCode();
                hashCode = hashCode ^ NodeB.GetHashCode();
                hashCode = (hashCode*397) ^ Distance.GetHashCode();
                return hashCode;
            }
        }
    }
}