﻿using System;
using System.Windows.Data;
using System.Windows.Controls;

namespace DoyenBiz.CitizenKiosk.Converters
{
    public class IsLastItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ContentPresenter contentPresenter = value as ContentPresenter;
            ItemsControl itemsControl = ItemsControl.ItemsControlFromItemContainer(contentPresenter);
            int index = itemsControl.ItemContainerGenerator.IndexFromContainer(contentPresenter);
            return (index == (itemsControl.Items.Count - 1));
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
