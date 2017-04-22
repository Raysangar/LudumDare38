public static class ArrayExtension {
  public static T Find<T> (this T[] array, System.Predicate<T> predicate)
  {
    int i = 0;
    while (i < array.Length && !predicate(array[i]))
    {
      ++i;
    }
    return (i < array.Length) ? array[i] : default (T);
  }
}
