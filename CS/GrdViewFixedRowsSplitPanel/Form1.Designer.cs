// Developer Express Code Central Example:
// How to provide the capability to create fixed rows in the GridView
// 
// The current example illustrates how to implement a functionality for creating
// fixed rows, which will be displayed on top of the GridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3045

namespace GridViewFixedRowsSplitPanel {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.gridSplitContainer1 = new GridViewFixedRowsSplitPanel.GridSplitContainerDescendant();
            this.gridSplitContainerDescendant1Grid = new DevExpress.XtraGrid.GridControl();
            this.gridSplitContainerDescendant1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainerDescendant1Grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainerDescendant1View)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSplitContainer1.Grid = this.gridSplitContainerDescendant1Grid;
            this.gridSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Panel1.Controls.Add(this.gridSplitContainerDescendant1Grid);
            this.gridSplitContainer1.Panel1.Text = "Panel1";
            this.gridSplitContainer1.Panel2.Text = "Panel2";
            this.gridSplitContainer1.Size = new System.Drawing.Size(705, 394);
            this.gridSplitContainer1.TabIndex = 0;
            this.gridSplitContainer1.Text = "gridSplitContainerDescendant1";
            // 
            // gridSplitContainerDescendant1Grid
            // 
            this.gridSplitContainerDescendant1Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSplitContainerDescendant1Grid.Location = new System.Drawing.Point(0, 0);
            this.gridSplitContainerDescendant1Grid.MainView = this.gridSplitContainerDescendant1View;
            this.gridSplitContainerDescendant1Grid.Name = "gridSplitContainerDescendant1Grid";
            this.gridSplitContainerDescendant1Grid.Size = new System.Drawing.Size(705, 394);
            this.gridSplitContainerDescendant1Grid.TabIndex = 0;
            this.gridSplitContainerDescendant1Grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridSplitContainerDescendant1View});
            // 
            // gridSplitContainerDescendant1View
            // 
            this.gridSplitContainerDescendant1View.GridControl = this.gridSplitContainerDescendant1Grid;
            this.gridSplitContainerDescendant1View.Name = "gridSplitContainerDescendant1View";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 394);
            this.Controls.Add(this.gridSplitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainerDescendant1Grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainerDescendant1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GridSplitContainerDescendant gridSplitContainer1;
        private DevExpress.XtraGrid.GridControl gridSplitContainerDescendant1Grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSplitContainerDescendant1View;





    }
}

