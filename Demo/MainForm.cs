using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Touchless.Vision.Camera;

namespace Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    comboBoxCameras.Items.Add(cam);

                if (comboBoxCameras.Items.Count > 0)
                    comboBoxCameras.SelectedIndex = 0;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            thrashOldCamera();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            thrashOldCamera();
        }

        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void startCapturing()
        {
            try
            {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 320;
                _frameSource.Camera.CaptureHeight = 240;
                _frameSource.Camera.Fps = 20;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBoxDisplay.Invalidate();
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        //

        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        //

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_frameSource == null)
                return;

            Bitmap current = (Bitmap)_latestFrame.Clone();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "*.bmp|*.bmp";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    current.Save(sfd.FileName);
                }
            }

            current.Dispose();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            // snap camera
            if (_frameSource != null)
                _frameSource.Camera.ShowPropertiesDialog();
        }
    }
}
