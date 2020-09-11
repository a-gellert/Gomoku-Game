namespace Gomoku.Core.Core
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public static bool Compare(Point point1, Point point2)
        {
            int first = point1.X * point1.X + point1.Y * point1.Y;
            int second = point2.X * point2.X + point2.Y * point2.Y;

            return first > second;
        }
        public bool IsOutOf(int size)
        {
            return X >= size || Y >= size || X < 0 || Y < 0;
        }
        public static bool operator ==(Point p1, Point p2)
        {
            if (ReferenceEquals(p1, p2))
                return true;

            if (ReferenceEquals(p1, null) || ReferenceEquals(p2, null))
                return false;

            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Point)) return false;

            Point that = (Point)obj;

            return that.X == this.X && that.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return (X.GetHashCode() ^ Y.GetHashCode());
        }
    }
}
