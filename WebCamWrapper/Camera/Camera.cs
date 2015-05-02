using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using WebCamLib;

namespace Touchless.Vision.Camera
{
   // Source: https://msdn.microsoft.com/en-us/library/aa925325.aspx
   public enum CameraControlProperty : int
   {
      Pan_degrees,
      Tilt_degrees,
      Roll_degrees,
      Zoom_mm,
      Exposure_lgSec,
      Iris_10f,
      Focus_mm,
      Flash,
   };

   public sealed class CameraControlPropertyValue
   {
      public CameraControlPropertyValue( int value, bool isAuto )
      {
         Value = value;
         IsAuto = isAuto;
      }

      public int Value
      {
         get;
         private set;
      }

      public bool IsAuto
      {
         get;
         private set;
      }

      public bool IsManual
      {
         get
         {
            return !IsAuto;
         }
      }
   }

   public sealed class CameraControlPropertyRange
   {
      public CameraControlPropertyRange( int minimum, int maximum, int step, int defaults, bool isAuto )
      {
         Minimum = minimum;
         Maximum = maximum;
         Step = step;
         Defaults = defaults;
         IsAuto = isAuto;
      }

      public int Minimum
      {
         get;
         private set;
      }

      public int Maximum
      {
         get;
         private set;
      }

      public int Range
      {
         get
         {
            return Maximum - Minimum;
         }
      }

      public int Step
      {
         get;
         private set;
      }

      public int Defaults
      {
         get;
         private set;
      }

      public bool IsAuto
      {
         get;
         private set;
      }

      public bool IsManual
      {
         get
         {
            return !IsAuto;
         }
      }
   }

   public sealed class CaptureSize : IComparable<CaptureSize>, IEquatable<CaptureSize>
   {
      public CaptureSize( int width, int height, int colorDepth )
      {
         Width = width;
         Height = height;
         ColorDepth = colorDepth;
      }

      public int Width
      {
         get;
         private set;
      }

      public int Height
      {
         get;
         private set;
      }

      public int ColorDepth
      {
         get;
         private set;
      }

      public override String ToString()
      {
         return String.Format( "{0} x {1} @ {2}", Width, Height, ColorDepth );
      }

      #region IComparable<CaptureSize> Members

      public int CompareTo( CaptureSize other )
      {
         int result;

         if( Width < other.Width )
            result = -1;
         else if( Width > other.Width )
            result = 1;
         else
         {
            if( Height < other.Height )
               result = -1;
            else if( Height > other.Height )
               result = 1;
            else
            {
               if( ColorDepth < other.ColorDepth )
                  result = -1;
               else if( ColorDepth > other.ColorDepth )
                  result = 1;
               else
                  result = 0;
            }
         }

         return result;
      }

      #endregion

      #region IEquatable<CaptureSize> Members

      public bool Equals( CaptureSize other )
      {
         return CompareTo( other ) == 0;
      }

      #endregion

      public override bool Equals( Object obj )
      {
         bool result;

         CaptureSize other = obj as CaptureSize;
         if( result = other != null )
            result = Equals( other );

         return result;
      }

      public override int GetHashCode()
      {
         int result = 0;

         result ^= Width;
         result ^= Height << 11;
         result ^= ColorDepth << 23;

         return result;
      }
   }

   /// <summary>
   /// Represents a camera in use by the Touchless system
   /// </summary>
   public class Camera : IDisposable
   {
      public const int IgnoredBitsPerPixel = -1;

      private readonly Object CameraMethodsLock = new Object();

      private readonly CameraMethods _cameraMethods;
      private RotateFlipType _rotateFlip = RotateFlipType.RotateNoneFlipNone;

      public Camera( CameraMethods cameraMethods, string name, int index )
      {
         _name = name;
         _index = index;

         lock( CameraMethodsLock )
         {
            _cameraMethods = cameraMethods;
            _cameraMethods.OnImageCapture += CaptureCallbackProc;
         }
      }

      public string Name
      {
         get
         {
            return _name;
         }
      }

      /// <summary>
      /// Defines the frames per second limit that is in place, -1 means no limit
      /// </summary>
      public int Fps
      {
         get
         {
            return _fpslimit;
         }
         set
         {
            _fpslimit = value;
            _timeBetweenFrames = ( 1000.0 / _fpslimit );
         }
      }

      /// <summary>
      /// Determines the width of the image captured
      /// </summary>
      public int CaptureWidth
      {
         get
         {
            return _width;
         }
         set
         {
            _width = value;
         }
      }

      /// <summary>
      /// Defines the height of the image captured
      /// </summary>
      public int CaptureHeight
      {
         get
         {
            return _height;
         }
         set
         {
            _height = value;
         }
      }

      /// <summary>
      /// Defines the bits per pixel of image captured.
      /// </summary>
      public int CaptureBitsPerPixel
      {
         get
         {
            return _bpp;
         }

         set
         {
            _bpp = value;
         }
      }

      public bool HasFrameLimit
      {
         get
         {
            return _fpslimit != -1;
         }
      }

