using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace MapEditor.src {
    class Editor : Form {
        private PictureBox MapView;
        private HScrollBar HScroll;
        private VScrollBar VScroll;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ColorDialog colorDialog1;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private TabControl ToolTab;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private ToolStrip Toolbar;
        private ToolStripButton tsbNew;
        private ToolStripButton tsbSave;
        private ToolStripButton tsbSaveAs;
        private ToolStripButton tsbOpen;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbUndo;
        private ToolStripButton tsbRedo;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbLayer1;
        private ToolStripButton tsbLayer2;
        private ToolStripButton tsbLayer3;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsbBrush;
        private ToolStripButton tsbFill;
        private ToolStripButton tsbLine;
        private ToolStripButton tsbRect;
        private ToolStripButton tsbCircle;
        private ToolStripButton tsbErase;
        private PictureBox TileBox;
        private VScrollBar TileScroll;
    
        static void Main() {
            Application.EnableVisualStyles();
            Application.Run(new Editor());
        }

        public Editor() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveAs = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUndo = new System.Windows.Forms.ToolStripButton();
            this.tsbRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLayer1 = new System.Windows.Forms.ToolStripButton();
            this.tsbLayer2 = new System.Windows.Forms.ToolStripButton();
            this.tsbLayer3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbErase = new System.Windows.Forms.ToolStripButton();
            this.tsbBrush = new System.Windows.Forms.ToolStripButton();
            this.tsbFill = new System.Windows.Forms.ToolStripButton();
            this.tsbLine = new System.Windows.Forms.ToolStripButton();
            this.tsbRect = new System.Windows.Forms.ToolStripButton();
            this.tsbCircle = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.HScroll = new System.Windows.Forms.HScrollBar();
            this.VScroll = new System.Windows.Forms.VScrollBar();
            this.ToolTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TileBox = new System.Windows.Forms.PictureBox();
            this.TileScroll = new System.Windows.Forms.VScrollBar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MapView = new System.Windows.Forms.PictureBox();
            this.Toolbar.SuspendLayout();
            this.ToolTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapView)).BeginInit();
            this.SuspendLayout();
            // 
            // Toolbar
            // 
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbSave,
            this.tsbSaveAs,
            this.tsbOpen,
            this.toolStripSeparator1,
            this.tsbUndo,
            this.tsbRedo,
            this.toolStripSeparator2,
            this.tsbLayer1,
            this.tsbLayer2,
            this.tsbLayer3,
            this.toolStripSeparator3,
            this.tsbErase,
            this.tsbBrush,
            this.tsbFill,
            this.tsbLine,
            this.tsbRect,
            this.tsbCircle});
            this.Toolbar.Location = new System.Drawing.Point(0, 0);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Size = new System.Drawing.Size(868, 25);
            this.Toolbar.TabIndex = 1;
            this.Toolbar.Text = "toolStrip1";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = ((System.Drawing.Image)(resources.GetObject("tsbNew.Image")));
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "New Map";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save";
            // 
            // tsbSaveAs
            // 
            this.tsbSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("tsbSaveAs.Image")));
            this.tsbSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveAs.Name = "tsbSaveAs";
            this.tsbSaveAs.Size = new System.Drawing.Size(23, 22);
            this.tsbSaveAs.Text = "Save As...";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbUndo
            // 
            this.tsbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsbUndo.Image")));
            this.tsbUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUndo.Name = "tsbUndo";
            this.tsbUndo.Size = new System.Drawing.Size(23, 22);
            this.tsbUndo.Text = "Undo";
            // 
            // tsbRedo
            // 
            this.tsbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsbRedo.Image")));
            this.tsbRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRedo.Name = "tsbRedo";
            this.tsbRedo.Size = new System.Drawing.Size(23, 22);
            this.tsbRedo.Text = "Redo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLayer1
            // 
            this.tsbLayer1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLayer1.Image = ((System.Drawing.Image)(resources.GetObject("tsbLayer1.Image")));
            this.tsbLayer1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLayer1.Name = "tsbLayer1";
            this.tsbLayer1.Size = new System.Drawing.Size(23, 22);
            this.tsbLayer1.Text = "Layer 1";
            // 
            // tsbLayer2
            // 
            this.tsbLayer2.Checked = true;
            this.tsbLayer2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbLayer2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLayer2.Image = ((System.Drawing.Image)(resources.GetObject("tsbLayer2.Image")));
            this.tsbLayer2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLayer2.Name = "tsbLayer2";
            this.tsbLayer2.Size = new System.Drawing.Size(23, 22);
            this.tsbLayer2.Text = "Layer 2";
            // 
            // tsbLayer3
            // 
            this.tsbLayer3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLayer3.Image = ((System.Drawing.Image)(resources.GetObject("tsbLayer3.Image")));
            this.tsbLayer3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLayer3.Name = "tsbLayer3";
            this.tsbLayer3.Size = new System.Drawing.Size(23, 22);
            this.tsbLayer3.Text = "Layer 3";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbErase
            // 
            this.tsbErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbErase.Image = ((System.Drawing.Image)(resources.GetObject("tsbErase.Image")));
            this.tsbErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbErase.Name = "tsbErase";
            this.tsbErase.Size = new System.Drawing.Size(23, 22);
            this.tsbErase.Text = "Erase";
            // 
            // tsbBrush
            // 
            this.tsbBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBrush.Image = ((System.Drawing.Image)(resources.GetObject("tsbBrush.Image")));
            this.tsbBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBrush.Name = "tsbBrush";
            this.tsbBrush.Size = new System.Drawing.Size(23, 22);
            this.tsbBrush.Text = "Brush";
            // 
            // tsbFill
            // 
            this.tsbFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFill.Image = ((System.Drawing.Image)(resources.GetObject("tsbFill.Image")));
            this.tsbFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFill.Name = "tsbFill";
            this.tsbFill.Size = new System.Drawing.Size(23, 22);
            this.tsbFill.Text = "Fill";
            // 
            // tsbLine
            // 
            this.tsbLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbLine.Image")));
            this.tsbLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLine.Name = "tsbLine";
            this.tsbLine.Size = new System.Drawing.Size(23, 22);
            this.tsbLine.Text = "Line";
            // 
            // tsbRect
            // 
            this.tsbRect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRect.Image = ((System.Drawing.Image)(resources.GetObject("tsbRect.Image")));
            this.tsbRect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRect.Name = "tsbRect";
            this.tsbRect.Size = new System.Drawing.Size(23, 22);
            this.tsbRect.Text = "Rectangle";
            // 
            // tsbCircle
            // 
            this.tsbCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCircle.Image = ((System.Drawing.Image)(resources.GetObject("tsbCircle.Image")));
            this.tsbCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCircle.Name = "tsbCircle";
            this.tsbCircle.Size = new System.Drawing.Size(23, 22);
            this.tsbCircle.Text = "Ellipse";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // HScroll
            // 
            this.HScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HScroll.Enabled = false;
            this.HScroll.LargeChange = 1;
            this.HScroll.Location = new System.Drawing.Point(290, 567);
            this.HScroll.Maximum = 0;
            this.HScroll.Name = "HScroll";
            this.HScroll.Size = new System.Drawing.Size(561, 17);
            this.HScroll.TabIndex = 2;
            // 
            // VScroll
            // 
            this.VScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.VScroll.Enabled = false;
            this.VScroll.LargeChange = 1;
            this.VScroll.Location = new System.Drawing.Point(851, 28);
            this.VScroll.Maximum = 0;
            this.VScroll.Name = "VScroll";
            this.VScroll.Size = new System.Drawing.Size(17, 539);
            this.VScroll.TabIndex = 3;
            // 
            // ToolTab
            // 
            this.ToolTab.Controls.Add(this.tabPage1);
            this.ToolTab.Controls.Add(this.tabPage2);
            this.ToolTab.Controls.Add(this.tabPage3);
            this.ToolTab.Dock = System.Windows.Forms.DockStyle.Left;
            this.ToolTab.ImageList = this.imageList1;
            this.ToolTab.Location = new System.Drawing.Point(0, 25);
            this.ToolTab.Name = "ToolTab";
            this.ToolTab.SelectedIndex = 0;
            this.ToolTab.Size = new System.Drawing.Size(288, 559);
            this.ToolTab.TabIndex = 4;
            this.ToolTab.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.TileBox);
            this.tabPage1.Controls.Add(this.TileScroll);
            this.tabPage1.ImageKey = "tilelist.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(280, 532);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tiles";
            // 
            // TileBox
            // 
            this.TileBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.TileBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.TileBox.BackgroundImage = global::MapEditor.Properties.Resources.grid1;
            this.TileBox.Location = new System.Drawing.Point(3, 3);
            this.TileBox.Name = "TileBox";
            this.TileBox.Size = new System.Drawing.Size(256, 526);
            this.TileBox.TabIndex = 0;
            this.TileBox.TabStop = false;
            // 
            // TileScroll
            // 
            this.TileScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TileScroll.Enabled = false;
            this.TileScroll.LargeChange = 1;
            this.TileScroll.Location = new System.Drawing.Point(260, 3);
            this.TileScroll.Maximum = 0;
            this.TileScroll.Name = "TileScroll";
            this.TileScroll.Size = new System.Drawing.Size(17, 526);
            this.TileScroll.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.ImageKey = "events.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(280, 532);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Events";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.ImageKey = "script_code.png";
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(280, 532);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Scripts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tilelist.png");
            this.imageList1.Images.SetKeyName(1, "events.png");
            this.imageList1.Images.SetKeyName(2, "script_code.png");
            // 
            // MapView
            // 
            this.MapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MapView.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MapView.BackgroundImage = global::MapEditor.Properties.Resources.grid1;
            this.MapView.Location = new System.Drawing.Point(290, 28);
            this.MapView.Name = "MapView";
            this.MapView.Size = new System.Drawing.Size(558, 536);
            this.MapView.TabIndex = 0;
            this.MapView.TabStop = false;
            // 
            // Editor
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(868, 584);
            this.Controls.Add(this.ToolTab);
            this.Controls.Add(this.VScroll);
            this.Controls.Add(this.HScroll);
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.MapView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(512, 400);
            this.Name = "Editor";
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.ToolTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TileBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MapView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
