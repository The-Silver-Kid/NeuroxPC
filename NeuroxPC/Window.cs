using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NeuroxPC {
    public partial class Window : Form {

        internal Index indexedData;
        Stream file;
        uint page;
        bool changes = false/*, fileChanged = false*/;
        string last;

        public Window() {
            InitializeComponent();
        }

        private void form1_Load(object sender, EventArgs e) {
            pageCount.Text = "Open or Create an Index File";
        }

        private void closeForm(object sender, FormClosingEventArgs e) {
        }

        private void createNewIndex(object sender, EventArgs e) {
            saveBox.ShowDialog(this);
        }

        private void createNewIndexConfirm(object sender, CancelEventArgs e) {
            loadPage.Enabled = false;
            newPage.Enabled = false;
            delPage.Enabled = false;
            pageNumber.Enabled = false;
            data.Enabled = false;
            data.Clear();
            savePage.Enabled = false;
            debug.Text = string.Empty;
            writeChanges.Enabled = false;
            if (!(file == null))
                file.Close();
            last = saveBox.FileName;
            file = saveBox.OpenFile();
            debug.Text = "Creating...";
            string pass = Prompt.getPass("Enter a password to protect this sheet", "Password");
            byte pin = Prompt.getPin("Enter a PIN", "PIN");

            indexedData = new IndexV1(pin, pass);

            byte[] crud = indexedData.encode();
            file.Write(crud, 0, crud.Length);
            file.Position = 0;

            loadPage.Enabled = true;
            newPage.Enabled = true;
            delPage.Enabled = true;
            addImage.Enabled = true;
            saveImg.Enabled = false;

            debug.Text = "Created Index";
            pageCount.Text = "Total Pages : 0";
        }

        private void loadContent(object sender, EventArgs e) {
            openBox.ShowDialog(this);
        }

        private void loadConfirm(object sender, CancelEventArgs e) {
            loadPage.Enabled = false;
            newPage.Enabled = false;
            delPage.Enabled = false;
            pageNumber.Enabled = false;
            data.Enabled = false;
            data.Clear();
            debug.Text = string.Empty;
            savePage.Enabled = false;
            writeChanges.Enabled = false;
            if (!(file == null))
                file.Close();
            last = openBox.FileName;
            file = new FileStream(openBox.FileName, FileMode.Open, FileAccess.ReadWrite);
            debug.Text = "Loading...";
            string pass = Prompt.getPass("Enter the password", "Password");
            byte pin = Prompt.getPin("Enter the PIN", "PIN");

            byte[] crud = new byte[file.Length];
            file.Read(crud, 0, crud.Length);
            file.Position = 0;

            indexedData = Index.iDindex(crud, pin, pass);

            loadPage.Enabled = true;
            saveImg.Enabled = false;
            newPage.Enabled = indexedData.checkout;
            delPage.Enabled = indexedData.checkout;
            addImage.Enabled = indexedData.checkout;

            if (!(indexedData.getAllKeys().Length == 0)) {
                pageNumber.Enabled = true;
                pageNumber.DataSource = indexedData.getAllKeys();
            }
            if (!Index.error)
                debug.Text = "Loaded Index";
            else {
                debug.Text = "An Error Occured while loading... Created a blank index.\n" +
                    "SAVING WILL OVERRIDE CONTENTS OF SELECTED FILE!";
                this.Text = this.Text + " IN OVERRIDE MODE : " + ((FileStream)file).Name;
                this.BackColor = Color.Red;
            }
            pageCount.Text = "Total Pages : " + indexedData.getAllKeys().Length;
        }

        private void cNewPage(object sender, EventArgs e) {
            picBox.Enabled = false;
            picBox.Visible = false;
            data.Visible = true;
            data.Enabled = true;
            saveImg.Enabled = false;
            //data.ResetText();
            page = getNextPage();
            debug.Text = "Currently Editing page : " + page;
        }

        private uint getNextPage() {
            uint ret = 0;
            uint[] keys = indexedData.getAllKeys();
            for (int i = 0; i < keys.Length; i++)
                if (ret < keys[i])
                    ret = keys[i];
            ret++;
            return ret;
        }

        private void lPage(object sender, EventArgs e) {
            if (pageNumber.Enabled) {
                page = (uint)pageNumber.SelectedValue;
                switch (indexedData.getPageType(page)) {
                    case (Index.TEXT):
                        picBox.Enabled = false;
                        picBox.Visible = false;
                        data.Visible = true;
                        data.Enabled = true;
                        saveImg.Enabled = false;
                        //data.ResetText();
                        data.Text = indexedData.getPageAsText((uint)pageNumber.SelectedValue);
                        break;
                    case (Index.IMAGE):
                        data.Enabled = false;
                        data.ResetText();
                        data.Visible = false;
                        picBox.Visible = true;
                        picBox.Enabled = true;
                        saveImg.Enabled = true;
                        picBox.Image = indexedData.getPageAsImage((uint)pageNumber.SelectedValue);
                        break;
                }
                debug.Text = "Loaded Page : " + pageNumber.SelectedValue;
            }
        }

        private void saveLocal(object sender, EventArgs e) {
            savePage.Enabled = false;
            debug.Text = debug.Text.Replace("\nUnsaved Changes!", string.Empty);
            if (indexedData.contains(page))
                indexedData.replacePage(page, Encoding.Unicode.GetBytes(data.Text), Index.TEXT);
            else
                indexedData.addPage(page, Encoding.Unicode.GetBytes(data.Text), Index.TEXT);
            /*fileChanged = true;*/
            writeChanges.Enabled = true;
            if (!(indexedData.getAllKeys().Length == 0)) {
                pageNumber.Enabled = true;
                pageNumber.DataSource = indexedData.getAllKeys();
            }
            changes = false;
            pageCount.Text = "Total Pages : " + indexedData.getAllKeys().Length;
            debug.Text = "Saved Changes Internally. Write to file to make changes official.";
        }

        private void delPageB(object sender, EventArgs e) {
            if (pageNumber.Enabled) {
                debug.Text = "Deleted Page : " + pageNumber.SelectedValue;
                if (!(indexedData.getAllKeys().Length == 0)) {
                    pageNumber.Enabled = true;
                    pageNumber.DataSource = indexedData.getAllKeys();
                } else {
                    pageNumber.Enabled = false;
                }
                pageCount.Text = "Total Pages : " + indexedData.getAllKeys().Length;
            }
        }

        private void writeChangesB(object sender, EventArgs e) {
            if (indexedData.checkout) {
                writeChanges.Enabled = false;
                byte[] crud = indexedData.encode();
                indexedData.encode();
                file.Write(crud, 0, crud.Length);
                file.Position = 0;
                debug.Text = "Wrote Changes to File.";
            } else
                debug.Text = "Cannot Write to File becasue of incorrect password or pin...";
        }

        private void data_TextChanged(object sender, EventArgs e) {
            if (!changes) {
                changes = true;
                if (indexedData.checkout)
                    savePage.Enabled = true;
                debug.Text = debug.Text + "\nUnsaved Changes!";
            }
        }

        private void addImageStart(object sender, EventArgs e) {
            openIMage.FileName = last;
            openIMage.ShowDialog();
        }

        private void addImageConfirm(object sender, CancelEventArgs e) {
            Bitmap Alfonse = (Bitmap)Image.FromStream(openIMage.OpenFile());
            page = getNextPage();
            if (indexedData.checkout) {
                if (indexedData.contains(page))
                    indexedData.replacePage(page, PicEncoder.encode(Alfonse), Index.IMAGE);
                else
                    indexedData.addPage(page, PicEncoder.encode(Alfonse), Index.IMAGE);
                writeChanges.Enabled = true;
                if (!(indexedData.getAllKeys().Length == 0)) {
                    pageNumber.Enabled = true;
                    pageNumber.DataSource = indexedData.getAllKeys();
                }
                changes = false;
                pageCount.Text = "Total Pages : " + indexedData.getAllKeys().Length;
                debug.Text = "Added Image into page " + page + ".\nSaved Changes Internally. Write to file to make changes official.\nLoad page to view saved image.";
            }
        }

        private void sImage(object sender, EventArgs e) {
            picBox.Image.Save("./img" + page + ".png");
            debug.Text = "Saved Image to img" + page + ".png.";
        }
    }

    static class Prompt {
        public static string getPass(string text, string caption) {
            Form prompt = new Form() {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            textBox.MaxLength = 16;
            Button confirmation = new Button() { Text = "Alright!", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        public static byte getPin(string text, string caption) {
            Form prompt = new Form() {
                Width = 250,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Width = 100, Text = text };
            NumericUpDown textBox = new NumericUpDown() { Left = 50, Top = 50, Width = 100 };
            textBox.Maximum = Byte.MaxValue;
            textBox.Minimum = Byte.MinValue;
            Button confirmation = new Button() { Text = "Excellent!", Left = 50, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? (byte)textBox.Value : (byte)0;
        }
    }
}
