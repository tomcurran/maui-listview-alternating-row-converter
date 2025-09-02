namespace maui_listview_alternating_row_converter;

using System.Globalization;

public class AlternatingRowColorConverter : BindableObject, IValueConverter
{
    public static readonly BindableProperty PrimaryColorProperty =
        BindableProperty.Create(nameof(PrimaryColor), typeof(string), typeof(AlternatingRowColorConverter), null);

    public static readonly BindableProperty SecondaryColorProperty =
        BindableProperty.Create(nameof(SecondaryColor), typeof(string), typeof(AlternatingRowColorConverter), null);

    public string PrimaryColor
    {
        get => (string)GetValue(PrimaryColorProperty);
        set => SetValue(PrimaryColorProperty, value);
    }

    public string SecondaryColor
    {
        get => (string)GetValue(SecondaryColorProperty);
        set => SetValue(SecondaryColorProperty, value);
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var colorResourceName = PrimaryColor;

        try
        {
            var itemsSource = ((ListView)parameter).ItemsSource as IEnumerable<object>;
            if (FindIndexOf(itemsSource, value) % 2 == 1)
            {
                colorResourceName = SecondaryColor;
            }
        }
        catch (Exception ex)
        {
            // intentionally blank
        }

        Application.Current!.Resources.TryGetValue(colorResourceName, out var resource);
        if (resource != null)
        {
            return (Color)resource;
        }

        return Colors.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }

    private static int FindIndexOf<T>(IEnumerable<T> source, T itemToFind)
    {
        int index = 0;
        foreach (T item in source)
        {
            if (EqualityComparer<T>.Default.Equals(item, itemToFind))
            {
                return index;
            }
            index++;
        }
        return -1;
    }
}
