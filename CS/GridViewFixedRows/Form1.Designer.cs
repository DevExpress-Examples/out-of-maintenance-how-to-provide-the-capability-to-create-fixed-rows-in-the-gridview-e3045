namespace GVFixedRows
{
    partial class Form1
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
            this.gridControlFixedRows1 = new GVFixedRows.GridControlFixedRows();
            this.gridViewFixedRows1 = new GVFixedRows.GridViewFixedRows();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFixedRows1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFixedRows1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlFixedRows1
            // 
            this.gridControlFixedRows1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlFixedRows1.Location = new System.Drawing.Point(0, 0);
            this.gridControlFixedRows1.MainView = this.gridViewFixedRows1;
            this.gridControlFixedRows1.Name = "gridControlFixedRows1";
            this.gridControlFixedRows1.Size = new System.Drawing.Size(284, 186);
            this.gridControlFixedRows1.TabIndex = 0;
            this.gridControlFixedRows1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFixedRows1});
            // 
            // gridViewFixedRows1
            // 
            this.gridViewFixedRows1.GridControl = this.gridControlFixedRows1;
            this.gridViewFixedRows1.Name = "gridViewFixedRows1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 186);
            this.Controls.Add(this.gridControlFixedRows1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlFixedRows1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewFixedRows1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GridControlFixedRows gridControlFixedRows1;
        private GridViewFixedRows gridViewFixedRows1;


    }
}