      public bool FlipHorizontal
      {
         get
         {
            return RotateFlip == RotateFlipType.RotateNoneFlipX || RotateFlip == RotateFlipType.Rotate180FlipNone;
         }

         set
         {
            if( value && FlipVertical )
            {
               RotateFlip = RotateFlipType.Rotate180FlipNone;
            }
            else if( value && !FlipVertical )
            {
               RotateFlip = RotateFlipType.RotateNoneFlipX;
            }
            else if( !value && FlipVertical )
            {
               RotateFlip = RotateFlipType.Rotate180FlipX;
            }
            else if( !value && !FlipVertical )
            {
               RotateFlip = RotateFlipType.RotateNoneFlipNone;
            }
         }
      }

      public bool FlipVertical
      {
         get
         {
            return RotateFlip == RotateFlipType.Rotate180FlipX || RotateFlip == RotateFlipType.Rotate180FlipNone;
         }

         set
         {
            if( value && FlipHorizontal )
            {
               RotateFlip = RotateFlipType.Rotate180FlipNone;
            }
            else if( value && !FlipHorizontal )
            {
               RotateFlip = RotateFlipType.Rotate180FlipX;
            }
            else if( !value && FlipHorizontal )
            {
               RotateFlip = RotateFlipType.RotateNoneFlipX;
            }
            else if( !value && !FlipHorizontal )
            {
               RotateFlip = RotateFlipType.RotateNoneFlipNone;
            }
         }
      }

      /// <summary>
      /// Command for rotating and flipping incoming images
      /// </summary>
      public RotateFlipType RotateFlip
      {
         get
         {
            return _rotateFlip;
         }
         set
         {
            // Swap height/width when rotating by 90 or 270
            if( ( int ) _rotateFlip % 2 != ( int ) value % 2 )
            {
               int temp = CaptureWidth;
               CaptureWidth = CaptureHeight;
               CaptureHeight = temp;
            }
            _rotateFlip = value;
         }
      }

      #region IDisposable Members

      /// <summary>
      /// Cleanup function for the camera
      /// </summary>
      public void Dispose()
      {
         StopCapture();
      }

      #endregion

      /// <summary>
      /// Returns the last image acquired from the camera
      /// </summary>
      /// <returns>A bitmap of the last image acquired from the camera</returns>
      public Bitmap GetCurrentImage()
      {
         Bitmap b = null;
         lock( _bitmapLock )
         {
            if( _bitmap == null )
            {
               return null;
            }

            b = new Bitmap( _bitmap );
         }

         return b;
      }

      public void ShowPropertiesDialog()
      {
         lock( CameraMethodsLock )
         {
            _cameraMethods.DisplayCameraPropertiesDialog( _index );
         }
      }

      public CameraInfo GetCameraInfo()
      {
         lock( CameraMethodsLock )
         {
            return _cameraMethods.GetCameraInfo( _index );
         }
      }

      public void SetProperty( int property, int value, bool auto ) // bntr
      {
         lock( CameraMethodsLock )
         {
            _cameraMethods.SetProperty( property, value, auto );
         }
      }

      public bool IsCameraPropertySupported( CameraControlProperty property )
      {
         bool result = false;

         lock( CameraMethodsLock )
         {
            _cameraMethods.IsCameraControlPropertySupported( ( int ) property, ref result );
         }

         return result;
      }

      public bool SetCameraProperty( CameraControlProperty property, bool auto )
      {
         return SetCameraProperty( property, 0, auto );
      }

      // Assume manual control
      public bool SetCameraProperty( CameraControlProperty property, int value )
      {
         return SetCameraProperty( property, value, false );
      }

      public bool SetCameraProperty( CameraControlProperty property, int value, bool auto )
      {
         bool result = false;

         lock( CameraMethodsLock )
         {
            _cameraMethods.SetCamaraControlProperty( ( int ) property, value, auto, ref result );
         }
         return result;
      }

      public CameraControlPropertyValue GetCameraProperty( CameraControlProperty property )
      {
         CameraControlPropertyValue result;

         {
            bool successful = false;

            int value = -1;
            bool isAuto = false;

            Exception inner = null;

            try
            {
               lock( CameraMethodsLock )
               {
                  _cameraMethods.GetCameraControlProperty( ( int ) property, ref value, ref isAuto, ref successful );
               }
            }
            catch( Exception e )
            {
               inner = e;
            }

            if( successful )
               result = new CameraControlPropertyValue( value, isAuto );
            else
               throw new InvalidOperationException( "Unable to retrieve the CameraControlProperty value.", inner );
         }

         return result;
      }

