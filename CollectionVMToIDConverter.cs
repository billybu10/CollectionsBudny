using CollectionsBudny.Models;
using CollectionsBudny.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsBudny
{
    public class CollectionVMToIDConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var a = (value as CollectionViewModel)?.ID;
            return a;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var xyz = value as string;
            if (String.IsNullOrWhiteSpace(xyz))
            {
                return null;
            }
            else
            {
                var b = new CollectionViewModel(Collection.Load(value as string));
                return b;
            }
        }
    }
}
