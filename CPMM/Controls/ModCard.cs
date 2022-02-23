using CPMM.Core.Mods;
using System.Windows;

namespace CPMM.Controls
{
    /// <summary>
    /// Card representing mod.
    /// </summary>
    public class ModCard : System.Windows.Controls.Control
    {
        /// <summary>
        /// Property for <see cref="Mod"/>.
        /// </summary>
        public static readonly DependencyProperty ModProperty = DependencyProperty.Register(nameof(Mod),
            typeof(IMod), typeof(ModCard), new PropertyMetadata(new Mod(), ModPropertyChangedCallback));

        public IMod Mod
        {
            get => (IMod)GetValue(ModProperty);
            set => SetValue(ModProperty, value);
        }

        private static void ModPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ModCard control) return;
        }
    }
}