      public CameraControlPropertyRange GetCameraPropertyRange( CameraControlProperty property )
      {
         CameraControlPropertyRange result;

         {
            bool successful = false;

            int minimum, maximum, step, defaults;
            bool isAuto;

            minimum = maximum = step = defaults = -1;
            isAuto = false;

            Exception inner = null;

            try
            {
               lock( CameraMethodsLock )
               {
                  _cameraMethods.GetCameraControlPropertyRange( ( int ) property, ref minimum, ref maximum, ref step, ref defaults, ref isAuto, ref successful );
               }
            }
            catch( Exception e )
            {
               inner = e;
            }

            if( successful )
               result = new CameraControlPropertyRange( minimum, maximum, step, defaults, isAuto );
            else
               throw new InvalidOperationException( "Unable to retrieve the CameraControlProperty range.", inner );
         }

         return result;
      }

      public IList<CaptureSize> GetCaptureSizes()
      {
         List<Tuple<int, int, int>> rawSizes = new List<Tuple<int, int, int>>();

         lock( CameraMethodsLock )
         {
            _cameraMethods.GetCaptureSizes( _index, rawSizes );
         }

         IList<CaptureSize> result = new List<CaptureSize>( rawSizes.Count );
         foreach( Tuple<int, int, int> size in rawSizes )
         {
            CaptureSize newSize = new CaptureSize( size.Item1, size.Item2, size.Item3 );
            result.Add( newSize );
         }

         return result;
      }

      /// <summary>
      /// Event fired when an image from the camera is captured
      /// </summary>
      public event EventHandler<CameraEventArgs> OnImageCaptured;

      /// <summary>
      /// Returns the camera name as the ToString implementation
      /// </summary>
      /// <returns>The name of the camera</returns>
      public override string ToString()
      {
         return _name;
      }

      #region Internal Implementation

      private readonly object _bitmapLock = new object();
      private readonly int _index;
      private readonly string _name;
      private Bitmap _bitmap;
      private DateTime _dtLastCap = DateTime.MinValue;
      private int _fpslimit = -1;
      private int _height = 240;
      private double _timeBehind;
      private double _timeBetweenFrames;
      private int _width = 320;
      private int _bpp = 24;

      internal void StartCapture()
      {
         lock( CameraMethodsLock )
         {
            _cameraMethods.StartCamera( _index, ref _width, ref _height, ref _bpp );
         }
      }

      internal void StopCapture()
      {
         lock( CameraMethodsLock )
         {
            _cameraMethods.StopCamera();
         }
      }

      /// <summary>
      /// Here is where the images come in as they are collected, as fast as they can and on a background thread
      /// </summary>
      private void CaptureCallbackProc( int dataSize, byte[] data )
      {
         // Do the magic to create a bitmap
         int stride = _width * 3;
         GCHandle handle = GCHandle.Alloc( data, GCHandleType.Pinned );
         var scan0 = handle.AddrOfPinnedObject();
         scan0 += ( _height - 1 ) * stride;
         var b = new Bitmap( _width, _height, -stride, PixelFormat.Format24bppRgb, scan0 );
         b.RotateFlip( _rotateFlip );
         // NOTE: It seems that bntr has made that resolution property work properly
         var copyBitmap = ( Bitmap ) b.Clone();
         // Copy the image using the Thumbnail function to also resize if needed
         //var copyBitmap = (Bitmap)b.GetThumbnailImage(_width, _height, null, IntPtr.Zero);

         // Now you can free the handle
         handle.Free();

         ImageCaptured( copyBitmap );
      }

      private void ImageCaptured( Bitmap bitmap )
      {
         DateTime dtCap = DateTime.Now;

         // Always save the bitmap
         lock( _bitmapLock )
         {
            _bitmap = bitmap;
         }

         // FPS affects the callbacks only
         if( _fpslimit != -1 )
         {
            if( _dtLastCap != DateTime.MinValue )
            {
               double milliseconds = ( ( dtCap.Ticks - _dtLastCap.Ticks ) / TimeSpan.TicksPerMillisecond ) * 1.15;
               if( milliseconds + _timeBehind >= _timeBetweenFrames )
               {
                  _timeBehind = ( milliseconds - _timeBetweenFrames );
                  if( _timeBehind < 0.0 )
                  {
                     _timeBehind = 0.0;
                  }
               }
               else
               {
                  _timeBehind = 0.0;
                  return; // ignore the frame
               }
            }
         }

         if( OnImageCaptured != null )
         {
            var fps = ( int ) ( 1 / dtCap.Subtract( _dtLastCap ).TotalSeconds );
            OnImageCaptured.Invoke( this, new CameraEventArgs( bitmap, fps ) );
         }

         _dtLastCap = dtCap;
      }

      #endregion
   }

   /// <summary>
   /// Camera specific EventArgs that provides the Image being captured
   /// </summary>
   public class CameraEventArgs : EventArgs
   {
      /// <summary>
      /// Current Camera Image
      /// </summary>
      public Bitmap Image
      {
         get
         {
            return _image;
         }
      }

      public int CameraFps
      {
         get
         {
            return _cameraFps;
         }
      }

      #region Internal Implementation

      private readonly int _cameraFps;
      private readonly Bitmap _image;

      internal CameraEventArgs( Bitmap i, int fps )
      {
         _image = i;
         _cameraFps = fps;
      }

      #endregion
   }
}