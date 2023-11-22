namespace SeaBattleNew
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelMessage = new System.Windows.Forms.Label();
            this.pictureBoxLinkor = new System.Windows.Forms.PictureBox();
            this.pictureBoxKreiser = new System.Windows.Forms.PictureBox();
            this.pictureBoxEsminec = new System.Windows.Forms.PictureBox();
            this.pictureBoxKater = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLinkor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKreiser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEsminec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKater)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SeaBattleNew.Properties.Resources.Fields;
            this.pictureBox1.Location = new System.Drawing.Point(41, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(401, 401);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::SeaBattleNew.Properties.Resources.Fields;
            this.pictureBox2.Enabled = false;
            this.pictureBox2.Location = new System.Drawing.Point(589, 84);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(401, 401);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(175, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 55);
            this.label1.TabIndex = 2;
            this.label1.Text = "Свои:";
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(712, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 55);
            this.label2.TabIndex = 3;
            this.label2.Text = "Вражьи:";
            this.label2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.label2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // labelMessage
            // 
            this.labelMessage.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.labelMessage.Location = new System.Drawing.Point(120, 497);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(794, 38);
            this.labelMessage.TabIndex = 4;
            this.labelMessage.Text = "Расставь свои корабли";
            this.labelMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMessage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.labelMessage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // pictureBoxLinkor
            // 
            this.pictureBoxLinkor.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxLinkor.BackgroundImage = global::SeaBattleNew.Properties.Resources.LinkorVert;
            this.pictureBoxLinkor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxLinkor.Location = new System.Drawing.Point(500, 105);
            this.pictureBoxLinkor.Name = "pictureBoxLinkor";
            this.pictureBoxLinkor.Size = new System.Drawing.Size(31, 121);
            this.pictureBoxLinkor.TabIndex = 6;
            this.pictureBoxLinkor.TabStop = false;
            this.pictureBoxLinkor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLinkor_MouseDown);
            this.pictureBoxLinkor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBoxLinkor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLinkor_MouseUp);
            // 
            // pictureBoxKreiser
            // 
            this.pictureBoxKreiser.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxKreiser.BackgroundImage = global::SeaBattleNew.Properties.Resources.KreiserVert;
            this.pictureBoxKreiser.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxKreiser.Location = new System.Drawing.Point(500, 241);
            this.pictureBoxKreiser.Name = "pictureBoxKreiser";
            this.pictureBoxKreiser.Size = new System.Drawing.Size(31, 91);
            this.pictureBoxKreiser.TabIndex = 7;
            this.pictureBoxKreiser.TabStop = false;
            this.pictureBoxKreiser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxKreiser_MouseDown);
            this.pictureBoxKreiser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBoxKreiser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxKreiser_MouseUp);
            // 
            // pictureBoxEsminec
            // 
            this.pictureBoxEsminec.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxEsminec.BackgroundImage = global::SeaBattleNew.Properties.Resources.EsminecVert;
            this.pictureBoxEsminec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxEsminec.Location = new System.Drawing.Point(500, 347);
            this.pictureBoxEsminec.Name = "pictureBoxEsminec";
            this.pictureBoxEsminec.Size = new System.Drawing.Size(31, 61);
            this.pictureBoxEsminec.TabIndex = 8;
            this.pictureBoxEsminec.TabStop = false;
            this.pictureBoxEsminec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEsminec_MouseDown);
            this.pictureBoxEsminec.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBoxEsminec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxEsminec_MouseUp);
            // 
            // pictureBoxKater
            // 
            this.pictureBoxKater.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxKater.BackgroundImage = global::SeaBattleNew.Properties.Resources.Kater;
            this.pictureBoxKater.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxKater.Location = new System.Drawing.Point(500, 421);
            this.pictureBoxKater.Name = "pictureBoxKater";
            this.pictureBoxKater.Size = new System.Drawing.Size(31, 31);
            this.pictureBoxKater.TabIndex = 9;
            this.pictureBoxKater.TabStop = false;
            this.pictureBoxKater.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxKater_MouseDown);
            this.pictureBoxKater.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.pictureBoxKater.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxKater_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 555);
            this.Controls.Add(this.pictureBoxKater);
            this.Controls.Add(this.pictureBoxEsminec);
            this.Controls.Add(this.pictureBoxKreiser);
            this.Controls.Add(this.pictureBoxLinkor);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морской бой";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLinkor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKreiser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEsminec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKater)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private Label labelMessage;
        private PictureBox pictureBoxKreiser;
        private PictureBox pictureBoxEsminec;
        private PictureBox pictureBoxKater;
        public PictureBox pictureBoxLinkor;
    }
}