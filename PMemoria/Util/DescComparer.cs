namespace PMemoria.Util;

public class DescComparer<T> : IComparer<T> where T : IComparable<T> {
    public int Compare(T? x, T? y) {
        return y?.CompareTo(x) ?? -1;
    }
}