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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDrucken = new System.Windows.Forms.Button();
            this.listBoxKlasse = new System.Windows.Forms.ListBox();
            this.listBoxSchueler = new System.Windows.Forms.ListBox();
            this.btnAlle = new System.Windows.Forms.Button();
            this.lblStartup = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
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
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 346);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(376, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "Hallo";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // btnDrucken
            // 
            this.btnDrucken.Enabled = false;
            this.btnDrucken.Location = new System.Drawing.Point(12, 230);
            this.btnDrucken.Name = "btnDrucken";
            this.btnDrucken.Size = new System.Drawing.Size(350, 101);
            this.btnDrucken.TabIndex = 21;
            this.btnDrucken.Text = "DRUCKEN";
            this.btnDrucken.UseVisualStyleBackColor = true;
            this.btnDrucken.Click += new System.EventHandler(this.BtnDrucken_Click);
            // 
            // listBoxKlasse
            // 
            this.listBoxKlasse.FormattingEnabled = true;
            this.listBoxKlasse.Location = new System.Drawing.Point(12, 12);
            this.listBoxKlasse.Name = "listBoxKlasse";
            this.listBoxKlasse.Size = new System.Drawing.Size(129, 212);
            this.listBoxKlasse.TabIndex = 24;
            this.listBoxKlasse.Click += new System.EventHandler(this.ListBoxKlasse_Click);
            // 
            // listBoxSchueler
            // 
            this.listBoxSchueler.FormattingEnabled = true;
            this.listBoxSchueler.Location = new System.Drawing.Point(147, 12);
            this.listBoxSchueler.Name = "listBoxSchueler";
            this.listBoxSchueler.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxSchueler.Size = new System.Drawing.Size(215, 186);
            this.listBoxSchueler.TabIndex = 25;
            this.listBoxSchueler.Click += new System.EventHandler(this.ListBoxSchueler_Click);
            // 
            // btnAlle
            // 
            this.btnAlle.Enabled = false;
            this.btnAlle.Location = new System.Drawing.Point(147, 201);
            this.btnAlle.Name = "btnAlle";
            this.btnAlle.Size = new System.Drawing.Size(215, 23);
            this.btnAlle.TabIndex = 26;
            this.btnAlle.Text = "Alle auswählen";
            this.btnAlle.UseVisualStyleBackColor = true;
            this.btnAlle.Click += new System.EventHandler(this.BtnAlle_Click);
            // 
            // lblStartup
            // 
            this.lblStartup.BackColor = System.Drawing.Color.Tomato;
            this.lblStartup.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartup.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblStartup.Location = new System.Drawing.Point(12, 9);
            this.lblStartup.Name = "lblStartup";
            this.lblStartup.Size = new System.Drawing.Size(350, 322);
            this.lblStartup.TabIndex = 27;
            this.lblStartup.Text = "Daten werden geladen\r\nBitte warten ...";
            this.lblStartup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStartup.Click += new System.EventHandler(this.lblStartup_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 368);
            this.Controls.Add(this.lblStartup);
            this.Controls.Add(this.btnAlle);
            this.Controls.Add(this.listBoxSchueler);
            this.Controls.Add(this.listBoxKlasse);
            this.Controls.Add(this.btnDrucken);
            this.Controls.Add(this.statusStrip1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(392, 407);
            this.MinimumSize = new System.Drawing.Size(392, 407);
            this.Name = "Form1";
            this.Text = "Schülerausweise drucken";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripStatusLabel tsFooter;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button btnDrucken;
        private System.Windows.Forms.ListBox listBoxKlasse;
        private System.Windows.Forms.ListBox listBoxSchueler;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnAlle;
        private System.Windows.Forms.Label lblStartup;
    }
}

