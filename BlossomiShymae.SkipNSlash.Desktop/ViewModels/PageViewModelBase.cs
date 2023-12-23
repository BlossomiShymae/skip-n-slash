using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlossomiShymae.SkipNSlash.Desktop.ViewModels
{
    public abstract class PageViewModelBase : ViewModelBase
    {
        public abstract void OnNavigated(string parameter);
    }
}