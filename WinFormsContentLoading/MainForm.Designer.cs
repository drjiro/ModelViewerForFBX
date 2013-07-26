namespace WinFormsContentLoading
{
    partial class MainForm
    {
        /// <summary>
        /// 必須のデザイナー変数。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用されているすべてのリソースをクリーン アップします。
        /// </summary>
        /// <param name="disposing">true (マネージ リソースを破棄する場合)、false (それ以外の場合)。</param>
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
        /// デザイナーのサポートに必要なメソッド - このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.farCliptextBox = new System.Windows.Forms.TextBox();
            this.nearCliptextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.aspecttextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.fovtextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.eyeUpZtextBox = new System.Windows.Forms.TextBox();
            this.eyeUpYtextBox = new System.Windows.Forms.TextBox();
            this.eyeUpXtextBox = new System.Windows.Forms.TextBox();
            this.eyeAtZtextBox = new System.Windows.Forms.TextBox();
            this.eyeAtYtextBox = new System.Windows.Forms.TextBox();
            this.eyeAtXtextBox = new System.Windows.Forms.TextBox();
            this.eyeZtextBox = new System.Windows.Forms.TextBox();
            this.eyeYtextBox = new System.Windows.Forms.TextBox();
            this.eyeXtextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.modelViewerControl = new WinFormsContentLoading.ModelViewerControl();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(85, 22);
            this.fileToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.openToolStripMenuItem.Text = "モデルを開く(&O)...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuClicked);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exitToolStripMenuItem.Text = "終了(&X)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.farCliptextBox);
            this.groupBox1.Controls.Add(this.nearCliptextBox);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.aspecttextBox);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.fovtextBox);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.eyeUpZtextBox);
            this.groupBox1.Controls.Add(this.eyeUpYtextBox);
            this.groupBox1.Controls.Add(this.eyeUpXtextBox);
            this.groupBox1.Controls.Add(this.eyeAtZtextBox);
            this.groupBox1.Controls.Add(this.eyeAtYtextBox);
            this.groupBox1.Controls.Add(this.eyeAtXtextBox);
            this.groupBox1.Controls.Add(this.eyeZtextBox);
            this.groupBox1.Controls.Add(this.eyeYtextBox);
            this.groupBox1.Controls.Add(this.eyeXtextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(530, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 278);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "カメラ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "～";
            // 
            // farCliptextBox
            // 
            this.farCliptextBox.Location = new System.Drawing.Point(204, 231);
            this.farCliptextBox.MaxLength = 6;
            this.farCliptextBox.Name = "farCliptextBox";
            this.farCliptextBox.Size = new System.Drawing.Size(45, 19);
            this.farCliptextBox.TabIndex = 12;
            this.farCliptextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nearCliptextBox
            // 
            this.nearCliptextBox.Location = new System.Drawing.Point(95, 228);
            this.nearCliptextBox.MaxLength = 6;
            this.nearCliptextBox.Name = "nearCliptextBox";
            this.nearCliptextBox.Size = new System.Drawing.Size(45, 19);
            this.nearCliptextBox.TabIndex = 11;
            this.nearCliptextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nearCliptextBox.TextChanged += new System.EventHandler(this.textBox15_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 12);
            this.label10.TabIndex = 39;
            this.label10.Text = "クリップ面";
            // 
            // aspecttextBox
            // 
            this.aspecttextBox.Location = new System.Drawing.Point(95, 197);
            this.aspecttextBox.MaxLength = 4;
            this.aspecttextBox.Name = "aspecttextBox";
            this.aspecttextBox.Size = new System.Drawing.Size(45, 19);
            this.aspecttextBox.TabIndex = 10;
            this.aspecttextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.aspecttextBox.TextChanged += new System.EventHandler(this.textBox16_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 200);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 12);
            this.label11.TabIndex = 37;
            this.label11.Text = "アスペクト比";
            // 
            // fovtextBox
            // 
            this.fovtextBox.Location = new System.Drawing.Point(95, 163);
            this.fovtextBox.MaxLength = 4;
            this.fovtextBox.Name = "fovtextBox";
            this.fovtextBox.Size = new System.Drawing.Size(45, 19);
            this.fovtextBox.TabIndex = 9;
            this.fovtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 12);
            this.label12.TabIndex = 35;
            this.label12.Text = "FOV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(213, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(158, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 33;
            this.label6.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "X";
            // 
            // eyeUpZtextBox
            // 
            this.eyeUpZtextBox.Location = new System.Drawing.Point(204, 127);
            this.eyeUpZtextBox.MaxLength = 3;
            this.eyeUpZtextBox.Name = "eyeUpZtextBox";
            this.eyeUpZtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeUpZtextBox.TabIndex = 8;
            this.eyeUpZtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eyeUpZtextBox.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            // 
            // eyeUpYtextBox
            // 
            this.eyeUpYtextBox.Location = new System.Drawing.Point(151, 127);
            this.eyeUpYtextBox.MaxLength = 3;
            this.eyeUpYtextBox.Name = "eyeUpYtextBox";
            this.eyeUpYtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeUpYtextBox.TabIndex = 7;
            this.eyeUpYtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // eyeUpXtextBox
            // 
            this.eyeUpXtextBox.Location = new System.Drawing.Point(95, 127);
            this.eyeUpXtextBox.MaxLength = 3;
            this.eyeUpXtextBox.Name = "eyeUpXtextBox";
            this.eyeUpXtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeUpXtextBox.TabIndex = 6;
            this.eyeUpXtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.eyeUpXtextBox.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // eyeAtZtextBox
            // 
            this.eyeAtZtextBox.Location = new System.Drawing.Point(204, 94);
            this.eyeAtZtextBox.MaxLength = 3;
            this.eyeAtZtextBox.Name = "eyeAtZtextBox";
            this.eyeAtZtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeAtZtextBox.TabIndex = 5;
            this.eyeAtZtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // eyeAtYtextBox
            // 
            this.eyeAtYtextBox.Location = new System.Drawing.Point(151, 94);
            this.eyeAtYtextBox.MaxLength = 3;
            this.eyeAtYtextBox.Name = "eyeAtYtextBox";
            this.eyeAtYtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeAtYtextBox.TabIndex = 4;
            this.eyeAtYtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // eyeAtXtextBox
            // 
            this.eyeAtXtextBox.Location = new System.Drawing.Point(95, 91);
            this.eyeAtXtextBox.MaxLength = 3;
            this.eyeAtXtextBox.Name = "eyeAtXtextBox";
            this.eyeAtXtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeAtXtextBox.TabIndex = 3;
            this.eyeAtXtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // eyeZtextBox
            // 
            this.eyeZtextBox.Location = new System.Drawing.Point(204, 60);
            this.eyeZtextBox.MaxLength = 3;
            this.eyeZtextBox.Name = "eyeZtextBox";
            this.eyeZtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeZtextBox.TabIndex = 2;
            this.eyeZtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // eyeYtextBox
            // 
            this.eyeYtextBox.Location = new System.Drawing.Point(151, 60);
            this.eyeYtextBox.MaxLength = 3;
            this.eyeYtextBox.Name = "eyeYtextBox";
            this.eyeYtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeYtextBox.TabIndex = 1;
            this.eyeYtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // eyeXtextBox
            // 
            this.eyeXtextBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.eyeXtextBox.Location = new System.Drawing.Point(95, 60);
            this.eyeXtextBox.MaxLength = 3;
            this.eyeXtextBox.Name = "eyeXtextBox";
            this.eyeXtextBox.Size = new System.Drawing.Size(45, 19);
            this.eyeXtextBox.TabIndex = 0;
            this.eyeXtextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 22;
            this.label4.Text = "上向くベクトル";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 21;
            this.label3.Text = "対象点";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "視点";
            // 
            // modelViewerControl
            // 
            this.modelViewerControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.modelViewerControl.Location = new System.Drawing.Point(12, 42);
            this.modelViewerControl.Model = null;
            this.modelViewerControl.Name = "modelViewerControl";
            this.modelViewerControl.Size = new System.Drawing.Size(512, 512);
            this.modelViewerControl.TabIndex = 1;
            this.modelViewerControl.TabStop = false;
            this.modelViewerControl.Text = "modelViewerControl";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.modelViewerControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Model Viewer for FBX/X Ver. 0.1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private ModelViewerControl modelViewerControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox farCliptextBox;
        private System.Windows.Forms.TextBox nearCliptextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox aspecttextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox fovtextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox eyeUpZtextBox;
        private System.Windows.Forms.TextBox eyeUpYtextBox;
        private System.Windows.Forms.TextBox eyeUpXtextBox;
        private System.Windows.Forms.TextBox eyeAtZtextBox;
        private System.Windows.Forms.TextBox eyeAtYtextBox;
        private System.Windows.Forms.TextBox eyeAtXtextBox;
        private System.Windows.Forms.TextBox eyeZtextBox;
        private System.Windows.Forms.TextBox eyeYtextBox;
        private System.Windows.Forms.TextBox eyeXtextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}

