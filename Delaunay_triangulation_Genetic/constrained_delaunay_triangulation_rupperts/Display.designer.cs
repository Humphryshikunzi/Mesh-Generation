namespace DelaunayGenericTriangulation
{
    partial class Display
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.MainPic = new System.Windows.Forms.Panel();
            this.triangleSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtPic = new System.Windows.Forms.PictureBox();
            this.button_randommap = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelXYLocation = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.MapFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RefineCheckBox = new System.Windows.Forms.CheckBox();
            this.MainPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtPic)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPic
            // 
            this.MainPic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPic.BackColor = System.Drawing.Color.White;
            this.MainPic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPic.Controls.Add(this.RefineCheckBox);
            this.MainPic.Controls.Add(this.triangleSize);
            this.MainPic.Controls.Add(this.label3);
            this.MainPic.Controls.Add(this.mtPic);
            this.MainPic.Location = new System.Drawing.Point(16, 79);
            this.MainPic.Margin = new System.Windows.Forms.Padding(4);
            this.MainPic.Name = "MainPic";
            this.MainPic.Size = new System.Drawing.Size(1065, 738);
            this.MainPic.TabIndex = 0;
            this.MainPic.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPic_Paint);
            this.MainPic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainPic_MouseClick);
            this.MainPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainPic_MouseMove);
            // 
            // triangleSize
            // 
            this.triangleSize.Location = new System.Drawing.Point(774, 19);
            this.triangleSize.Name = "triangleSize";
            this.triangleSize.Size = new System.Drawing.Size(106, 22);
            this.triangleSize.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(569, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Enter Triangle Edge Size(1-5)";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // mtPic
            // 
            this.mtPic.BackColor = System.Drawing.Color.Transparent;
            this.mtPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtPic.Enabled = false;
            this.mtPic.Location = new System.Drawing.Point(0, 0);
            this.mtPic.Margin = new System.Windows.Forms.Padding(4);
            this.mtPic.Name = "mtPic";
            this.mtPic.Size = new System.Drawing.Size(1061, 734);
            this.mtPic.TabIndex = 0;
            this.mtPic.TabStop = false;
            // 
            // button_randommap
            // 
            this.button_randommap.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_randommap.Location = new System.Drawing.Point(16, 15);
            this.button_randommap.Margin = new System.Windows.Forms.Padding(4);
            this.button_randommap.Name = "button_randommap";
            this.button_randommap.Size = new System.Drawing.Size(177, 46);
            this.button_randommap.TabIndex = 1;
            this.button_randommap.Text = "Create random map";
            this.button_randommap.UseVisualStyleBackColor = true;
            this.button_randommap.Click += new System.EventHandler(this.button_randommap_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(405, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Left click to create mesh || Right click to delete mesh";
            // 
            // LabelXYLocation
            // 
            this.LabelXYLocation.AutoSize = true;
            this.LabelXYLocation.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelXYLocation.Location = new System.Drawing.Point(745, 28);
            this.LabelXYLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelXYLocation.Name = "LabelXYLocation";
            this.LabelXYLocation.Size = new System.Drawing.Size(0, 20);
            this.LabelXYLocation.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(924, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "Upload Map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MapFileName
            // 
            this.MapFileName.Location = new System.Drawing.Point(647, 30);
            this.MapFileName.Name = "MapFileName";
            this.MapFileName.Size = new System.Drawing.Size(231, 22);
            this.MapFileName.TabIndex = 5;
            this.MapFileName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(658, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter File Name";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // RefineCheckBox
            // 
            this.RefineCheckBox.AutoSize = true;
            this.RefineCheckBox.Location = new System.Drawing.Point(941, 22);
            this.RefineCheckBox.Name = "RefineCheckBox";
            this.RefineCheckBox.Size = new System.Drawing.Size(104, 20);
            this.RefineCheckBox.TabIndex = 3;
            this.RefineCheckBox.Text = "Refine Mesh";
            this.RefineCheckBox.UseVisualStyleBackColor = true;
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 832);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MapFileName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LabelXYLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_randommap);
            this.Controls.Add(this.MainPic);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Display";
            this.Text = "Random Map Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.MainPic.ResumeLayout(false);
            this.MainPic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtPic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPic;
        private System.Windows.Forms.Button button_randommap;
        private System.Windows.Forms.PictureBox mtPic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelXYLocation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox MapFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox triangleSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox RefineCheckBox;
    }
}

