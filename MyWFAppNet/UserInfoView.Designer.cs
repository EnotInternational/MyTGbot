namespace MyWFAppNet
{
    partial class UserInfoView
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bindingSource1 = new BindingSource(components);
            flowLayoutPanel1 = new FlowLayoutPanel();
            UserName = new Label();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // bindingSource1
            // 
            bindingSource1.CurrentChanged += bindingSource1_CurrentChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(textBox1);
            flowLayoutPanel1.Location = new Point(17, 38);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(181, 199);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // UserName
            // 
            UserName.AutoSize = true;
            UserName.FlatStyle = FlatStyle.Flat;
            UserName.Location = new Point(46, 0);
            UserName.Name = "UserName";
            UserName.Size = new Size(63, 15);
            UserName.TabIndex = 0;
            UserName.Text = "User name";
            UserName.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(3, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(178, 23);
            textBox1.TabIndex = 1;
            // 
            // UserInfoView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(UserName);
            Controls.Add(flowLayoutPanel1);
            Name = "UserInfoView";
            Size = new Size(214, 240);
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private BindingSource bindingSource1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label UserName;
        private TextBox textBox1;
    }
}
