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
            this.GenericAspectRatio = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.DTAAspectRatioValue = new System.Windows.Forms.Label();
            this.GTAAspectRatioLbl = new System.Windows.Forms.Label();
            this.NoOfItersValue = new System.Windows.Forms.Label();
            this.GeneticlblValue = new System.Windows.Forms.Label();
            this.DtaTotal = new System.Windows.Forms.Label();
            this.itersLbl = new System.Windows.Forms.Label();
            this.GeneticLbl = new System.Windows.Forms.Label();
            this.dta = new System.Windows.Forms.Label();
            this.RefineCheckBox = new System.Windows.Forms.CheckBox();
            this.triangleSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtPic = new System.Windows.Forms.PictureBox();
            this.button_randommap = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelXYLocation = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.MapFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fullMesh = new System.Windows.Forms.Button();
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
            this.MainPic.Controls.Add(this.fullMesh);
            this.MainPic.Controls.Add(this.GenericAspectRatio);
            this.MainPic.Controls.Add(this.label6);
            this.MainPic.Controls.Add(this.DTAAspectRatioValue);
            this.MainPic.Controls.Add(this.GTAAspectRatioLbl);
            this.MainPic.Controls.Add(this.NoOfItersValue);
            this.MainPic.Controls.Add(this.GeneticlblValue);
            this.MainPic.Controls.Add(this.DtaTotal);
            this.MainPic.Controls.Add(this.itersLbl);
            this.MainPic.Controls.Add(this.GeneticLbl);
            this.MainPic.Controls.Add(this.dta);
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
            // GenericAspectRatio
            // 
            this.GenericAspectRatio.AutoSize = true;
            this.GenericAspectRatio.Location = new System.Drawing.Point(857, 62);
            this.GenericAspectRatio.Name = "GenericAspectRatio";
            this.GenericAspectRatio.Size = new System.Drawing.Size(14, 16);
            this.GenericAspectRatio.TabIndex = 13;
            this.GenericAspectRatio.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(704, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Genetic Aspect Ratio:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // DTAAspectRatioValue
            // 
            this.DTAAspectRatioValue.AutoSize = true;
            this.DTAAspectRatioValue.Location = new System.Drawing.Point(537, 61);
            this.DTAAspectRatioValue.Name = "DTAAspectRatioValue";
            this.DTAAspectRatioValue.Size = new System.Drawing.Size(14, 16);
            this.DTAAspectRatioValue.TabIndex = 11;
            this.DTAAspectRatioValue.Text = "0";
            this.DTAAspectRatioValue.Click += new System.EventHandler(this.label5_Click);
            // 
            // GTAAspectRatioLbl
            // 
            this.GTAAspectRatioLbl.AutoSize = true;
            this.GTAAspectRatioLbl.Location = new System.Drawing.Point(398, 62);
            this.GTAAspectRatioLbl.Name = "GTAAspectRatioLbl";
            this.GTAAspectRatioLbl.Size = new System.Drawing.Size(118, 16);
            this.GTAAspectRatioLbl.TabIndex = 10;
            this.GTAAspectRatioLbl.Text = "GTA Aspect Ratio:";
            // 
            // NoOfItersValue
            // 
            this.NoOfItersValue.AutoSize = true;
            this.NoOfItersValue.Location = new System.Drawing.Point(955, 22);
            this.NoOfItersValue.Name = "NoOfItersValue";
            this.NoOfItersValue.Size = new System.Drawing.Size(14, 16);
            this.NoOfItersValue.TabIndex = 9;
            this.NoOfItersValue.Text = "0";
            // 
            // GeneticlblValue
            // 
            this.GeneticlblValue.AutoSize = true;
            this.GeneticlblValue.Location = new System.Drawing.Point(797, 23);
            this.GeneticlblValue.Name = "GeneticlblValue";
            this.GeneticlblValue.Size = new System.Drawing.Size(14, 16);
            this.GeneticlblValue.TabIndex = 8;
            this.GeneticlblValue.Text = "0";
            // 
            // DtaTotal
            // 
            this.DtaTotal.AutoSize = true;
            this.DtaTotal.Location = new System.Drawing.Point(626, 19);
            this.DtaTotal.Name = "DtaTotal";
            this.DtaTotal.Size = new System.Drawing.Size(14, 16);
            this.DtaTotal.TabIndex = 7;
            this.DtaTotal.Text = "0";
            this.DtaTotal.Click += new System.EventHandler(this.dtaTotal_Click);
            // 
            // itersLbl
            // 
            this.itersLbl.AutoSize = true;
            this.itersLbl.Location = new System.Drawing.Point(877, 23);
            this.itersLbl.Name = "itersLbl";
            this.itersLbl.Size = new System.Drawing.Size(72, 16);
            this.itersLbl.TabIndex = 6;
            this.itersLbl.Text = "No Of Iters:";
            // 
            // GeneticLbl
            // 
            this.GeneticLbl.AutoSize = true;
            this.GeneticLbl.Location = new System.Drawing.Point(701, 23);
            this.GeneticLbl.Name = "GeneticLbl";
            this.GeneticLbl.Size = new System.Drawing.Size(90, 16);
            this.GeneticLbl.TabIndex = 5;
            this.GeneticLbl.Text = "Genetic Total:";
            // 
            // dta
            // 
            this.dta.AutoSize = true;
            this.dta.Location = new System.Drawing.Point(545, 19);
            this.dta.Name = "dta";
            this.dta.Size = new System.Drawing.Size(75, 16);
            this.dta.TabIndex = 4;
            this.dta.Text = "DTA Total: ";
            // 
            // RefineCheckBox
            // 
            this.RefineCheckBox.AutoSize = true;
            this.RefineCheckBox.Location = new System.Drawing.Point(398, 19);
            this.RefineCheckBox.Name = "RefineCheckBox";
            this.RefineCheckBox.Size = new System.Drawing.Size(104, 20);
            this.RefineCheckBox.TabIndex = 3;
            this.RefineCheckBox.Text = "Refine Mesh";
            this.RefineCheckBox.UseVisualStyleBackColor = true;
            this.RefineCheckBox.CheckedChanged += new System.EventHandler(this.RefineCheckBox_CheckedChanged);
            // 
            // triangleSize
            // 
            this.triangleSize.Location = new System.Drawing.Point(265, 16);
            this.triangleSize.Name = "triangleSize";
            this.triangleSize.Size = new System.Drawing.Size(106, 22);
            this.triangleSize.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 19);
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
            // fullMesh
            // 
            this.fullMesh.Location = new System.Drawing.Point(60, 47);
            this.fullMesh.Name = "fullMesh";
            this.fullMesh.Size = new System.Drawing.Size(115, 44);
            this.fullMesh.TabIndex = 14;
            this.fullMesh.Text = "Full Mesh";
            this.fullMesh.UseVisualStyleBackColor = true;
            this.fullMesh.Click += new System.EventHandler(this.fullMesh_Click);
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
        private System.Windows.Forms.Label NoOfItersValue;
        private System.Windows.Forms.Label GeneticlblValue;
        private System.Windows.Forms.Label DtaTotal;
        private System.Windows.Forms.Label itersLbl;
        private System.Windows.Forms.Label GeneticLbl;
        private System.Windows.Forms.Label dta;
        private System.Windows.Forms.Label GenericAspectRatio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label DTAAspectRatioValue;
        private System.Windows.Forms.Label GTAAspectRatioLbl;
        private System.Windows.Forms.Button fullMesh;
    }
}

