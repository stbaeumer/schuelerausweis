namespace schuelerausweis
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tsFooter = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsFoot = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbxKlasse = new System.Windows.Forms.TextBox();
            this.tbxNachname = new System.Windows.Forms.TextBox();
            this.tbxVorname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGeburtsdatum = new System.Windows.Forms.Label();
            this.btnDrucken = new System.Windows.Forms.Button();
            this.pbxSchueler = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSchueler)).BeginInit();
            this.SuspendLayout();
            // 
            // tsFooter
            // 
            this.tsFooter.Name = "tsFooter";
            this.tsFooter.Size = new System.Drawing.Size(23, 23);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFoot});
            this.statusStrip1.Location = new System.Drawing.Point(0, 287);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(389, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsFoot
            // 
            this.tsFoot.Image = global::schuelerausweis.Properties.Resources.pbxDbConnectFailed1;
            this.tsFoot.Name = "tsFoot";
            this.tsFoot.Size = new System.Drawing.Size(16, 17);
            // 
            // tbxKlasse
            // 
            this.tbxKlasse.Location = new System.Drawing.Point(90, 18);
            this.tbxKlasse.Name = "tbxKlasse";
            this.tbxKlasse.Size = new System.Drawing.Size(155, 20);
            this.tbxKlasse.TabIndex = 13;
            this.tbxKlasse.TextChanged += new System.EventHandler(this.TbxKlasse_TextChanged);
            // 
            // tbxNachname
            // 
            this.tbxNachname.Location = new System.Drawing.Point(90, 44);
            this.tbxNachname.Name = "tbxNachname";
            this.tbxNachname.Size = new System.Drawing.Size(155, 20);
            this.tbxNachname.TabIndex = 14;
            this.tbxNachname.TextChanged += new System.EventHandler(this.TbxNachname_TextChanged);
            // 
            // tbxVorname
            // 
            this.tbxVorname.Location = new System.Drawing.Point(90, 70);
            this.tbxVorname.Name = "tbxVorname";
            this.tbxVorname.Size = new System.Drawing.Size(155, 20);
            this.tbxVorname.TabIndex = 15;
            this.tbxVorname.TextChanged += new System.EventHandler(this.TbxVorname_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Klasse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Nachname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Vorname";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Geburtsdatum";
            // 
            // lblGeburtsdatum
            // 
            this.lblGeburtsdatum.AutoSize = true;
            this.lblGeburtsdatum.Location = new System.Drawing.Point(94, 104);
            this.lblGeburtsdatum.Name = "lblGeburtsdatum";
            this.lblGeburtsdatum.Size = new System.Drawing.Size(0, 13);
            this.lblGeburtsdatum.TabIndex = 20;
            // 
            // btnDrucken
            // 
            this.btnDrucken.Location = new System.Drawing.Point(15, 134);
            this.btnDrucken.Name = "btnDrucken";
            this.btnDrucken.Size = new System.Drawing.Size(350, 138);
            this.btnDrucken.TabIndex = 21;
            this.btnDrucken.Text = "DRUCKEN";
            this.btnDrucken.UseVisualStyleBackColor = true;
            this.btnDrucken.Click += new System.EventHandler(this.BtnDrucken_Click);
            // 
            // pbxSchueler
            // 
            this.pbxSchueler.Location = new System.Drawing.Point(264, 18);
            this.pbxSchueler.Name = "pbxSchueler";
            this.pbxSchueler.Size = new System.Drawing.Size(101, 99);
            this.pbxSchueler.TabIndex = 23;
            this.pbxSchueler.TabStop = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 309);
            this.Controls.Add(this.pbxSchueler);
            this.Controls.Add(this.btnDrucken);
            this.Controls.Add(this.lblGeburtsdatum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxVorname);
            this.Controls.Add(this.tbxNachname);
            this.Controls.Add(this.tbxKlasse);
            this.Controls.Add(this.statusStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Schülerausweis";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSchueler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripStatusLabel tsFooter;
        private System.Windows.Forms.ToolStripStatusLabel tsFoot;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TextBox tbxKlasse;
        private System.Windows.Forms.TextBox tbxNachname;
        private System.Windows.Forms.TextBox tbxVorname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGeburtsdatum;
        private System.Windows.Forms.Button btnDrucken;
        private System.Windows.Forms.PictureBox pbxSchueler;
    }
}

