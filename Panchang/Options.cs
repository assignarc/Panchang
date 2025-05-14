

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using org.transliteral.panchang;
namespace org.transliteral.panchang.app
{
    public delegate object ApplyOptions(object sender);
    /// <summary>
    /// Display a PropertyGrid for any object, and deal with
    /// event handlers to perform any requested updates
    /// </summary>
    public class Options : Form
    {
        public PropertyGrid pGrid;
        private Button bApply;
        private Button bCancel;
        private ApplyOptions applyEvent;
        private Button bOK;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public Options(object a, ApplyOptions o, bool NoCancel)
        {
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            pGrid.SelectedObject = new GlobalizedPropertiesWrapper(a);
            pGrid.HelpVisible = true;
            applyEvent = o;
            bCancel.Enabled = false;
        }
        public Options(object a, ApplyOptions o)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            pGrid.SelectedObject = new GlobalizedPropertiesWrapper(a);
            applyEvent = o;
            //this.applyEvent(pGrid.SelectedObject);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pGrid = new PropertyGrid();
            bApply = new Button();
            bCancel = new Button();
            bOK = new Button();
            SuspendLayout();
            // 
            // pGrid
            // 
            pGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left
                | AnchorStyles.Right;
            pGrid.CommandsVisibleIfAvailable = true;
            pGrid.LargeButtons = false;
            pGrid.LineColor = SystemColors.ScrollBar;
            pGrid.Location = new Point(8, 8);
            pGrid.Name = "pGrid";
            pGrid.PropertySort = PropertySort.Categorized;
            pGrid.Size = new Size(284, 216);
            pGrid.TabIndex = 1;
            pGrid.Text = "propertyGrid1";
            pGrid.ToolbarVisible = false;
            pGrid.ViewBackColor = SystemColors.Window;
            pGrid.ViewForeColor = SystemColors.WindowText;
            pGrid.Click += new EventHandler(pGrid_Click);
            // 
            // bApply
            // 
            bApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bApply.Location = new Point(8, 232);
            bApply.Name = "bApply";
            bApply.TabIndex = 0;
            bApply.Text = "Apply";
            bApply.Click += new EventHandler(bApply_Click);
            // 
            // bCancel
            // 
            bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bCancel.DialogResult = DialogResult.Cancel;
            bCancel.Location = new Point(204, 232);
            bCancel.Name = "bCancel";
            bCancel.TabIndex = 2;
            bCancel.Text = "Cancel";
            bCancel.Click += new EventHandler(bCancel_Click);
            // 
            // bOK
            // 
            bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                | AnchorStyles.Right;
            bOK.Location = new Point(104, 232);
            bOK.Name = "bOK";
            bOK.Size = new Size(79, 23);
            bOK.TabIndex = 3;
            bOK.Text = "OK";
            bOK.Click += new EventHandler(bOK_Click);
            // 
            // MhoraOptions
            // 
            AcceptButton = bApply;
            AutoScaleBaseSize = new Size(5, 13);
            CancelButton = bCancel;
            ClientSize = new Size(296, 273);
            Controls.Add(bOK);
            Controls.Add(bCancel);
            Controls.Add(bApply);
            Controls.Add(pGrid);
            Name = "MhoraOptions";
            Text = "Options";
            Load += new EventHandler(MhoraOptions_Load);
            ResumeLayout(false);

        }
        #endregion

        private void MhoraOptions_Load(object sender, EventArgs e)
        {

        }

        private void pGrid_Click(object sender, EventArgs e)
        {

        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Apply(bool bKeepOpen)
        {
            GlobalizedPropertiesWrapper wrapper = (GlobalizedPropertiesWrapper)pGrid.SelectedObject;
            object objApplied = applyEvent(wrapper.GetWrappedObject());
            if (bKeepOpen)
                pGrid.SelectedObject = new GlobalizedPropertiesWrapper(objApplied);
        }
        private void bApply_Click(object sender, EventArgs e)
        {
            Apply(true);
        }

        private void bOK_Click(object sender, EventArgs e)
        {
            Apply(false);
            Close();
        }
    }

