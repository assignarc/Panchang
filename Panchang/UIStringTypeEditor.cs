using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace org.transliteral.panchang.app
{

    public class UIStringTypeEditor : UITypeEditor
    {
        private IWindowsFormsEditorService edSvc = null;
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            string stringInit = "";
            if (value is string)
                stringInit = (string)value;
            LongStringEditor le = new LongStringEditor(stringInit);
            le.TitleText = "Event Description";
            edSvc.ShowDialog(le);
            return le.EditorText;
        }
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    }

}
