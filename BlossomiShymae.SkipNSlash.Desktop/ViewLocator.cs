using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using BlossomiShymae.SkipNSlash.Desktop.ViewModels;

namespace BlossomiShymae.SkipNSlash.Desktop
{
    public class ViewLocator : IDataTemplate
    {
        public Control? Build(object? param)
        {
            if (param is null)
                return new TextBlock { Text = "data was null" };

            var name = param.GetType().FullName!.Replace("ViewModels", "Views")
                .Replace("ViewModel", string.Empty);
            var type = Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }
            else
            {
                return new TextBlock { Text = "Not Found:" + name };
            }
        }

        public bool Match(object? data)
        {
            if (data is ViewModelBase vm)
            {
                return true;
            }

            return false;
        }
    }
}