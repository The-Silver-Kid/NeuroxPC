namespace NeuroxPC {
    partial class Window {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.data = new System.Windows.Forms.TextBox();
            this.index = new System.Windows.Forms.Button();
            this.cnewindex = new System.Windows.Forms.Button();
            this.newPage = new System.Windows.Forms.Button();
            this.loadPage = new System.Windows.Forms.Button();
            this.saveBox = new System.Windows.Forms.SaveFileDialog();
            this.pageNumber = new System.Windows.Forms.ComboBox();
            this.pageCount = new System.Windows.Forms.Label();
            this.debug = new System.Windows.Forms.Label();
            this.savePage = new System.Windows.Forms.Button();
            this.writeChanges = new System.Windows.Forms.Button();
            this.delPage = new System.Windows.Forms.Button();
            this.openBox = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // data
            // 
            this.data.AcceptsReturn = true;
            this.data.Enabled = false;
            this.data.Location = new System.Drawing.Point(12, 120);
            this.data.Multiline = true;
            this.data.Name = "data";
            this.data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.data.Size = new System.Drawing.Size(960, 529);
            this.data.TabIndex = 0;
            this.data.TextChanged += new System.EventHandler(this.data_TextChanged);
            // 
            // index
            // 
            this.index.Location = new System.Drawing.Point(12, 12);
            this.index.Name = "index";
            this.index.Size = new System.Drawing.Size(75, 30);
            this.index.TabIndex = 1;
            this.index.Text = "Find Index";
            this.index.UseVisualStyleBackColor = true;
            this.index.Click += new System.EventHandler(this.loadContent);
            // 
            // cnewindex
            // 
            this.cnewindex.Location = new System.Drawing.Point(93, 12);
            this.cnewindex.Name = "cnewindex";
            this.cnewindex.Size = new System.Drawing.Size(75, 30);
            this.cnewindex.TabIndex = 2;
            this.cnewindex.Text = "New Index";
            this.cnewindex.UseVisualStyleBackColor = true;
            this.cnewindex.Click += new System.EventHandler(this.createNewIndex);
            // 
            // newPage
            // 
            this.newPage.Enabled = false;
            this.newPage.Location = new System.Drawing.Point(12, 48);
            this.newPage.Name = "newPage";
            this.newPage.Size = new System.Drawing.Size(75, 30);
            this.newPage.TabIndex = 3;
            this.newPage.Text = "New Page";
            this.newPage.UseVisualStyleBackColor = true;
            this.newPage.Click += new System.EventHandler(this.cNewPage);
            // 
            // loadPage
            // 
            this.loadPage.Enabled = false;
            this.loadPage.Location = new System.Drawing.Point(93, 48);
            this.loadPage.Name = "loadPage";
            this.loadPage.Size = new System.Drawing.Size(75, 30);
            this.loadPage.TabIndex = 4;
            this.loadPage.Text = "Load Page";
            this.loadPage.UseVisualStyleBackColor = true;
            this.loadPage.Click += new System.EventHandler(this.lPage);
            // 
            // saveBox
            // 
            this.saveBox.DefaultExt = "NXD";
            this.saveBox.Filter = "Neurox Data Files|*.NXD|All Files|*.*";
            this.saveBox.FileOk += new System.ComponentModel.CancelEventHandler(this.createNewIndexConfirm);
            // 
            // pageNumber
            // 
            this.pageNumber.Enabled = false;
            this.pageNumber.FormattingEnabled = true;
            this.pageNumber.Location = new System.Drawing.Point(174, 57);
            this.pageNumber.Name = "pageNumber";
            this.pageNumber.Size = new System.Drawing.Size(75, 21);
            this.pageNumber.TabIndex = 5;
            // 
            // pageCount
            // 
            this.pageCount.Location = new System.Drawing.Point(174, 81);
            this.pageCount.Name = "pageCount";
            this.pageCount.Size = new System.Drawing.Size(164, 13);
            this.pageCount.TabIndex = 6;
            this.pageCount.Text = "label1";
            this.pageCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // debug
            // 
            this.debug.Location = new System.Drawing.Point(255, 15);
            this.debug.Name = "debug";
            this.debug.Size = new System.Drawing.Size(717, 63);
            this.debug.TabIndex = 7;
            this.debug.Text = "Nothing Loaded!";
            this.debug.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // savePage
            // 
            this.savePage.Enabled = false;
            this.savePage.Location = new System.Drawing.Point(12, 84);
            this.savePage.Name = "savePage";
            this.savePage.Size = new System.Drawing.Size(156, 30);
            this.savePage.TabIndex = 8;
            this.savePage.Text = "Save Changes";
            this.savePage.UseVisualStyleBackColor = true;
            this.savePage.Click += new System.EventHandler(this.saveLocal);
            // 
            // writeChanges
            // 
            this.writeChanges.Enabled = false;
            this.writeChanges.Location = new System.Drawing.Point(883, 84);
            this.writeChanges.Name = "writeChanges";
            this.writeChanges.Size = new System.Drawing.Size(89, 30);
            this.writeChanges.TabIndex = 9;
            this.writeChanges.Text = "Write to Disk";
            this.writeChanges.UseVisualStyleBackColor = true;
            this.writeChanges.Click += new System.EventHandler(this.writeChangesB);
            // 
            // delPage
            // 
            this.delPage.Enabled = false;
            this.delPage.Location = new System.Drawing.Point(174, 12);
            this.delPage.Name = "delPage";
            this.delPage.Size = new System.Drawing.Size(75, 30);
            this.delPage.TabIndex = 10;
            this.delPage.Text = "Delete Page";
            this.delPage.UseVisualStyleBackColor = true;
            this.delPage.Click += new System.EventHandler(this.delPageB);
            // 
            // openBox
            // 
            this.openBox.Filter = "Neurox Data Files|*.NXD|All Files|*.*";
            this.openBox.FileOk += new System.ComponentModel.CancelEventHandler(this.loadConfirm);
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.delPage);
            this.Controls.Add(this.writeChanges);
            this.Controls.Add(this.savePage);
            this.Controls.Add(this.debug);
            this.Controls.Add(this.pageCount);
            this.Controls.Add(this.pageNumber);
            this.Controls.Add(this.loadPage);
            this.Controls.Add(this.newPage);
            this.Controls.Add(this.cnewindex);
            this.Controls.Add(this.index);
            this.Controls.Add(this.data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Window";
            this.Text = "NeuroxPC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.closeForm);
            this.Load += new System.EventHandler(this.form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox data;
        public System.Windows.Forms.Button index;
        public System.Windows.Forms.Button cnewindex;
        public System.Windows.Forms.Button newPage;
        private System.Windows.Forms.Button loadPage;
        private System.Windows.Forms.SaveFileDialog saveBox;
        private System.Windows.Forms.ComboBox pageNumber;
        private System.Windows.Forms.Label pageCount;
        private System.Windows.Forms.Label debug;
        private System.Windows.Forms.Button savePage;
        private System.Windows.Forms.Button writeChanges;
        private System.Windows.Forms.Button delPage;
        private System.Windows.Forms.OpenFileDialog openBox;
    }
}