    public class GlobalizedPropertiesWrapper : ICustomTypeDescriptor
    {
        private object obj = null;
        public GlobalizedPropertiesWrapper(object _obj)
        {
            obj = _obj;
        }
        public object GetWrappedObject()
        {
            return obj;
        }
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(obj, true);
        }
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(obj, true);
        }
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(obj, true);
        }
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(obj, true);
        }
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(obj, true);
        }
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(obj, true);
        }
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(obj, editorBaseType, true);
        }
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(obj, attributes, true);
        }
        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(obj, true);
        }
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return obj;
        }
        public bool IsPropertyVisible(PropertyDescriptor prop)
        {
            if (null != prop.Attributes[typeof(InVisibleAttribute)])
                return false;

            return true;
            //	Console.WriteLine ("Property {0} is invisible", prop.DisplayName);
            //return true;
        }
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            ArrayList orderedProperties = new ArrayList();
            PropertyDescriptorCollection retProps = new PropertyDescriptorCollection(null);
            PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(obj, attributes, true);
            foreach (PropertyDescriptor oProp in baseProps)
            {
                Attribute attOrder = oProp.Attributes[typeof(PropertyOrderAttribute)];
                if (false == IsPropertyVisible(oProp))
                    continue;

                if (attOrder != null)
                {
                    //
                    // If the attribute is found, then create an pair object to hold it
                    //
                    PropertyOrderAttribute poa = (PropertyOrderAttribute)attOrder;
                    orderedProperties.Add(new PropertyOrderPair(oProp, oProp.Name, poa.Order));
                }
                else
                {
                    //
                    // If no order attribute is specifed then given it an order of 0
                    //
                    orderedProperties.Add(new PropertyOrderPair(oProp, oProp.Name, 0));
                }
                //retProps.Add (new GlobalizedPropertyDescriptor(oProp));

                //Console.WriteLine ("Enumerating property {0}", oProp.DisplayName);
                //PGDisplayName invisible = (PGDisplayName)oProp.Attributes[typeof(PGNotVisible)];
                //if (invisible == null)
                //else
                //	Console.WriteLine ("Property {0} is invisible", oProp.DisplayName);
            }

            orderedProperties.Sort();
            foreach (PropertyOrderPair pop in orderedProperties)
            {
                Logger.Info($"Adding sorted {pop.Name}");
                retProps.Add(new GlobalizedPropertyDescriptor(pop.Property));
            }
            return retProps;
        }
        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection baseProps = TypeDescriptor.GetProperties(obj, true);
            return baseProps;
        }

    }

    public class GlobalizedPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor basePropertyDescriptor;

        public GlobalizedPropertyDescriptor(PropertyDescriptor basePropertyDescriptor) : base(basePropertyDescriptor)
        {
            this.basePropertyDescriptor = basePropertyDescriptor;
        }

        public override bool CanResetValue(object component)
        {
            return basePropertyDescriptor.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get { return basePropertyDescriptor.ComponentType; }
        }

        public override string DisplayName
        {
            get
            {
                VisibleAttribute dn = (VisibleAttribute)basePropertyDescriptor.Attributes[typeof(VisibleAttribute)];
                if (dn != null)
                    return dn.Text;
                return basePropertyDescriptor.DisplayName;
            }
        }

        public override string Description
        {
            get { return basePropertyDescriptor.Description; }
        }

        public override object GetValue(object component)
        {
            return basePropertyDescriptor.GetValue(component);
        }

        public override bool IsReadOnly
        {
            get { return basePropertyDescriptor.IsReadOnly; }
        }

        public override string Name
        {
            get { return basePropertyDescriptor.Name; }
        }

        public override Type PropertyType
        {
            get { return basePropertyDescriptor.PropertyType; }
        }

        public override void ResetValue(object component)
        {
            basePropertyDescriptor.ResetValue(component);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return basePropertyDescriptor.ShouldSerializeValue(component);
        }

        public override void SetValue(object component, object value)
        {
            basePropertyDescriptor.SetValue(component, value);
        }
    }

    public class PropertyOrderPair : IComparable
    {
        private int _order;
        private string _name;
        private PropertyDescriptor _pdesc;
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public PropertyDescriptor Property
        {
            get { return _pdesc; }
        }

        public PropertyOrderPair(PropertyDescriptor pdesc, string name, int order)
        {
            _pdesc = pdesc;
            _order = order;
            _name = name;
        }

        public int CompareTo(object obj)
        {
            //
            // Sort the pair objects by ordering by order value
            // Equal values get the same rank
            //
            int otherOrder = ((PropertyOrderPair)obj)._order;
            if (otherOrder == _order)
            {
                //
                // If order not specified, sort by name
                //
                string otherName = ((PropertyOrderPair)obj)._name;
                return string.Compare(_name, otherName);
            }
            else if (otherOrder > _order)
            {
                return -1;
            }
            return 1;
        }
    }

}
